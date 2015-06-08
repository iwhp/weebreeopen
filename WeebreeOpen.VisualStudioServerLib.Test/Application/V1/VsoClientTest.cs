namespace WeebreeOpen.VisualStudioServerLib.Test.Application.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioServerLib.Application.V1;
    using WeebreeOpen.VisualStudioServerLib.Properties;
    using WeebreeOpen.VisualStudioServerLib.Test;

    [TestClass]
    public class VsoClientTest
    {
        [TestMethod]
        public void VsoClient_Constructor()
        {
            // Assign

            // Act
            VsoClient sut = new VsoClient(TestData.VsoTenantName, TestData.UserCredential);

            IVsoSimple result1 = sut.GetService<IVsoSimple>();

            //IVsoBuild result2 = sut.GetService<IVsoBuild>();
            //var result2A = result2.GetBuilds("bvd.li.web");

            IVsoWit result3 = sut.GetService<IVsoWit>();
            var result3A = result3.GetIterationNode("bvd.li.web", 2);
            var result3B = result3.GetQueries("bvd.li.web", depth: 2);

            var result3C = result3.RunLinkQuery("Bvd.Li.Web", result3B.Result.Items[0].Children[2]);
            var result3D = result3.RunFlatQuery("Bvd.Li.Web", result3B.Result.Items[0].Children[1].Children[2]);

            // Assert
        }

        [TestMethod]
        public void VsoClient_RunFlatQuery()
        {
            // Assign

            // Act
            VsoClient sut = new VsoClient(TestData.VsoTenantName, TestData.UserCredential);

            IVsoWit sut2 = sut.GetService<IVsoWit>();
            var result2 = sut2.RunFlatQuery("bvd.li.web", Resources.WorkItem1);

            // Assert

            // Log
            List<string> fields = new List<string>();
            foreach (var item in result2.Result.Columns)
            {
                Console.WriteLine(item.ReferenceName);
                fields.Add(item.ReferenceName);
            }

            List<int> ids = new List<int>();
            foreach (var item in result2.Result.WorkItems)
            {
                Console.WriteLine(item.Id);
                ids.Add(item.Id);
            }

            if (ids.Count > 0)
            {
                var wits = sut2.GetWorkItems(ids.Distinct().ToArray(), fields: fields.Distinct().ToArray());
                foreach (var item in wits.Result.Items)
                {
                    Console.WriteLine("---");
                    foreach (var field in item.Fields)
                    {
                        Console.WriteLine(field.Key + ":" + field.Value);
                    }
                }
            }
        }

        [TestMethod]
        public void VsoClient_RunLinkQuery()
        {
            // Assign

            // Act
            VsoClient sut = new VsoClient(TestData.VsoTenantName, TestData.UserCredential);

            IVsoWit sut2 = sut.GetService<IVsoWit>();
            var result2 = sut2.RunLinkQuery("bvd.li.web", Resources.WorkItemLinks1);

            // Assert

            // Log
            List<int> ids = new List<int>();
            foreach (var item in result2.Result.Relations)
            {
                Console.WriteLine(item.Target.Id + " " + item.Target.Url);
                ids.Add(item.Target.Id);
            }

            var wits = sut2.GetWorkItems(ids.Distinct().ToArray());
            foreach (var item in wits.Result.Items)
            {
                Console.WriteLine("---");
                foreach (var field in item.Fields)
                {
                    Console.WriteLine(field.Key + ":" + field.Value);
                }
            }
        }
    }
}
