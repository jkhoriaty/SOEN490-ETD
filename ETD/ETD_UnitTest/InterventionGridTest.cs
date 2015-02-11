using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.ViewsPresenters.MapSection;
using ETD.Models.Grids;
using ETD.ViewsPresenters;

namespace ETD_UnitTest
{
    [TestClass]
    public class InterventionGridTest
    {
        [TestMethod]
        public void InterventionGridCreation()
        {
            MainWindow mainwindow = new MainWindow();
            int interNumber = 5;
            MapSectionPage mapSection = new MapSectionPage(mainwindow);
            int size = 20;
            InterventionGrid ig = new InterventionGrid(interNumber,mapSection,size);
            Assert.AreEqual(ig.Name, "Intervention_5");
            Assert.AreEqual(ig.Width, size);
            Assert.AreEqual(ig.Height, size);
            Assert.AreEqual(ig.Tag, "intervention");
        }
    }
}
