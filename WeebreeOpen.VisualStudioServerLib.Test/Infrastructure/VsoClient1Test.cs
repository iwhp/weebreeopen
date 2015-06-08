namespace WeebreeOpen.VisualStudioServerLib.Test.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Infrastructure.Test;
    using WeebreeOpen.VisualStudioServerLib.Test;

    [TestClass]
    public class VsoClient1Test
    {
        [TestMethod]
        public async Task VsoClient1_Constructor()
        {
            // Assign

            // Act
            VsoClient1.TestConnection(TestData.VsoTenantName, TestData.UserName, TestData.Password);

            // Assert
        }
    }
}
