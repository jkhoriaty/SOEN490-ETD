using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD;
using System.Diagnostics;

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

        [TestMethod]
        public void SetRemoveTimerTest()
        {
            Stopwatch a = new Stopwatch();
            Stopwatch b = new Stopwatch();
            Timer.SetTimer(3,a);
            Assert.AreEqual(Timer.timers.ContainsKey(1), true);
            Assert.AreEqual(Timer.timers.ContainsKey(3), false);
            Timer.SetTimer(3,b);
            Assert.AreEqual(Timer.timers.ContainsKey(2), true);
            Timer.SetTimer(3, a);
            Assert.AreEqual(Timer.timers.ContainsKey(3), true);
            Assert.AreEqual(Timer.currentIntervention,4);
            Timer.RemoveTimer(2);
            Assert.AreEqual(Timer.timers.ContainsKey(2), false);
        }


    }


}
