using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NWheels.Domains.ECommerce.Tests.Stacks
{
    [TestFixture]
    public class ManualStackTests
    {
        [Test]
        public void ManualTest_MongoBreezeNancy()
        {
            StackTestUtility.RunManualTest(
                StackName.MongoNancyBreeze, 
                runExecutable: StackTestUtility.TestBoardExeFilePath, 
                browseUrl: "http://localhost:8901/admin");
        }
    }
}
