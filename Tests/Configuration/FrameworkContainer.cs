using System.Reflection;
using Bootstrap;
using Bootstrap.StructureMap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Manufacturing.Framework.Configuration
{
    [TestClass]
    public class FrameworkContainerTests
    {
        [TestMethod]
        public void RegisterDefaultTypes_DontExplode()
        {
            Bootstrapper.With.StructureMap();
        }

        [TestMethod]
        public void Boostrapper_AssertIocConfigurationIsValid()
        {
            Bootstrapper
                .With.StructureMap()
                .IncludingOnly
                .Assembly(Assembly.GetAssembly(typeof(FrameworkContainer)))
                .Start();

            var container = (IContainer)Bootstrapper.Container;
            container.AssertConfigurationIsValid();
        }
    }
}
