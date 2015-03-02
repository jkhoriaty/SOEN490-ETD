using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters.MapSection.PinManagement;
using ETD.ViewsPresenters;
using ETD.Models.Objects;
namespace ETD_UnitTest
{
    [TestClass]
    public class ResourceUITest
    {
        [TestMethod]
      
		public void CreateTeamPinTest()
		{
			MainWindow window = new MainWindow();
			MapSectionPage mapSectionPage = new MapSectionPage(window);
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
    }
}
