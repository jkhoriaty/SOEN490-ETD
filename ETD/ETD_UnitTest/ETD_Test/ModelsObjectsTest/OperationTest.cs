using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;

namespace ETD_UnitTest
{
    [TestClass]
    public class OperationTest
    {
        [TestMethod]
        public void OperationCreationTest()
        {    
            DateTime dt = DateTime.Now;
            Operation op = new Operation("name", "acro", dt, dt, "dpName");
            Assert.AreEqual(op.getOperationName(), "name");
            Assert.AreEqual(op.getAcronym(), "acro");
            Assert.AreEqual(op.getShiftStart(), dt);
            Assert.AreEqual(op.getShiftEnd(), dt);
            Assert.AreEqual(op.getDispatcherName(), "dpName");
      
        }
    }
}
