using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;

namespace ETD_UnitTest
{
    [TestClass]
    public class InterventionAdditionalInfoTest
    {
        [TestMethod]
        public void InterventionAICreationTest()
        {
            InterventionAdditionalInfo ai = new InterventionAdditionalInfo("info", DateTime.Now);
            DateTime dt = DateTime.Now;
            Assert.AreEqual(ai.getInformation(), "info");
            Assert.AreEqual(ai.getTimestamp(), dt);
        }

    }
}
