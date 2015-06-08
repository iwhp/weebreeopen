namespace WeebreeOpen.VisualStudioServerLib.Infrastructure.Test
{
    // .Net Framework Libraries
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Script.Serialization;

    // Bouncy Castle Libraries used for CMAC encryption
    using Org.BouncyCastle.Crypto.Engines;
    using Org.BouncyCastle.Crypto.Macs;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Utilities.Encoders;

    static class VsoClient4
    {
        static void Main(string[] args)
        {
            // Setup the variables necessary to make the Request
            string grantType = "assertion";
            string assertionType = "urn:ecollege:names:moauth:1.0:assertion";
            string applicationName = "{applicationName}";
            string keyMoniker = "{consumerKey}";
            string applicationID = "E3DC2183-E99C-43D0-8996-60897B1F8482";
            string clientString = "{clientString}";
            string username = "{username}";
            string secret = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Im9PdmN6NU1fN3AtSGpJS2xGWHo5M3VfVjBabyJ9.eyJjaWQiOiJlM2RjMjE4My1lOTljLTQzZDAtODk5Ni02MDg5N2IxZjg0ODIiLCJjc2kiOiI1ODBiOWM0Mi02NTY0LTRhMmMtYTE5Mi0yZTMxYjFlMTIwODMiLCJuYW1laWQiOiI2NGZjYjFkMS1mNDUwLTQ5MDYtYTY0Yy01OTZjNmVlYzRlMDgiLCJpc3MiOiJhcHAudnNzcHMudmlzdWFsc3R1ZGlvLmNvbSIsImF1ZCI6ImFwcC52c3Nwcy52aXN1YWxzdHVkaW8uY29tIiwibmJmIjoxNDI2NDQxNjY0LCJleHAiOjE0NTc5Nzc2NjR9.kRoUEm3u3V0JOERuN1JS7er-cy0WF7VyOyZz4UBYhvypkAGESbzyRanEuxeG9lE1xil1rPyzY7_dfAeqPmLnQdduUUoRRiPbWLIkwFddNkenxLtVC1oZrvf4CuvUveEqtKc6GRr2262VMhQC2o3NDZJJfAtZiD6C2G_iadO70sDEMIK2EONdx7e6OFereYMabmMPQiusIoanIOQ9YrIafzi1NWWr55Up0v_aFHvHFTIOuCSpejbub-WjdIfTUgSd-quxEyI8emR57MIGyMP9lTfIZJiowoSHb8rG5f-UuX8Fcy5FARRTwgoH2MfXPZpOwUnggObPN9TsKsAe1W1rtA";
            string url = "https://app.vssps.visualstudio.com/oauth2/token";
            HttpWebResponse response = null;

            // Create the Assertion String
            string assertion = buildAssertion(applicationName, keyMoniker, applicationID, clientString,
              username, secret);

            try
            {
                // Create the data to send
                StringBuilder data = new StringBuilder();
                data.Append("grant_type=" + Uri.EscapeDataString(grantType));
                data.Append("&assertion_type=" + Uri.EscapeDataString(assertionType));
                data.Append("&assertion=" + Uri.EscapeDataString(assertion));

                // Create a byte array of the data to be sent
                byte[] byteArray = Encoding.UTF8.GetBytes(data.ToString());

                // Setup the Request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                // Write data
                Stream postStream = request.GetRequestStream();
                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();

                // Send Request & Get Response
                response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    // Get the Response Stream
                    string json = reader.ReadToEnd();
                    Console.WriteLine(json);

                    // Retrieve the Access Token
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    Dictionary<string, object> x = (Dictionary<string, object>)ser.DeserializeObject(json);
                    string accessToken = x["access_token"].ToString();
                }
            }
            catch (WebException e)
            {
                // This exception will be raised if the server didn't return 200 - OK
                // Retrieve more information about the error
                if (e.Response != null)
                {
                    using (HttpWebResponse err = (HttpWebResponse)e.Response)
                    {
                        Console.WriteLine("The server returned '{0}' with the status code '{1} ({2:d})'.",
                          err.StatusDescription, err.StatusCode, err.StatusCode);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }

            Console.ReadLine();
        }

        #region Helper Functions

        /// <summary>
        /// Builds a signed OAuth 2.0 assertion.
        /// </summary>
        /// <param name="applicationName">The name of the application brokering the token request.</param>
        /// <param name="keyMoniker">GUID that identifies the educational partner (Public Key).</param>
        /// <param name="applicationID">GUID that uniquely identifies the client.</param> 
        /// <param name="clientString">The string value that uniquely identifies the campus/school.</param>
        /// <param name="username">The LearningStudio user's external identifier.</param>
        /// <param name="secret">Alphanumeric string used to validate the identity of the education partner (Private Key).</param>
        /// <returns>A signed, encoded, and fully constructed assertion.</returns>
        private static string buildAssertion(
          string applicationName,
          string keyMoniker,
          string applicationID,
          string clientString,
          string username,
          string secret
        )
        {
            // Get the UTC Date Timestamp
            string timestamp = DateTime.UtcNow.ToString("s") + "Z";

            // Setup the Assertion String
            string assertion = String.Format("{0}|{1}|{2}|{3}|{4}|{5}", applicationName, keyMoniker, applicationID,
              clientString, username, timestamp);

            // Generate the CMAC used for Assertion Security
            string cmac = generateCmac(secret, assertion);

            // Add the CMAC to the Assertion String
            string assertionFinal = String.Format("{0}|{1}", assertion, cmac);
            Console.WriteLine("Assertion String = " + assertion + "\r\n");

            return assertionFinal;
        }

        /// <summary>
        /// Generates a HEX-encoded CMAC-AES digest.
        /// </summary>
        /// <param name="key">The secret key used to sign the data.</param>
        /// <param name="msg">The data to be signed.</param>
        /// <returns>A CMAC-AES digest.</returns>
        private static string generateCmac(string key, string msg)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] data = Encoding.UTF8.GetBytes(msg);

            CMac macProvider = new CMac(new AesFastEngine());
            macProvider.Init(new KeyParameter(keyBytes));
            macProvider.Reset();

            macProvider.BlockUpdate(data, 0, data.Length);
            byte[] output = new byte[macProvider.GetMacSize()];
            macProvider.DoFinal(output, 0);

            return Encoding.ASCII.GetString(Hex.Encode(output));
        }

        #endregion // Helper Functions
    }

}
