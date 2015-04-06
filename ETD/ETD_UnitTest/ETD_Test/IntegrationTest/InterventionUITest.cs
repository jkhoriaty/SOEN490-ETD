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
    { /*
        [TestMethod]
        public void CreateInterventionPinTest()
        {
            MainWindow window = new MainWindow();
			AdditionalInfoPage adInfo = new AdditionalInfoPage(window);
            MapSectionPage mapSectionPage = new MapSectionPage(window, adInfo);
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

        [TestMethod]
        public void DeleteInterventionPinTest()
        {
            MainWindow window = new MainWindow();
			AdditionalInfoPage adInfo = new AdditionalInfoPage(window);
            MapSectionPage mapSectionPage = new MapSectionPage(window, adInfo);
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

            iv.Completed();
            bool exist = false;
            foreach (Pin pin in Pin.getPinList())
            {
                if (pin.getRelatedObject() == iv)
                {
                    check = true;
                }
            }

            Assert.IsFalse(exist);
 
        } */
    }
}
