using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Grids;
using ETD.Models.Objects;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters;

namespace ETD_UnitTest
{
    [TestClass]
    public class AddionalInfoGridTest
    {
        [TestMethod]
        public void AddtionalInfoGridCreation()
        {
            MainWindow mainwindow = new MainWindow();
            String test = "test";
            AdditionalInfo ai = new AdditionalInfo(test);
            AdditionalInfoPage aiPage = new AdditionalInfoPage(mainwindow);
            //int size = 10;
            //AdditionalInfoGrid aig = new AdditionalInfoGrid(ai, aiPage, size);
            //Assert.AreEqual(aig.gr, ai);
            //Assert.AreEqual(aig.Tag, "additionalinfo");

        }
    }
}
