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
            DateTime dt = DateTime.Now;
            InterventionAdditionalInfo ai = new InterventionAdditionalInfo("info", DateTime.Now);
            Assert.AreEqual(ai.getInformation(), "info");
            Assert.AreEqual(ai.getTimestamp(), dt);
        }

    }
}
