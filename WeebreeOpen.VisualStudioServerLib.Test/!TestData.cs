namespace WeebreeOpen.VisualStudioServerLib.Test
{
    using System;
    using System.Linq;
    using System.Net;
    using WeebreeOpen.VisualStudioServerLib.Test.Properties;

    public class TestData
    {
        public static string RootUrl = "";

        public static string VsoTenantName
        {
            get { return Settings.Default.VsoTenantName; }
        }

        public static string UserName
        {
            get { return Settings.Default.UserName; }
        }
        public static string Password
        {
            get { return Settings.Default.Password; }
        }

        public static NetworkCredential UserCredential
        {
            get
            {
                return new NetworkCredential(TestData.UserName, TestData.Password);
            }
        }

    }
}
