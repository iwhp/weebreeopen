namespace WeebreeOpen.VisualStudioServerLib.Test.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Infrastructure.Test;
    using WeebreeOpen.VisualStudioServerLib.Test;

    [TestClass]
    public class VsoClient2Test
    {
        [TestMethod]
        public async Task VsoClient2_Constructor()
        {
            // Assign
            VsoClient2 sut = new VsoClient2(TestData.VsoTenantName, TestData.UserName, TestData.Password);

            // Act
            sut.RunSample();

            // Assert
            Assert.IsNotNull(sut);
        }
    }
}
