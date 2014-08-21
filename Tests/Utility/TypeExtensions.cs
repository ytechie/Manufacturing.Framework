using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.Utility
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void ExcludeTypes_VerifyTypesExcluded()
        {
            var typeList = new List<Type> {typeof (string), typeof (int)};
            var typeList2 = new List<Type> {typeof (int), typeof (DateTime)};

            var outTypes = typeList.ExcludeTypes(typeList2).ToList();
            Assert.AreEqual(1, outTypes.Count);
            Assert.AreEqual(typeof(string), outTypes[0]);
        }
    }
}
