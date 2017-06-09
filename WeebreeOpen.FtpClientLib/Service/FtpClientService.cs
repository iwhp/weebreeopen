using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WeebreeOpen.FtpClientLib.Model;

namespace WeebreeOpen.FtpClientLib.Service
{
    public class FtpClientService
    {
        #region Constructors

        private FtpClientService(bool isThrowException)
        {
            this.EventMessages = new List<FtpServiceEventArgs>();
            this.IsThrowException = isThrowException;
        }

        public FtpClientService(string serverNameOrIp, string userName, string password, bool isThrowException = true)
            : this(isThrowException)
        {
            if (serverNameOrIp == null) { throw new ArgumentNullException("serverNameOrIp"); }
            if (userName == null) { throw new ArgumentNullException("userName"); }
            if (password == null) { throw new ArgumentNullException("password"); }

            this.FtpClientConnection = new FtpClientConnection(serverNameOrIp, userName, password);
        }

        public FtpClientService(FtpClientConnection connection, bool isThrowException = true)
            : this(isThrowException)
        {
            if (connection == null) { throw new ArgumentNullException("connection"); }

            this.FtpClientConnection = connection;
        }

        #endregion

        #region Properties

        /// <summary>
        /// List of REGEX formats for different FTP server listing formats.
        /// </summary>
        private static readonly string[] parseFormats = new string[]
        {
            // UNIX/LINUX
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\w+\s+\w+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{4})\s+(?<filename>.+)",

            // UNIX/LINUX
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\d+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{4})\s+(?<filename>.+)",

