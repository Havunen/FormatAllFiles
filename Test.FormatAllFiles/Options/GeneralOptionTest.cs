﻿using System;
using FormatAllFiles.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.FormatAllFiles.Options
{
    /// <summary>
    /// <see cref="GeneralOption"/>のテストクラスです。
    /// </summary>
    [TestClass]
    public class GeneralOptionTest
    {
        [TestMethod]
        public void CreateFileFilterTest()
        {
            var filter = CreateFileFilter(string.Empty);
            Assert.IsTrue(filter("file.txt"));
            Assert.IsTrue(filter(string.Empty));

            filter = CreateFileFilter("*.txt");
            Assert.IsTrue(filter("file.txt"));
            Assert.IsFalse(filter("file.cs"));
            Assert.IsFalse(filter(string.Empty));

            filter = CreateFileFilter("*.txt;*.cs");
            Assert.IsTrue(filter("file.txt"));
            Assert.IsTrue(filter("file.cs"));
            Assert.IsFalse(filter(string.Empty));
        }

        [TestMethod]
        public void CreateHierarchyFilterTest()
        {
            var filter = CreateHierarchyFilter(true);
            Assert.IsTrue(filter("file.txt"));
            Assert.IsFalse(filter("file.tt"));
            Assert.IsFalse(filter("file.txt.tt"));
            Assert.IsTrue(filter(string.Empty));

            filter = CreateHierarchyFilter(false);
            Assert.IsTrue(filter("file.txt"));
            Assert.IsTrue(filter("file.tt"));
            Assert.IsTrue(filter(string.Empty));
        }

        /// <summary>
        /// テストで使用する FileFilter を作成します。
        /// </summary>
        private Func<string, bool> CreateFileFilter(string inclusionPattern)
        {
            var option = new GeneralOption
            {
                InclusionFilePattern = inclusionPattern
            };

            return option.CreateFileFilter();
        }

        /// <summary>
        /// テストで使用する HierarchyFilter を作成します。
        /// </summary>
        private Func<string, bool> CreateHierarchyFilter(bool excludeGeneratedT4)
        {
            var option = new GeneralOption
            {
                ExcludeGeneratedT4 = excludeGeneratedT4
            };

            return option.CreateHierarchyFilter();
        }
    }
}
