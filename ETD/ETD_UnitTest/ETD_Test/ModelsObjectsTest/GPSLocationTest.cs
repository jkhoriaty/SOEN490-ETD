using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;
using ETD.Services.Database;
using ETD.ViewsPresenters;

namespace ETD_UnitTest
{
    [TestClass]
    public class GPSLocationTest
    {
        [TestMethod]
        public void GPSCreationTest()
        {
			GPSLocation gps = new GPSLocation("myID", 30.25, 12.42);
			Assert.AreEqual(30.25, gps.getLattitude());
			Assert.AreEqual(12.42, gps.getLongitude());
        }

		[TestMethod]
		public void GPSCreation2Test()
		{
			GPSLocation gps = new GPSLocation(10.12, 21.21, 51.12, 18.192);
			Assert.AreEqual(10.12, gps.getLattitude());
			Assert.AreEqual(21.21, gps.getLongitude());
			Assert.AreEqual(51.12, gps.getX());
			Assert.AreEqual(18.192, gps.getY());
		}

		[TestMethod]
		public void GPSGetGPSLocationFromIDTest()
		{
			GPSLocation gps = new GPSLocation("myID2", 30.25, 12.42);
			Assert.AreEqual(gps, GPSLocation.getGPSLocationFromID("myID2"));
		}

		[TestMethod]
		public void GPSGetDictionaryTest()
		{
			GPSLocation gps = new GPSLocation("myID3", 30.30, 30.12);
			Assert.IsTrue(GPSLocation.getDictionary().Count >= 1);
		}

		[TestMethod]
		public void GPSGetLattitude()
		{
			GPSLocation gps = new GPSLocation("myID4", 1.21, 2.12);
			Assert.AreEqual(1.21, gps.getLattitude());
		}

		[TestMethod]
		public void GPSGetLongitude()
		{
			GPSLocation gps = new GPSLocation("myID5", 1.21, 2.12);
			Assert.AreEqual(2.12, gps.getLongitude());
		}

		[TestMethod]
		public void GPSGetX()
		{
			GPSLocation gps = new GPSLocation(10.12, 21.21, 51.12, 18.192);
			Assert.AreEqual(51.12, gps.getX());
		}

		[TestMethod]
		public void GPSGetY()
		{
			GPSLocation gps = new GPSLocation(10.12, 21.21, 51.12, 18.192);
			Assert.AreEqual(18.192, gps.getY());
		}
    }
}
