﻿namespace WeebreeOpen.FtpClientLib.Service
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using WeebreeOpen.FtpClientLib.Model;

    public class FtpClientService
    {
        #region Constructors

        private FtpClientService()
        {
            this.EventMessages = new List<FtpServiceEventArgs>();
        }

        public FtpClientService(string serverNameOrIp, string userName, string password)
            : this()
        {
            if (serverNameOrIp == null) { throw new ArgumentNullException("serverNameOrIp"); }
            if (userName == null) { throw new ArgumentNullException("userName"); }
            if (password == null) { throw new ArgumentNullException("password"); }

            this.FtpClientConnection = new FtpClientConnection(serverNameOrIp, userName, password);
        }

        public FtpClientService(FtpClientConnection connection)
            : this()
        {
            if (connection == null) { throw new ArgumentNullException("connection"); }

            this.FtpClientConnection = connection;
        }

        #endregion

        #region Properties

        public List<FtpServiceEventArgs> EventMessages { get; set; }

        public FtpClientConnection FtpClientConnection { get; private set; }

        //private string currentDirectory = "/";

        //public string CurrentDirectory
        //{
        //    get
        //    {
        //        //return directory, ensure it ends with /
        //        return currentDirectory + ((currentDirectory.EndsWith("/")) ? "" : "/").ToString();
        //    }
        //    set
        //    {
        //        if (!value.StartsWith("/"))
        //        {
        //            throw (new ApplicationException("Directory should start with /"));
        //        }
        //        currentDirectory = value;
        //    }
        //}

        /// <summary>
        /// List of REGEX formats for different FTP server listing formats.
        /// </summary>
        private static readonly string[] parseFormats = new string[]
        {
            //  UNIX/LINUX
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<filename>.+)",
            //  UNIX/LINUX
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<filename>.+)",
            //  UNIX/LINUX
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<filename>.+)",
            //  MS FTP in detailed mode
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<filename>.+)",
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})(\\s+)(?<size>(\\d+))(\\s+)(?<ctbit>(\\w+\\s\\w+))(\\s+)(?<size2>(\\d+))\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{2}:\\d{2})\\s+(?<filename>.+)",
            //  MS FTP in 'DOS' mode
            "(?<timestamp>\\d{2}\\-\\d{2}\\-\\d{2}\\s+\\d{2}:\\d{2}[Aa|Pp][mM])\\s+(?<dir>\\<\\w+\\>){0,1}(?<size>\\d+){0,1}\\s+(?<filename>.+)"
        };

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

        #region Directory Listing

        public List<FtpEntry> GetDirectoryListing(string initialDirectoryPath = "")
        {
            return ProcessListing(initialDirectoryPath).ToList();
        }

        public List<FtpEntry> GetDirectoryListingRecursive(string initialDirectoryPath = "")
        {
            List<FtpEntry> allEntries = ProcessListing(initialDirectoryPath).ToList();

            foreach (var rootItem in allEntries.Where(x => x.FtpEntryType == FtpEntryType.Directory).ToList())
            {
                allEntries.AddRange(GetDirectoryListingRecursive(rootItem.DirectoryPath));
            }

            return allEntries;
        }

        private IEnumerable<FtpEntry> ProcessListing(string directoryPath)
        {
            var ftpWebResponseString = new StringBuilder();

            FtpWebRequest ftpWebRequest;
            ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.FtpClientConnection.ServerNameOrIp + "/" + directoryPath));
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
            foreach (var line in lines)
            {
                Match split = GetMatchingRegex(line);

                int x;
                var dir = split.Groups["dir"].ToString();
                //var permission = split.Groups["permission"].ToString();
                //var filecode = split.Groups["filecode"].ToString();
                //var owner = split.Groups["owner"].ToString();
                //var group = split.Groups["group"].ToString();
                var size = split.Groups["size"].ToString();
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

                var filename = split.Groups["filename"].ToString();

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
                    Size = Int32.TryParse(size, out x) ? x : 0,
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

        #region Directory Create, Delete

        public bool CreateDirectory(string directoryPath)
        {
            //perform create
            string uri = "ftp://" + this.FtpClientConnection.ServerNameOrIp + AdjustDir(directoryPath);
            System.Net.FtpWebRequest ftp = GetRequest(uri);
            //Set request to MkDir
            ftp.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                //get response but ignore it
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
            //perform remove
            string uri = "ftp://" + this.FtpClientConnection.ServerNameOrIp + AdjustDir(directoryPath);
            System.Net.FtpWebRequest ftp = GetRequest(uri);
            //Set request to RmDir
            ftp.Method = System.Net.WebRequestMethods.Ftp.RemoveDirectory;
            try
            {
                //get response but ignore it
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
            // Delete all entries within the diretory
            List<FtpEntry> ftpEntries = GetDirectoryListingRecursive(directoryPath);

            foreach (var ftpEntry in ftpEntries.OrderByDescending(x => x.DirectoryPath).ToList())
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

        #region File Download

        public bool DownloadFile(string filePathSource, string filePathTarget, bool overrideExisting, DateTime? setDateTimeForFile = null)
        {
            FileInfo fi = new FileInfo(filePathTarget);
            return this.DownloadFile(filePathSource, fi, overrideExisting, setDateTimeForFile);
        }

        public bool DownloadFileRecursive(string startingPathSource, string startingPathTarget, bool overrideExisting = false, bool deleteSourceAfterDownload = false)
        {
            List<FtpEntry> ftpEntries = GetDirectoryListingRecursive(startingPathSource);

            System.IO.Directory.CreateDirectory(startingPathTarget);

            // Copy all Files
            foreach (var ftpEntry in ftpEntries.OrderByDescending(x => x.DirectoryPath).ToList())
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
                    Console.WriteLine(ftpEntry.DateTime);
                    isFileDownloadOK = DownloadFile(ftpEntry.DirectoryPath, targetFilePath, overrideExisting, ftpEntry.DateTime);
                }

                if (isFileDownloadOK)
                {
                    if (deleteSourceAfterDownload)
                    {
                        if (ftpEntry.FtpEntryType == FtpEntryType.File)
                        {
                            bool isFileDeleteOK = DeleteFile(ftpEntry.DirectoryPath);
                            if (!isFileDeleteOK) { return false; }
                        }

                        // Delete Directory (if not empty, it will not be deleted (return code = false))
                        bool isDirectoryDeleteOK = DeleteDirectory(ftpEntry.DirectoryPath);
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private bool DownloadFile(string filePathSource, FileInfo fileInfoTarget, bool overrideExisting, DateTime? setDateTimeForFile = null)
        {
            //1. check target
            if (fileInfoTarget.Exists && !(overrideExisting))
            {
                throw (new ApplicationException("Target file already exists"));
            }

            //2. check source
            string source;
            if (filePathSource.Trim() == "")
            {
                throw (new ApplicationException("File not specified"));
            }
            else if (filePathSource.Contains("/"))
            {
                //treat as a full path
                source = AdjustDir(filePathSource);
            }
            else
            {
                //treat as filename only, use current directory
                source = filePathSource;
            }

            string URI = "ftp://" + this.FtpClientConnection.ServerNameOrIp + source;

            //3. perform copy
            System.Net.FtpWebRequest ftp = GetRequest(URI);

            //Set request to download a file in binary mode
            ftp.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;
            ftp.UseBinary = true;

            //open request and get response stream
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    //loop to read & write to file
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
                            //catch error and delete file only partially downloaded
                            fs.Close();
                            //delete target file as it's incomplete
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
        public bool UploadFile(string filePathSource, string filePathTarget)
        {
            //1. check source
            if (!File.Exists(filePathSource))
            {
                throw (new ApplicationException("File " + filePathSource + " not found"));
            }
            //copy to FI
            FileInfo fi = new FileInfo(filePathSource);
            return UploadFile(fi, filePathTarget);
        }

        /// <summary>
        /// Upload a local file to the FTP server
        /// </summary>
        /// <param name="fileInfoSource">Source file</param>
        /// <param name="filePathTarget">Target filename (optional)</param>
        /// <returns></returns>
        private bool UploadFile(FileInfo fileInfoSource, string filePathTarget)
        {
            //copy the file specified to target file: target file can be full path or just filename (uses current dir)

            //1. check target
            string target;
            if (filePathTarget.Trim() == "")
            {
                //Blank target: use source filename & current dir
                target = fileInfoSource.Name;
            }
            else if (filePathTarget.Contains("/"))
            {
                //If contains / treat as a full path
                target = AdjustDir(filePathTarget);
            }
            else
            {
                //otherwise treat as filename only, use current directory
                target = filePathTarget;
            }

            string URI = "ftp://" + this.FtpClientConnection.ServerNameOrIp + target;

            //perform copy
            System.Net.FtpWebRequest ftp = GetRequest(URI);

            //Set request to upload a file in binary
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;

            //Notify FTP of the expected size
            ftp.ContentLength = fileInfoSource.Length;

            //create byte array to store: ensure at least 1 byte!
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;

            //open file for reading
            using (FileStream fs = fileInfoSource.OpenRead())
            {
                try
                {
                    //open request to send
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
                    //ensure file closed
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
            //Determine if file or full path
            string uri = "ftp://" + this.FtpClientConnection.ServerNameOrIp + GetFullPath(filePath);

            System.Net.FtpWebRequest ftp = GetRequest(uri);
            //Set request to delete
            ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile;
            try
            {
                //get response but ignore it
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

        #region Convert FtpServiceArgs

        public static string BuildHtmlMessage(List<FtpServiceEventArgs> ftpServiceEvents)
        {
            string outputHtml = "";

            // Handle errors
            outputHtml += "<h1>FTP SERVICE ERRORS</h1>";
            outputHtml += string.Format("<p>Errors Occurred: {0}</p>", ftpServiceEvents.Where(x => x.Type == FtpServiceEventType.Error).Count());
            outputHtml += "<ul>";

            foreach (var item in ftpServiceEvents.Where(x => x.Type == FtpServiceEventType.Error).OrderBy(x => x.EventOccuredAt))
            {
                outputHtml += string.Format("<li>{0} - {1}<br/>From: {2}{3}{4}{5}<br/>To: {6}{7}<br>{8}</li>",
                    item.Type, item.Message,
                    item.Directory, item.File, item.DirectoryFrom, item.FileFrom,
                    item.DirectoryTo, item.FileTo,
                    item.Exception.ToString());
            }

            outputHtml += "</ul>";

            // Handle all others
            outputHtml += "<h1>FTP SERVICE ACTIONS</h1>";
            outputHtml += string.Format("<p>Number of Actions: {0}", ftpServiceEvents.Where(x => x.Type != FtpServiceEventType.Error).Count());
            outputHtml += "<ul>";

            foreach (var item in ftpServiceEvents.Where(x => x.Type != FtpServiceEventType.Error).OrderBy(x => x.EventOccuredAt))
            {
                string exception = item.Exception == null ? "" : item.Exception.ToString();

                outputHtml += string.Format("<li>{0} - {1}<br/>From: {2}{3}{4}{5}<br/>To: {6}{7}<br>{8}</li>",
                    item.Type, item.Message,
                    item.Directory, item.File, item.DirectoryFrom, item.FileFrom,
                    item.DirectoryTo, item.FileTo,
                    exception);
            }
            outputHtml += "</ul>";

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
            //create request
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(uri);
            //Set the login details
            result.Credentials = GetCredentials();
            //Do not keep alive (stateless mode)
            result.KeepAlive = false;
            return result;
        }

        /// <summary>
        /// Get the credentials from username/password
        /// </summary>
        private System.Net.ICredentials GetCredentials()
        {
            return new System.Net.NetworkCredential(this.FtpClientConnection.UserName, this.FtpClientConnection.Password);
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

        private string AdjustDir(string path)
        {
            return ((path.StartsWith("/")) ? "" : "/").ToString() + path;
        }

        private string GetFullPath(string file)
        {
            if (file.Contains("/"))
            {
                return AdjustDir(file);
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