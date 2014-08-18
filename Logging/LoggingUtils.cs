using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using Manufacturing.Framework.Utility;

namespace Manufacturing.Framework.Logging
{
    public class LoggingUtils
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void InitializeLogging(bool turnOnConsoleLogger = false)
        {
#if(DEBUG)
            //log4net.Util.LogLog.InternalDebugging = true;
#endif

            var path = AssemblyUtils.GetAssemblyDirectory();
            var log4NetConfigPath = Path.Combine(path, "log4net.Config");
            XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(log4NetConfigPath));

            Log.Info("Logging Initialized");

            if (turnOnConsoleLogger)
            {
                var consoleAppender = new ColoredConsoleAppender
                {
                    Layout = new PatternLayout()
                };
                consoleAppender.ActivateOptions();
                BasicConfigurator.Configure(consoleAppender);
            }
        }

        /// <summary>
        ///     Sets up Logging programmically for scenarios such as unit tests. It logs
        ///     to the trace output, and also to a local UDP port to watch with a tool
        ///     such as Log2Console (http://log2console.codeplex.com/)
        /// </summary>
        public static void InitLocalLogging()
        {
            var traceAppender = new TraceAppender { Layout = new PatternLayout() };

            var udpAppender = new UdpAppender
            {
                RemoteAddress = IPAddress.Loopback,
                RemotePort = 8080,
                Name = "UDPAppender",
                Encoding = new ASCIIEncoding(),
                Threshold = Level.Debug,
                Layout = new XmlLayoutSchemaLog4j()
            };
            udpAppender.ActivateOptions();

            BasicConfigurator.Configure(traceAppender, udpAppender);

            Log.Info("UDP & Trace Logging Enabled");
        }
    }
}
