namespace WeebreeOpen.VisualStudioServerLib.Infrastructure.Test
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Telekom.Common.Auth;

    public class VsoClient3
    {
        #region Methods

        public static void TestConnection(string vsoTenantName, string username, string password)
        {
            string clientId = "";
            string clientSecret = "";
            string scope = "";

            TelekomOAuth2Auth.BaseUrl = "https://global.telekom.com/gcp-web-api";
            TelekomOAuth2Auth auth = new TelekomOAuth2Auth(clientId, clientSecret, scope);

            auth.RequestAccessToken();

            if (!auth.HasValidToken())
            {

                throw new Exception("Token invalid");
            }
            Console.WriteLine("Token valid"); System.Console.ReadKey();
        }

        #endregion
    }
}