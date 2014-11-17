using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD;

namespace ETD_UnitTest
{
    [TestClass]
    public class TimerTest
    {
        [TestMethod]
        public void StartTimerTest()
        {
            Timer.StartTimer();
            Assert.AreEqual(Timer.currentIntervention, 1);
            Assert.AreEqual(Timer.currentTeam, 0);
        }
    }


}
