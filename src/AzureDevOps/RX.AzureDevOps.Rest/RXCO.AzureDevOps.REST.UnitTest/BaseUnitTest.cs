using Microsoft.VisualStudio.TestTools.UnitTesting;
using RXCO.AzureDevOps.REST.Base;

namespace RXCO.AzureDevOps.REST.UnitTest
{
    [TestClass]
    public class BaseUnitTest
    {
        [TestMethod]
        public void APISemanticVersionWrapperCompareEqual()
        {
            APISemanticVersionWrapper versionA = new APISemanticVersionWrapper(1, 1);
            APISemanticVersionWrapper versionB = new APISemanticVersionWrapper(1, 1);
            Assert.AreEqual<APISemanticVersionWrapper>(versionA, versionB);
            Assert.IsTrue(versionA == versionB);
        }

        [TestMethod]
        public void APISemanticVersionWrapperCompareLessThan()
        {
            // check for less than and greater than
            APISemanticVersionWrapper versionA = new APISemanticVersionWrapper(1, 0);
            APISemanticVersionWrapper versionB = new APISemanticVersionWrapper(1, 1);
            Assert.AreNotEqual<APISemanticVersionWrapper>(versionA, versionB);
            Assert.IsTrue(versionA < versionB);
            Assert.IsTrue(versionB > versionA);
        }
    }
}
