namespace WeebreeOpen.FtpClientLib.Service
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

        public FtpClientService(string serverNameOrIp, string userName, string password)
        {
            if (serverNameOrIp == null) { throw new ArgumentNullException("serverNameOrIp"); }
            if (userName == null) { throw new ArgumentNullException("userName"); }
            if (password == null) { throw new ArgumentNullException("password"); }

            this.FtpClientConnection = new FtpClientConnection(serverNameOrIp, userName, password);
        }

        public FtpClientService(FtpClientConnection connection)
        {
            if (connection == null) { throw new ArgumentNullException("connection"); }

            this.FtpClientConnection = connection;
        }

        #endregion

        #region Properties

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
        private static string[] _ParseFormats = new string[] {
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
            "(?<timestamp>\\d{2}\\-\\d{2}\\-\\d{2}\\s+\\d{2}:\\d{2}[Aa|Pp][mM])\\s+(?<dir>\\<\\w+\\>){0,1}(?<size>\\d+){0,1}\\s+(?<filename>.+)" };

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

            return ParseAndExtract(ftpWebResponseArray, directoryPath);
        }

        private static IEnumerable<FtpEntry> ParseAndExtract(string[] lines, string directoryPathInital)
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
                try
                {
                    string dateTimeString = split.Groups["timestamp"].Value;
                    Console.WriteLine("Date from Ftp Server: " + dateTimeString);
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
                }
                catch (Exception ex)
                {
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

        public bool CreateDirectory(string dirpath)
        {
            //perform create
            string URI = "ftp://" + this.FtpClientConnection.ServerNameOrIp + AdjustDir(dirpath);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            //Set request to MkDir
            ftp.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteDirectory(string dirpath)
        {
            //perform remove
            string URI = "ftp://" + this.FtpClientConnection.ServerNameOrIp + AdjustDir(dirpath);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            //Set request to RmDir
            ftp.Method = System.Net.WebRequestMethods.Ftp.RemoveDirectory;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteDirectoryRecursive(string dirpath)
        {
            // Delete all entries within the diretory
            List<FtpEntry> ftpEntries = GetDirectoryListingRecursive(dirpath);

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
            return DeleteDirectory(dirpath);
        }

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

        #endregion

        #region File Download

        public bool DownloadFile(string sourceFilename, string targetFilename, bool permitOverwrite, DateTime? setDateTimeForFile = null)
        {
            FileInfo fi = new FileInfo(targetFilename);
            return this.FileDownload(sourceFilename, fi, permitOverwrite, setDateTimeForFile);
        }

        public bool DownloadFileRecursive(string sourceStartingPath, string targetStartingPath, bool overrideExisting = false, bool deleteSourceAfterDownload = false)
        {
            List<FtpEntry> ftpEntries = GetDirectoryListingRecursive(sourceStartingPath);

            System.IO.Directory.CreateDirectory(targetStartingPath);

            // Copy all Files
            foreach (var ftpEntry in ftpEntries.OrderByDescending(x => x.DirectoryPath).ToList())
            {
                // Build target path
                string x1 = ftpEntry.DirectoryPath;
                if (!string.IsNullOrWhiteSpace(sourceStartingPath))
                {
                    x1 = ftpEntry.DirectoryPath.Replace(sourceStartingPath, "");
                }

                string targetFilePath = targetStartingPath + x1.Replace("/", @"\");

                string targetPath;
                if (ftpEntry.FtpEntryType == FtpEntryType.File)
                {
                    targetPath = Path.GetDirectoryName(targetFilePath);
                }
                else
                {
                    targetPath = targetFilePath;
                }

                if (AddSuffix(targetStartingPath, @"\") != AddSuffix(targetPath, @"\"))
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

        private bool FileDownload(string sourceFilename, FileInfo targetFI, bool permitOverwrite, DateTime? setDateTimeForFile = null)
        {
            //1. check target
            if (targetFI.Exists && !(permitOverwrite))
            {
                throw (new ApplicationException("Target file already exists"));
            }

            //2. check source
            string source;
            if (sourceFilename.Trim() == "")
            {
                throw (new ApplicationException("File not specified"));
            }
            else if (sourceFilename.Contains("/"))
            {
                //treat as a full path
                source = AdjustDir(sourceFilename);
            }
            else
            {
                //treat as filename only, use current directory
                source = sourceFilename;
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
                    using (FileStream fs = targetFI.OpenWrite())
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
                        catch (Exception)
                        {
                            //catch error and delete file only partially downloaded
                            fs.Close();
                            //delete target file as it's incomplete
                            targetFI.Delete();
                            throw;
                        }
                    }

                    responseStream.Close();
                }

                response.Close();
            }

            if (setDateTimeForFile != null)
            {
                targetFI.CreationTime = setDateTimeForFile.Value;
                targetFI.LastWriteTime = setDateTimeForFile.Value;
            }

            return true;
        }

        #endregion

        #region File Upload

        /// <summary>
        /// Copy a local file to the FTP server
        /// </summary>
        /// <param name="localFilename">Full path of the local file</param>
        /// <param name="targetFilename">Target filename, if required</param>
        /// <returns></returns>
        /// <remarks>If the target filename is blank, the source filename is used
        /// (assumes current directory). Otherwise use a filename to specify a name
        /// or a full path and filename if required.</remarks>
        public bool UploadFile(string localFilename, string targetFilename)
        {
            //1. check source
            if (!File.Exists(localFilename))
            {
                throw (new ApplicationException("File " + localFilename + " not found"));
            }
            //copy to FI
            FileInfo fi = new FileInfo(localFilename);
            return UploadFile(fi, targetFilename);
        }

        /// <summary>
        /// Upload a local file to the FTP server
        /// </summary>
        /// <param name="fi">Source file</param>
        /// <param name="targetFilename">Target filename (optional)</param>
        /// <returns></returns>
        private bool UploadFile(FileInfo fi, string targetFilename)
        {
            //copy the file specified to target file: target file can be full path or just filename (uses current dir)

            //1. check target
            string target;
            if (targetFilename.Trim() == "")
            {
                //Blank target: use source filename & current dir
                target = fi.Name;
            }
            else if (targetFilename.Contains("/"))
            {
                //If contains / treat as a full path
                target = AdjustDir(targetFilename);
            }
            else
            {
                //otherwise treat as filename only, use current directory
                target = targetFilename;
            }

            string URI = "ftp://" + this.FtpClientConnection.ServerNameOrIp + target;

            //perform copy
            System.Net.FtpWebRequest ftp = GetRequest(URI);

            //Set request to upload a file in binary
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;

            //Notify FTP of the expected size
            ftp.ContentLength = fi.Length;

            //create byte array to store: ensure at least 1 byte!
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;

            //open file for reading
            using (FileStream fs = fi.OpenRead())
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
                    return false;
                }
                finally
                {
                    //ensure file closed
                    fs.Close();
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
        /// <param name="filename">filename or full path</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteFile(string filename)
        {
            //Determine if file or full path
            string URI = "ftp://" + this.FtpClientConnection.ServerNameOrIp + GetFullPath(filename);

            System.Net.FtpWebRequest ftp = GetRequest(URI);
            //Set request to delete
            ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Helper

        //Get the basic FtpWebRequest object with the
        //common settings and security
        private FtpWebRequest GetRequest(string URI)
        {
            //create request
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(URI);
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
            for (int i = 0; i <= _ParseFormats.Length - 1; i++)
            {
                rx = new Regex(_ParseFormats[i]);
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