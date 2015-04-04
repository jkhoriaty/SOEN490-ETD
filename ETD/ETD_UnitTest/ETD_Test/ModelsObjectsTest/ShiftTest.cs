using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;
using ETD.Services.Database;

namespace ETD_UnitTest
{
    [TestClass]
    public class ShiftTest
    {
        [TestMethod]
        public void ShiftCreationTest()
        {
			Shift shift = new Shift("A", "sector1", 10);
			Assert.AreEqual("A", shift.getTeamName());
			Assert.AreEqual("sector1", shift.getSector());
			Assert.AreEqual(10, shift.getShiftDuration());
        }

		[TestMethod]
		public void ShiftListTest()
		{
			Shift shift = new Shift("A", "sector1", 10);
			Assert.AreEqual(2, shift.getShiftsList().Count);
		}

		[TestMethod]
		public void ShiftInfoTest()
		{
			Shift shift = new Shift("A", "sector1", 10);
			Shift shift2 = new Shift("B", "sector2", 5);
			shift.setShiftInfo(0, shift2);
			Assert.AreEqual(shift2, shift.getShiftInfo()[0]);
		}

		[TestMethod]
		public void ShiftGetTeamNameTest()
		{
			Shift shift = new Shift("A", "sector1", 10);
			Assert.AreEqual("A", shift.getTeamName());
		}

		[TestMethod]
		public void ShiftGetSector()
		{
			Shift shift = new Shift("A", "sector1", 10);
			Assert.AreEqual("sector1", shift.getSector());
		}

		[TestMethod]
		public void ShiftGetDuration()
		{
			Shift shift = new Shift("A", "sector1", 10);
			Assert.AreEqual(10, shift.getShiftDuration());
		}
    }
}
