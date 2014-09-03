using System.Reflection;
using Bootstrap.StructureMap;
using Manufacturing.Framework.Dto;
using Manufacturing.Framework.Utility;
using Microsoft.FluentMessaging;
using log4net;
using Manufacturing.Framework.Datasource;
using Manufacturing.Framework.Repository.Implementation;
using Microsoft.ConventionConfiguration;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Pipeline;

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
                    y.SingleImplementationsOfInterface().OnAddedPluginTypes(z => z.LifecycleIs(new TransientLifecycle()));

                    //y.ExcludeType<IMessageSender>();
                    y.ExcludeType<BlobRepository>();
                });

                x.For<IDatasourceRecordSerializer>().Use<DatasourceRecordSerializer>().AlwaysUnique();
                x.For<ITimer>().Use<ThreadingTimer>().AlwaysUnique();
            });

            
        }

        public void Register(System.ComponentModel.IContainer container)
        {
        }
    }
}
