using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.Models.Objects;
using ETD.Models.Services;

namespace ETD_UnitTest
{
    [TestClass]
    public class WordTest
    {
        [TestMethod]
        public void WordCreationTest()
        {
            String testFr = "french";
            String testEn = "english";
            Word testWord = new Word(testFr, testEn);
            Assert.AreEqual(testWord.getEnglish(), "english");
            Assert.AreEqual(testWord.getFrench(), "french");
            testWord.setEnglish("english2");
            Assert.AreEqual(testWord.getEnglish(), "english2");

        }
    }
}
