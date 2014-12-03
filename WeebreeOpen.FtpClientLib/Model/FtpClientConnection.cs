namespace WeebreeOpen.FtpClientLib.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FtpClientConnection
    {
        public string ServerNameOrIp { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
