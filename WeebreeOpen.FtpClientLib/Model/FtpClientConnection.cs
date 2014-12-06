namespace WeebreeOpen.FtpClientLib.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FtpClientConnection
    {
        public FtpClientConnection(string serverNameOrIp, string userName, string password)
        {
            if (serverNameOrIp == null) { throw new ArgumentNullException("serverNameOrIp"); }
            if (userName == null) { throw new ArgumentNullException("userName"); }
            if (password == null) { throw new ArgumentNullException("password"); }

            this.ServerNameOrIp = serverNameOrIp;
            this.UserName = userName;
            this.Password = password;
        }
        public string ServerNameOrIp { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
