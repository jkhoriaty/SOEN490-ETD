using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;

namespace ETD_UnitTest.ModelsObjectsTest
{
    [TestClass]
    public class ResourceTest
    {
        [TestMethod]
        public void TeamResourceCreationTest()
        {
            Team t = new Team("team");
            DateTime dt = DateTime.Now;
            Resource rs = new Resource(t);
            Assert.AreEqual(rs.getTeam(), t);
            Assert.AreEqual(rs.getIntervening(), true);
            Assert.AreEqual(rs.getMovingTime(), dt);
        }
        /*
        [TestMethod]
        public void ResourceCreationTest()
        {
            Team t = new Team("Ateam");
            DateTime dt = DateTime.Now;
            DateTime ft = dt.AddDays(1);
            ft.AddDays(1);
            Resource r = new Resource("rsName", t, true, dt, ft); 
            Assert.AreEqual(r.getResourceName(), "rsName");
            r.setResourceName("rs");
            Assert.AreEqual(r.getResourceName(), "rs");
            Assert.AreEqual(r.getTeam(),t);
            Assert.AreEqual(r.getIntervening(), true);
            r.setIntervening(false);
            Assert.AreEqual(r.getIntervening(), false);
            Assert.AreEqual(r.getMovingTime(), dt);
            Assert.AreEqual(r.getArrivalTime(), dt);
            DateTime dtf = DateTime.Now.AddDays(1);
            r.setMoving(dtf);
            r.setArrival(dtf);
            Assert.AreEqual(r.getMovingTime(), dtf);
            Assert.AreEqual(r.getArrivalTime(), dtf);

        }*/
    }
}
