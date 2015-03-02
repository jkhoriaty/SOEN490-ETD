using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;
using System.Collections.Generic;

namespace ETD_UnitTest
{
    [TestClass]
    public class RequestTest
    {
        [TestMethod]
        public void RequestCreation()
        {
            List<Request> requestList = Request.getRequestList();
            requestList.Clear();
            Request rq = new Request("client", "request", "handle", DateTime.Now, DateTime.Now);
            Request rp = new Request("client", "request", "handle", DateTime.Now, DateTime.Now);
            Assert.AreEqual(requestList.Count, 2);
        }

        [TestMethod]
        public void RequestFollowUpInfoTest()
        {
            Request[] fInfo;
            List<Request> requestList = Request.getRequestList();
            requestList.Clear();
            Request rq = new Request("client", "request", "handle", DateTime.Now, DateTime.Now);
            rq.SetFollowUpInfo(1, rq);
            fInfo = rq.getFollowUpInfo();
            Assert.AreEqual(fInfo[1], rq);
 
        }
    }
}
