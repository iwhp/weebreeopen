namespace WeebreeOpen.Office365Api.Infrastructure.Office365Api.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using WeebreeOpen.Office365Api.Domain.Mail;
    using WeebreeOpen.Office365Api.Infrastructure.Office365Api.Mapping;

    public class MailRepository
    {
        public MailRepository(Office365Context office365Context)
        {
            this.Office365Context = office365Context;
        }

        public Office365Context Office365Context { get; private set; }

        public Folder GetFolderInbox()
        {
            //
            var authClearText = string.Format("{0}:{1}", Office365Context.UserName, Office365Context.Password);
            var authEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(authClearText));
            var authHeaderValue = "Basic " + authEncoded;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", authHeaderValue);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            HttpResponseMessage response = httpClient.GetAsync("https://outlook.office365.com/api/v1.0/me/folders/Inbox").Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("API Error: " + response.StatusCode);
            }

            string json = response.Content.ReadAsStringAsync().Result;

            Folder folder = Newtonsoft.Json.JsonConvert.DeserializeObject<Folder>(json);

            return folder;
        }

        public List<Folder> GetFolders()
        {
            List<Folder> folders = new List<Folder>();

            //
            var authClearText = string.Format("{0}:{1}", Office365Context.UserName, Office365Context.Password);
            var authEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(authClearText));
            var authHeaderValue = "Basic " + authEncoded;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", authHeaderValue);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            HttpResponseMessage response = httpClient.GetAsync("https://outlook.office365.com/api/v1.0/me/folders/Inbox/childfolders").Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("API Error: " + response.StatusCode);
            }

            string json = response.Content.ReadAsStringAsync().Result;

            FoldersMapping rootobject = Newtonsoft.Json.JsonConvert.DeserializeObject<FoldersMapping>(json);

            foreach (var item in rootobject.value)
            {
                folders.Add(item);
            }

            return folders;
        }

        public void GetMails()
        {
            //
            var authClearText = string.Format("{0}:{1}", Office365Context.UserName, Office365Context.Password);
            var authEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(authClearText));
            var authHeaderValue = "Basic " + authEncoded;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", authHeaderValue);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            HttpResponseMessage response = httpClient.GetAsync("https://outlook.office365.com/api/v1.0/me/folders/Inbox/messages").Result;
            string json = response.Content.ReadAsStringAsync().Result;

            MessagesMapping rootobject = Newtonsoft.Json.JsonConvert.DeserializeObject<MessagesMapping>(json);

            foreach (var item in rootobject.value)
            {
                //Console.WriteLine(item.Subject);
            }
        }
    }
}