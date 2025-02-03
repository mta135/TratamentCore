using log4net;
using log4net.Repository;
using System.Reflection;

namespace Tratament.Web.LoggerSetup
{
    public class WriteLog
    {
        public static ILog Common { get; private set; }
        //public static ILog Web { get; private set; }
        //public static ILog DB { get; set; }
        //public static ILog Schedulers { get; private set; }
        //public static ILog MSign { get; private set; }


        public static void InitLoggers()
        {
            #region log4net Static
            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());

            var fileInfo = new FileInfo(@"log4net.config");

            log4net.Config.XmlConfigurator.Configure(repository, fileInfo);
            #endregion 

            Common = LogManager.GetLogger("CommonAppender");


            //Web = LogManager.GetLogger("WebAppender");
            //DB = LogManager.GetLogger("DatabaseAppender");
            //Schedulers = LogManager.GetLogger("SchedulersAppender");
            //MSign = LogManager.GetLogger("MSignAppender");

        }
    }
}
