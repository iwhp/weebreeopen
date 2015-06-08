namespace WeebreeOpen.VisualStudioServerLib.Infrastructure.Test
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class VsoClient1
    {
        #region Methods

        public static async void TestConnection(string vsoTenantName, string username, string password)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                        );

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password)))
                        );

                    string url = string.Format("https://{0}.visualstudio.com/DefaultCollection/_apis/build/builds", vsoTenantName);

                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion
    }
}