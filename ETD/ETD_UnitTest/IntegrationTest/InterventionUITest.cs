using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters;
using ETD.Models.Objects;
using ETD.CustomObjects.CustomUIObjects;

namespace ETD_UnitTest.IntegrationTest
{
    [TestClass]
    public class InterventionUITest
    {
        [TestMethod]
        public void CreateInterventionPinTest()
        {
            MainWindow window = new MainWindow();
            MapSectionPage mapSectionPage = new MapSectionPage(window);
            Intervention iv = new Intervention();
            mapSectionPage.Update();
            bool check = false;
            foreach (Pin pin in Pin.getPinList())
            {
                if (pin.getRelatedObject() == iv)
                {
                    check = true;
                }
            }

            Assert.IsTrue(check);
        }
    }
}
