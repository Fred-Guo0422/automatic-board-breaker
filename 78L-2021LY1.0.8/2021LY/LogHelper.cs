using _2021LY.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "config", Watch = true)]

namespace _2021LY
{
    class LogHelper
    {
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");
        public static readonly log4net.ILog logdebug = log4net.LogManager.GetLogger("logdebug");
        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        /// 操作日志  记录每个动作流程的日志
        /// </summary>
        public static void WriteInfoLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                BusinessForm.getForm().Add_Info_ToText(info);
                loginfo.Info(info);
            }
        }

        /// <summary>
        /// 后台日志  后台调试以及无所谓的异常日志   
        /// </summary>
        public static void WriteErrorLog(string info, Exception se=null)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }
        /// <summary>
        /// 报警日志   即前台弹框提示的日志
        /// </summary>
        public static void WriteDebugLog(string info)
        {
            if (logdebug.IsDebugEnabled)
            {
                logdebug.Debug(info);
            }
        }
    }
}
