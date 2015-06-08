namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;
    using WeebreeOpen.VisualStudioServerLib.Test.Properties;

    public class VsoTestBase
    {
        protected VsoClient vsoClient;

        [TestInitialize]
        public void Initialize()
        {
            this.vsoClient = new VsoClient(Settings.Default.VsoTenantName, new NetworkCredential(Settings.Default.UserName, Settings.Default.Password));
            this.OnInitialize(vsoClient);
        }

        protected virtual void OnInitialize(VsoClient vsoClient)
        {
        }
    }
}