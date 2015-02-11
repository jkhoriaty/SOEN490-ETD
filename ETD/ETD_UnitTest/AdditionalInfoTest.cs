using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;

namespace ETD_UnitTest
{
    [TestClass]
    public class AdditionalInfoTest
    {
        [TestMethod]
        public void AdditonalInfoCreationTest()
        {
            AdditionalInfo ai = new AdditionalInfo("Test");
            Assert.AreEqual(ai.getAdditionalinfoName(),"Test");
            ai.setAISize(5);
            int size = 5;
            Assert.AreEqual(ai.getAISize(), size);

        }
    }
}
