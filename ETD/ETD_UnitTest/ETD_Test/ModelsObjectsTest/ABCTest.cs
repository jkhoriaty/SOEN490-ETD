using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;
using ETD.Services.Database;
using ETD.ViewsPresenters;

namespace ETD_UnitTest
{
    [TestClass]
    public class ABCTest
    {
        [TestMethod]
        public void DefaultABCCreationTest()
        {
            ABC abc = new ABC();
            Assert.AreEqual(abc.getConsciousness(), "notSet");
            Assert.AreEqual(abc.getDisoriented(), false);
            Assert.AreEqual(abc.getAirways(),"notSet");
            Assert.AreEqual(abc.getBreathing(),"notSet");
            Assert.AreEqual(abc.getBreathingFrequency(),-1);
            Assert.AreEqual(abc.getCirculation(),"notSet");
            Assert.AreEqual(abc.getCirculationFrequency(),-1); 
        }

        [TestMethod]
        public void ABCCreationTest()
        {
            ABC abc = new ABC(2, "cons", true, "air", "breath", 10, "circ", 15);
            Assert.AreEqual(abc.getConsciousness(), "cons");
            Assert.AreEqual(abc.getDisoriented(), true);
            Assert.AreEqual(abc.getAirways(), "air");
            Assert.AreEqual(abc.getBreathing(), "breath");
            Assert.AreEqual(abc.getBreathingFrequency(), 10);
            Assert.AreEqual(abc.getCirculation(), "circ");
            Assert.AreEqual(abc.getCirculationFrequency(), 15);
        }

        [TestMethod]
        public void getterSetterTest()
        {
            ABC abc = new ABC();
            abc.setConsciousness("set");
            Assert.AreEqual(abc.getConsciousness(), "set");
            abc.setDisoriented(true);
            Assert.AreEqual(abc.getDisoriented(), true);
            abc.setAirways("set");
            Assert.AreEqual(abc.getAirways(), "set");
            abc.setBreathing("set");
            Assert.AreEqual(abc.getBreathing(), "set");
            abc.setBreathingFrequency(5);
            Assert.AreEqual(abc.getBreathingFrequency(), 5);
            abc.setCirculation("set");
            Assert.AreEqual(abc.getCirculation(), "set");
            abc.setCirculationFrequency(55);
            Assert.AreEqual(abc.getCirculationFrequency(), 55);
 
        }
    }
}
