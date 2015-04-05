using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters;
using ETD.Models.Objects;
using ETD.CustomObjects.CustomUIObjects;

namespace ETD_UnitTest
{
    [TestClass]
    public class ResourceUITest
    { /*
        [TestMethod]      
		public void CreateTeamPinTest()
		{
			MainWindow window = new MainWindow();
			AdditionalInfoPage adInfo = new AdditionalInfoPage(window);
			MapSectionPage mapSectionPage = new MapSectionPage(window, adInfo);
			Team a = new Team("A");
			mapSectionPage.Update();
			
			bool check = false;
			foreach(Pin pin in Pin.getPinList())
			{
				if(pin.getRelatedObject() == a)
				{
					check = true;
				}
			}
			Assert.IsTrue(check);
		        
        }

        [TestMethod]
        public void DeleteTeamPinTest()
        {
            MainWindow window = new MainWindow();
			AdditionalInfoPage adInfo = new AdditionalInfoPage(window);
            MapSectionPage mapSectionPage = new MapSectionPage(window, adInfo);
            Team a = new Team("A");
            mapSectionPage.Update();

            bool check = false;
            foreach (Pin pin in Pin.getPinList())
            {
                if (pin.getRelatedObject() == a)
                {
                    check = true;
                }
            }
            Assert.IsTrue(check);

            Team.DeleteTeam(a);
            bool exist = false;
            foreach (Pin pin in Pin.getPinList())
            {
                if (pin.getRelatedObject() == a)
                {
                    check = true;
                }
            }

            Assert.IsFalse(exist);
 
        } */
    }
}