            // UNIX/LINUX
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\d+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{1,2}:\d{2})\s+(?<filename>.+)",

            // ??? (ERNi DRUCK)
            // Processing: drwxrwxrwx               folder        0 May 29 19:22 TestDir1
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+folder\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{1,2}:\d{2})\s+(?<filename>.+)",

            // MS FTP in detailed mode
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\w+\s+\w+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{1,2}:\d{2})\s+(?<filename>.+)",
            @"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})(\s+)(?<size>(\d+))(\s+)(?<ctbit>(\w+\s\w+))(\s+)(?<size2>(\d+))\s+(?<timestamp>\w+\s+\d+\s+\d{2}:\d{2})\s+(?<filename>.+)",

            // MS FTP in 'DOS' mode
            @"(?<timestamp>\d{2}\-\d{2}\-\d{2}\s+\d{2}:\d{2}[Aa|Pp][mM])\s+(?<dir>\<\w+\>){0,1}(?<size>\d+){0,1}\s+(?<filename>.+)"
        };

        private bool IsThrowException { get; set; } = true;

        public List<FtpServiceEventArgs> EventMessages { get; set; }

        public FtpClientConnection FtpClientConnection { get; private set; }

        #endregion

        #region Event

        public event EventHandler<FtpServiceEventArgs> FtpServiceEvent;

        protected virtual void OnRaiseFtpServiceEvent(FtpServiceEventArgs e)
        {
            this.EventMessages.Add(e);
            EventHandler<FtpServiceEventArgs> handler = FtpServiceEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Directory Exists, Create, Delete

        /// <summary>
        /// Verifies if a directory does exist.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns>true if directory does exist, false if not.</returns>
        public bool DirectoryExists(string directoryPath)
        {
            directoryPath = AdjustDirectory(directoryPath);

            // Perform create
            string uri = "ftp://" + this.FtpClientConnection.ServerNameOrIp + directoryPath;
            FtpWebRequest ftp = GetRequest(uri);

            // Set request to MkDir
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;
            try
            {
                // Get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Will create one new directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns>true if directory was created, false if not.</returns>
        public bool CreateDirectory(string directoryPath)
        {
            directoryPath = AdjustDirectory(directoryPath);

            // Perform create
            string uri = "ftp://" + this.FtpClientConnection.ServerNameOrIp + directoryPath;
            FtpWebRequest ftp = GetRequest(uri);

            // Set request to MkDir
            ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                // Get response but ignore it
                string str = GetStringResponse(ftp);
                OnRaiseFtpServiceEvent(FtpServiceEventArgs.DirectoryCreate(directoryPath));
            }
            catch (Exception ex)
            {
                OnRaiseFtpServiceEvent(FtpServiceEventArgs.Error(string.Format("Could not create directory: [{0}].", directoryPath), ex));
                return false;
            }
            return true;
        }

        public bool DeleteDirectory(string directoryPath)
        {
            // Perform remove
            string uri = "ftp://" + this.FtpClientConnection.ServerNameOrIp + AdjustDirectory(directoryPath);
            FtpWebRequest ftp = GetRequest(uri);

            // Set request to RmDir
            ftp.Method = WebRequestMethods.Ftp.RemoveDirectory;
            try
            {
                // Get response but ignore it
                string str = GetStringResponse(ftp);
                OnRaiseFtpServiceEvent(FtpServiceEventArgs.DirectoryDelete(directoryPath));
            }
            catch (Exception ex)
            {
                OnRaiseFtpServiceEvent(FtpServiceEventArgs.Error(string.Format("Could not delete directory: [{0}].", directoryPath), ex));
                return false;
            }
            return true;
        }

        public bool DeleteDirectoryRecursive(string directoryPath)
        {
            // Delete all entries within the directory
            List<FtpEntry> ftpEntries = GetDirectoryListingRecursive(directoryPath);

            foreach (FtpEntry ftpEntry in ftpEntries.OrderByDescending(x => x.DirectoryPath).ToList())
            {
                if (ftpEntry.FtpEntryType == FtpEntryType.File)
                {
                    bool isFileDeleteOK = DeleteFile(ftpEntry.DirectoryPath);
                    if (!isFileDeleteOK) { return false; }
                }
                else
                {
                    bool isDirectoryDeleteOK = DeleteDirectory(ftpEntry.DirectoryPath);
                    if (!isDirectoryDeleteOK) { return false; }
                }
            }

            // Delete the directory requested
            return DeleteDirectory(directoryPath);
        }

        #endregion

        #region Directory Listing

        public List<FtpEntry> GetDirectoryListing(string initialDirectoryPath = "")
        {
            return ProcessListing(initialDirectoryPath).ToList();
        }

        public List<FtpEntry> GetDirectoryListingRecursive(string initialDirectoryPath = "")
        {
            List<FtpEntry> allEntries = ProcessListing(initialDirectoryPath).ToList();

            foreach (FtpEntry rootItem in allEntries.Where(x => x.FtpEntryType == FtpEntryType.Directory).ToList())
            {
                allEntries.AddRange(GetDirectoryListingRecursive(rootItem.DirectoryPath));
            }

            return allEntries;
        }

        private IEnumerable<FtpEntry> ProcessListing(string directoryPath)
        {
            StringBuilder ftpWebResponseString = new StringBuilder();

            FtpWebRequest ftpWebRequest;
            ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.FtpClientConnection.ServerNameOrIp + AdjustDirectory(directoryPath)));
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.Credentials = new NetworkCredential(this.FtpClientConnection.UserName, this.FtpClientConnection.Password);
            ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            WebResponse response;
            try
            {
                response = ftpWebRequest.GetResponse();
            }
            catch (Exception)
            {
                return new List<FtpEntry>();
            }
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string line = reader.ReadLine();
            while (line != null)
            {
                ftpWebResponseString.Append(line);
                ftpWebResponseString.Append("\n");

                line = reader.ReadLine();
            }

            reader.Close();
            response.Close();

            string[] ftpWebResponseArray = new string[0];
            if (ftpWebResponseString.Length != 0)
            {
                ftpWebResponseString.Remove(ftpWebResponseString.ToString().LastIndexOf('\n'), 1);
                ftpWebResponseArray = ftpWebResponseString.ToString().Split('\n');
            }

            return ParseAndExtractDirectoryListing(ftpWebResponseArray, directoryPath);
        }

        private IEnumerable<FtpEntry> ParseAndExtractDirectoryListing(string[] lines, string directoryPathInital)
        {
            foreach (string line in lines)
            {
                Match split = GetMatchingRegex(line);

                if (split == null)
                {
                    break;
                }

                int x;
                string dir = split.Groups["dir"].ToString();
                //var permission = split.Groups["permission"].ToString();
                //var filecode = split.Groups["filecode"].ToString();
                //var owner = split.Groups["owner"].ToString();
                //var group = split.Groups["group"].ToString();
                string size = split.Groups["size"].ToString();
                //var month = split.Groups["month"].ToString();
                //var timeYear = split.Groups["year"].ToString();
                //var time = split.Groups["time"].ToString();
                //var day = split.Groups["day"].ToString();
                DateTime dateTime;
                string dateTimeString = "";
                try
                {
                    dateTimeString = split.Groups["timestamp"].Value;
                    if (dateTimeString.Substring(3, 1) == " ") // "Dec 19 15:44"
                    {
                        if (dateTimeString.Length == 12)
                        {
                            dateTimeString = dateTimeString.Substring(0, 7) + DateTime.Now.Year + " " + dateTimeString.Substring(7, 5);
                            if (dateTimeString.Substring(4, 1) == " ")
                            {
                                dateTimeString = dateTimeString.Substring(0, 4) + dateTimeString.Substring(5);
                            }
                        }
                        dateTime = DateTime.ParseExact(dateTimeString, "MMM d yyyy HH:mm", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        dateTime = DateTime.Parse(split.Groups["timestamp"].Value, new CultureInfo("en-US"));
                    }
                    OnRaiseFtpServiceEvent(FtpServiceEventArgs.Information("Date from Ftp Server: " + dateTimeString + " converted to: " + dateTime));
                }
                catch (Exception ex)
                {
                    OnRaiseFtpServiceEvent(FtpServiceEventArgs.Error(string.Format("Could not parse date: [{0}].", dateTimeString), ex));
                    dateTime = Convert.ToDateTime(null);
                }

                string filename = split.Groups["filename"].ToString();

                FtpEntryType ftpEntryType;
                string directoryPath;
                if (dir != "" && dir != "-")
                {
                    ftpEntryType = FtpEntryType.Directory;
                    directoryPath = directoryPathInital + "/" + filename;
                }
                else
                {
                    ftpEntryType = FtpEntryType.File;
                    directoryPath = directoryPathInital + "/" + filename;
                }

                yield return new FtpEntry()
                {
                    //Dir = dir,
                    //Filecode = filecode,
                    //Group = group,
                    //FullPath = CurrentRemoteDirectory + "/ " + filename,
                    Name = filename,
                    //Owner = owner,
                    //Permission = permission,
                    Size = int.TryParse(size, out x) ? x : 0,
                    DateTime = dateTime,
                    FtpEntryType = ftpEntryType,
                    DirectoryPath = directoryPath
                    //Month = month,
                    //Day = day,
                    //YearTime = timeYear
                };
            }
        }

        #endregion

        #region File Download

        public bool DownloadFile(string filePathSource, string filePathTarget, bool isOverrideExisting, DateTime? setDateTimeForFile = null)
        {
            FileInfo fi = new FileInfo(filePathTarget);
            return this.DownloadFile(filePathSource, fi, isOverrideExisting, setDateTimeForFile);
        }

        public bool DownloadFileRecursive(string startingPathSource, string startingPathTarget, bool isOverrideExisting = false, bool isDeleteSourceAfterDownload = false, bool isDeleteChildDirectories = false)
        {
            List<FtpEntry> ftpEntries = GetDirectoryListingRecursive(startingPathSource);

            System.IO.Directory.CreateDirectory(startingPathTarget);

            // Copy all Files
            foreach (FtpEntry ftpEntry in ftpEntries.OrderByDescending(x => x.DirectoryPath).ToList())
            {
                // Build target path
                string x1 = ftpEntry.DirectoryPath;
                if (!string.IsNullOrWhiteSpace(startingPathSource))
                {
                    x1 = ftpEntry.DirectoryPath.Replace(startingPathSource, "");
                }

                string targetFilePath = startingPathTarget + x1.Replace("/", @"\");

                string targetPath;
                if (ftpEntry.FtpEntryType == FtpEntryType.File)
                {
                    targetPath = Path.GetDirectoryName(targetFilePath);
                }
                else
                {
                    targetPath = targetFilePath;
                }

                if (AddSuffix(startingPathTarget, @"\") != AddSuffix(targetPath, @"\"))
                {
                    DirectoryInfo directoryCreated = Directory.CreateDirectory(targetPath);
                    directoryCreated.CreationTime = ftpEntry.DateTime;
                    directoryCreated.LastWriteTime = ftpEntry.DateTime;
                }

                bool isFileDownloadOK = true;
                if (ftpEntry.FtpEntryType == FtpEntryType.File)
                {
                    isFileDownloadOK = DownloadFile(ftpEntry.DirectoryPath, targetFilePath, isOverrideExisting, ftpEntry.DateTime);
                }

                if (isFileDownloadOK)
                {
                    if (isDeleteSourceAfterDownload)
                    {
                        if (ftpEntry.FtpEntryType == FtpEntryType.File)
                        {
                            bool isFileDeleteOK = DeleteFile(ftpEntry.DirectoryPath);
                            if (!isFileDeleteOK)
                            {
                                OnRaiseFtpServiceEvent(FtpServiceEventArgs.Error(string.Format("During coping file could not delete file: [{0}].", ftpEntry.DirectoryPath)));
                                return false;
                            }
                        }
                        else
                        {
                            if (isDeleteChildDirectories)
                            {
                                // Delete Directory (if not empty, it will not be deleted (return code = false))
                                bool isDirectoryDeleteOK = DeleteDirectory(ftpEntry.DirectoryPath);
                                if (!isDirectoryDeleteOK)
                                {
                                    OnRaiseFtpServiceEvent(FtpServiceEventArgs.Error(string.Format("During coping file could not delete directory (direcctory no empty?): [{0}].", ftpEntry.DirectoryPath)));
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private bool DownloadFile(string filePathSource, FileInfo fileInfoTarget, bool isOverrideExisting, DateTime? setDateTimeForFile = null)
        {
            // 1. check target
            if (fileInfoTarget.Exists && !(isOverrideExisting))
            {
                throw (new ApplicationException("Target file already exists"));
            }

            // 2. check source
            string source;
            if (filePathSource.Trim() == "")
            {
                throw (new ApplicationException("File not specified"));
            }
            else if (filePathSource.Contains("/"))
            {
                // Treat as a full path
                source = AdjustFile(filePathSource);
            }
            else
            {
                // Treat as filename only, use current directory
                source = filePathSource;
            }

            string URI = "ftp://" + this.FtpClientConnection.ServerNameOrIp + source;

            // 3. perform copy
            FtpWebRequest ftp = GetRequest(URI);

            // Set request to download a file in binary mode
            ftp.Method = WebRequestMethods.Ftp.DownloadFile;
            ftp.UseBinary = true;

            // Open request and get response stream
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Loop to read & write to file
                    using (FileStream fs = fileInfoTarget.OpenWrite())
                    {
                        try
                        {
                            byte[] buffer = new byte[2048];
                            int read = 0;
                            do
                            {
                                read = responseStream.Read(buffer, 0, buffer.Length);
                                fs.Write(buffer, 0, read);
                            } while (!(read == 0));
                            responseStream.Close();
                            fs.Flush();
                            fs.Close();
                        }
                        catch (Exception ex)
                        {
                            OnRaiseFtpServiceEvent(FtpServiceEventArgs.Error(string.Format("Could not download file: [{0}] to file [{1}].", filePathSource, fileInfoTarget), ex));
                            // Catch error and delete file only partially downloaded
                            fs.Close();
                            // Delete target file as it's incomplete
                            fileInfoTarget.Delete();
                            return false;
                        }
                    }

                    responseStream.Close();
                }

                response.Close();
                OnRaiseFtpServiceEvent(FtpServiceEventArgs.FileDownload(filePathSource, fileInfoTarget.FullName));
            }

            if (setDateTimeForFile != null)
            {
                fileInfoTarget.CreationTime = setDateTimeForFile.Value;
                fileInfoTarget.LastWriteTime = setDateTimeForFile.Value;
            }

            return true;
        }

        #endregion

        #region File Upload

        /// <summary>
        /// Copy a local file to the FTP server
        /// </summary>
        /// <param name="filePathSource">Full path of the local file</param>
        /// <param name="filePathTarget">Target filename, if required</param>
        /// <returns></returns>
        /// <remarks>If the target filename is blank, the source filename is used
        /// (assumes current directory). Otherwise use a filename to specify a name
        /// or a full path and filename if required.</remarks>
        public bool UploadFile(string filePathSource, string filePathTarget, bool isOverrideExisting = false, bool isDeleteSourceAfterDownload = false)
        {
            filePathTarget = AdjustFile(filePathTarget);

            // Check if source exists
            if (!File.Exists(filePathSource))
            {
                throw (new ApplicationException("File " + filePathSource + " not found"));
            }

            // Check if target exists and should be deleted
            if (FileExists(filePathTarget) && isOverrideExisting)
            {
                DeleteFile(filePathTarget);
            }

            // Copy to FI
            FileInfo fi = new FileInfo(filePathSource);
            bool result = UploadFile(fi, filePathTarget);

            if (result && isDeleteSourceAfterDownload)
            {
                fi.Delete();
            }
            return result;
        }

        /// <summary>
        /// Upload a local file to the FTP server
        /// </summary>
        /// <param name="fileInfoSource">Source file</param>
        /// <param name="filePathTarget">Target filename (optional)</param>
        /// <returns></returns>
        private bool UploadFile(FileInfo fileInfoSource, string filePathTarget)
        {
            filePathTarget = AdjustFile(filePathTarget);

            // Copy the file specified to target file: target file can be full path or just filename (uses current dir)

            // 1. check target
            string target;
            if (filePathTarget.Trim() == "")
            {
                // Blank target: use source filename & current dir
                target = fileInfoSource.Name;
            }
            else if (filePathTarget.Contains("/"))
            {
                // If contains / treat as a full path
                target = AdjustFile(filePathTarget);
            }
            else
            {
                // Otherwise treat as filename only, use current directory
                target = filePathTarget;
            }

            string Uri = "ftp://" + this.FtpClientConnection.ServerNameOrIp + target;

            // Perform copy
            FtpWebRequest ftp = GetRequest(Uri);

            // Set request to upload a file in binary
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;

            // Notify FTP of the expected size
            ftp.ContentLength = fileInfoSource.Length;

            // Create byte array to store: ensure at least 1 byte!
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;

            // Open file for reading
            using (FileStream fs = fileInfoSource.OpenRead())
            {
                try
                {
                    // Open request to send
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            dataRead = fs.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < BufferSize));
                        rs.Close();
                    }

                }
                catch (Exception ex)
                {
                    OnRaiseFtpServiceEvent(FtpServiceEventArgs.Error(string.Format("Could not upload file: [{0}] to file [{1}].", fileInfoSource.FullName, filePathTarget), ex));
                    return false;
                }
                finally
                {
                    // Ensure file closed
                    fs.Close();
                    OnRaiseFtpServiceEvent(FtpServiceEventArgs.FileUpload(fileInfoSource.FullName, filePathTarget));
                }
            }

            ftp = null;
            return true;
        }

        #endregion

        #region File Delete

        /// <summary>
        /// Delete remote file
        /// </summary>
        /// <param name="filePath">filename or full path</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteFile(string filePath)
        {
            // Determine if file or full path
            string uri = "ftp://" + this.FtpClientConnection.ServerNameOrIp + GetFullPath(filePath);

            FtpWebRequest ftp = GetRequest(uri);
            // Set request to delete
            ftp.Method = WebRequestMethods.Ftp.DeleteFile;
            try
            {
                // Get response but ignore it
                string str = GetStringResponse(ftp);
                OnRaiseFtpServiceEvent(FtpServiceEventArgs.FileDelete(filePath));
            }
            catch (Exception ex)
            {
                OnRaiseFtpServiceEvent(FtpServiceEventArgs.Error(string.Format("Could not delete file: [{0}].", filePath), ex));
                return false;
            }

            return true;
        }

        #endregion

        #region File Exits

        public bool FileExists(string filePath)
        {
            // Prepare FtpWebRequest
            string uri = "ftp://" + this.FtpClientConnection.ServerNameOrIp + GetFullPath(filePath);
            FtpWebRequest ftp = GetRequest(uri);
            ftp.Method = WebRequestMethods.Ftp.GetFileSize; // Set request to get size

            try
            {
                GetStringResponse(ftp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Convert FtpServiceArgs

        public static string BuildHtmlMessage(List<FtpServiceEventArgs> ftpServiceEvents)
        {
            if (ftpServiceEvents == null)
            {
                throw new ArgumentNullException("ftpServiceEvents");
            }

            string outputHtml = "";

            // Handle errors
            outputHtml += "<h1>FTP SERVICE ERRORS</h1>";
            outputHtml += string.Format("<p>Errors Occurred: {0}</p>", ftpServiceEvents.Where(x => x.Type == FtpServiceEventType.Error).Count());
            outputHtml += "<ul>";

            foreach (FtpServiceEventArgs item in ftpServiceEvents.Where(x => x.Type == FtpServiceEventType.Error).OrderBy(x => x.EventOccuredAt))
            {
                outputHtml = FormatHtmlOutputForItem(outputHtml, item);
            }

            outputHtml += "</ul>";

            // Handle all others
            outputHtml += "<h1>FTP SERVICE ACTIONS</h1>";
            outputHtml += string.Format("<p>Number of Actions: {0}</p>", ftpServiceEvents.Where(x => x.Type != FtpServiceEventType.Error).Count());
            outputHtml += "<ul>";

            foreach (FtpServiceEventArgs item in ftpServiceEvents.Where(x => x.Type != FtpServiceEventType.Error).OrderBy(x => x.EventOccuredAt))
            {
                outputHtml = FormatHtmlOutputForItem(outputHtml, item);
            }
            outputHtml += "</ul>";

            return outputHtml;
        }

        private static string FormatHtmlOutputForItem(string outputHtml, FtpServiceEventArgs item)
        {
            string exception = item.Exception == null ? "" : item.Exception.ToString();

            outputHtml += string.Format("<li>{0} - {1}<br/>From: {2}{3}{4}{5}<br/>To: {6}{7}<br/>Exception: {8}<br/></li>",
                item.Type, item.Message,
                item.Directory, item.File, item.DirectoryFrom, item.FileFrom,
                item.DirectoryTo, item.FileTo,
                exception);

            outputHtml = outputHtml.Replace("From: <br/>", "");
            outputHtml = outputHtml.Replace("To: <br/>", "");
            outputHtml = outputHtml.Replace("Exception: <br/>", "");

            return outputHtml;
        }

        #endregion

        #region Helper

        /// <summary>
        /// Obtains a response stream as a string
        /// </summary>
        /// <param name="ftp">current FTP request</param>
        /// <returns>String containing response</returns>
        /// <remarks>FTP servers typically return strings with CR and not CRLF. Use respons.Replace(vbCR, vbCRLF) to convert to an MSDOS string</remarks>
        private string GetStringResponse(FtpWebRequest ftp)
        {
            //Get the result, streaming to a string
            string result = "";
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                long size = response.ContentLength;
                using (Stream datastream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(datastream))
                    {
                        result = sr.ReadToEnd();
                        sr.Close();
                    }

                    datastream.Close();
                }

                response.Close();
            }

            return result;
        }

        //Get the basic FtpWebRequest object with the
        //common settings and security
        private FtpWebRequest GetRequest(string uri)
        {
            // Create request
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(uri);
            // Set the login details
            result.Credentials = GetCredentials();
            // Do not keep alive (stateless mode)
            result.KeepAlive = false;
            return result;
        }

        /// <summary>
        /// Get the credentials from username/password
        /// </summary>
        private ICredentials GetCredentials()
        {
            return new NetworkCredential(this.FtpClientConnection.UserName, this.FtpClientConnection.Password);
        }

        private static Match GetMatchingRegex(string line)
        {
            Regex rx;
            Match m;
            for (int i = 0; i <= parseFormats.Length - 1; i++)
            {
                rx = new Regex(parseFormats[i]);
                m = rx.Match(line);
                if (m.Success)
                {
                    return m;
                }
            }
            return null;
        }

        private string AdjustDirectory(string path)
        {
            // Add [/] if missing at the beginning
            path = ((path.StartsWith("/")) ? "" : "/").ToString() + path;

            // Add [/] if missing at the end
            path = path + ((path.EndsWith("/")) ? "" : "/").ToString();

            return path;
        }

        private string AdjustFile(string path)
        {
            // If [\] in path, replace with [/]
            if (path.Contains(@"\"))
            {
                path = path.Replace(@"\", "/");
            }

            // Add [/] if missing at the beginning
            path = ((path.StartsWith("/")) ? "" : "/").ToString() + path;

            return path;
        }

        private string GetFullPath(string file)
        {
            if (file.Contains("/"))
            {
                return AdjustFile(file);
            }
            else
            {
                return file;
            }
        }

        private string AddSuffix(string target, string suffix)
        {
            if (target.Substring(target.Length - 1) != suffix)
            {
                target += suffix;
            }

            return target;
        }

        #endregion
    }
}
