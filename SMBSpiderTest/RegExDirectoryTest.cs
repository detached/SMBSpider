﻿//<auto-generated />
using SMBSpider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SMBSpiderTest
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "RegExDirectoryTest" und soll
    ///alle RegExDirectoryTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class RegExDirectoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Testkontext auf, der Informationen
        ///über und Funktionalität für den aktuellen Testlauf bietet, oder legt diesen fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        // 
        //Sie können beim Verfassen Ihrer Tests die folgenden zusätzlichen Attribute verwenden:
        //
        //Mit ClassInitialize führen Sie Code aus, bevor Sie den ersten Test in der Klasse ausführen.
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Mit ClassCleanup führen Sie Code aus, nachdem alle Tests in einer Klasse ausgeführt wurden.
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Ein Test für "GetFiles"
        ///</summary>
        [TestMethod()]
        public void GetFilesInclusiveTest()
        {
            string path = @"C:\temp\";
            string path2 = @"C:\temp\TestFolder\";
            string searchPattern = @"[a-zA-Z\:\\]*another[a-zA-Z\\]*";
            string[] expected = {@"C:\temp\TestFolder\anotherTest.txt"};
            List<string> actual;
            ISearchDirsAndFiles searcher = new InclusiveRegexSearcher(searchPattern);
            actual = new List<string>(searcher.GetFiles(path));
            Assert.IsTrue(actual.Count == 0);

            actual = new List<string>(searcher.GetFiles(path2));
            Assert.AreEqual(expected[0], actual[0]);
        }

        [TestMethod()]
        public void GetFilesExclusiveTest()
        {
            string path = @"C:\temp\";
            string path2 = @"C:\temp\TestFolder\";
            string searchPattern = @"[a-zA-Z\:\\]*another[a-zA-Z\\]*";
            string[] expected = { @"C:\temp\testFile.txt" };
            ISearchDirsAndFiles searcher = new ExclusiveRegexSearcher(searchPattern);
            List<string> actual = new List<string>(searcher.GetFiles(path));
            Assert.IsTrue(actual.Count == 1);
            Assert.AreEqual(expected[0], actual[0]);

            actual = new List<string>(searcher.GetFiles(path2));
            Assert.IsTrue(actual.Count == 0);
        }

        /// <summary>
        ///Ein Test für "GetDirectories"
        ///</summary>
        [TestMethod()]
        public void GetDirectoriesInclusiveTest()
        {
            string path = @"C:\temp\";
            string searchPattern = @"[a-zA-Z\:\\]*Folder[a-zA-Z\\]*";
            string[] expected = { @"C:\temp\TestFolder" };
            List<string> actual;
            ISearchDirsAndFiles searcher = new InclusiveRegexSearcher(searchPattern);
            actual = new List<string>(searcher.GetDirectories(path));
            Assert.IsTrue(actual.Count == 1);
            Assert.AreEqual(expected[0], actual[0]);
        }

        /// <summary>
        ///Ein Test für "GetDirectories"
        ///</summary>
        [TestMethod()]
        public void GetDirectoriesExclusiveTest()
        {
            string path = @"C:\temp\";
            string searchPattern = @"[a-zA-Z\:\\]*Folder[a-zA-Z\\]*";
            string[] expected = { @"C:\temp\TestDir" };

            ISearchDirsAndFiles searcher = new ExclusiveRegexSearcher(searchPattern);
            List<string> actual = new List<string>(searcher.GetDirectories(path));
            Assert.IsTrue(actual.Count == 1);
            Assert.AreEqual(expected[0], actual[0]);
        }
    }
}
