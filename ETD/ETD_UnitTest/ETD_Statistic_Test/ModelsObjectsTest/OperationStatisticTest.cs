using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD_Statistic.Model;

namespace ETD_UnitTest.ETD_Statistic_Test
{
    [TestClass]
    public class OperationStatisticTest
    {
        [TestMethod]
        public void OperationStatisticCreation()
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            String volunteer = "Test";
            String fn = "first";
            String veh = "veh";
            String part = "part";
            String organi = "organizer";
            String super = "sup";
            String events = "events";
            String dispatch = "dispatch";

            OperationStatistic os = new OperationStatistic(start,end,volunteer,fn,veh,part,organi,super,events,dispatch);

            Assert.AreEqual(start, os.getStartDate());
            Assert.AreEqual(end, os.getEndDate());
            Assert.AreEqual(volunteer, os.getVolunteerFollowup());
            Assert.AreEqual(fn, os.getFinance());
            Assert.AreEqual(veh, os.getVehicle());
            Assert.AreEqual(part, os.getParticularSituation());
            Assert.AreEqual(organi, os.getOrganizationFollowup());
            Assert.AreEqual(super, os.getSupervisorFollowup());
            Assert.AreEqual(events, os.getEventName());
            Assert.AreEqual(dispatch, os.getDispatcherName());
        }
    }
}
