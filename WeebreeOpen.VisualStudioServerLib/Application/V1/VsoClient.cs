namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;

    /// <summary>
    /// Main entry point for using VSO REST APIs
    /// </summary>
    public class VsoClient
    {
        #region Constructors

        public VsoClient(string accountName, NetworkCredential userCredential, string collectionName = DefaultCollection)
        {
            this.rootUrl = string.Format(AccountRootUrl, accountName, collectionName);
            this.userCredential = userCredential;
        }

        #endregion

        #region Properties

        private static readonly Dictionary<Type, Type> serviceMapping = new Dictionary<Type, Type>()
        {
            { typeof(IVsoGit), typeof(GitRestClient) },
            { typeof(IVsoBuild), typeof(BuildRestClient) },
            { typeof(IVsoProjectCollection), typeof(ProjectCollectionRestClient) },
            { typeof(IVsoProject), typeof(ProjectRestClient) },
            { typeof(IVsoTag), typeof(TagRestClient) },
            { typeof(IVsoVersionControl), typeof(VersionControlRestClient) },
            { typeof(IVsoWit), typeof(WitRestClient) },
            { typeof(IVsoSimple), typeof(SimpleRestClient) }
        };

        private const string AccountRootUrl = "https://{0}.visualstudio.com/{1}";

        private const string DefaultCollection = "DefaultCollection";

        private readonly string rootUrl;

        private readonly NetworkCredential userCredential;

        #endregion

        #region Methods

        public Task<T> Get<T>(string url)
        {
            return this.GetService<IVsoSimple>().Get<T>(url);
        }

        public T GetService<T>()
        {
            if (!serviceMapping.ContainsKey(typeof(T)))
            {
                throw new VsoException("Unknown service requested.");
            }

            return (T)Activator.CreateInstance(serviceMapping[typeof(T)], this.rootUrl, this.userCredential);
        }

        #endregion
    }
}