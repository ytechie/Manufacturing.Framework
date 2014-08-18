using System.Reflection;
using Bootstrap.StructureMap;
using Manufacturing.Framework.Dto;
using Microsoft.FluentMessaging;
using log4net;
using Manufacturing.Framework.Datasource;
using Manufacturing.Framework.Repository.Implementation;
using Microsoft.ConventionConfiguration;
using StructureMap;

namespace Manufacturing.Framework.Configuration
{
    public class FrameworkContainer : IStructureMapRegistration
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Register(IContainer container)
        {
            ConfigurationLoader.LoadConfigurations(container, ".\\Configuration\\", "{0}Configuration");

            container.Configure(x =>
            {
                x.Scan(y =>
                {
                    y.TheCallingAssembly();
                    y.SingleImplementationsOfInterface().OnAddedPluginTypes(z => z.LifecycleIs(InstanceScope.Unique));

                    y.ExcludeType<IMessageSender>();
                    y.ExcludeType<BlobRepository>();
                });

                x.For<ISerializer<DatasourceRecord>>().Use<DatasourceRecordSerializer>();
            });

            
        }

        public void Register(System.ComponentModel.IContainer container)
        {
        }
    }
}
