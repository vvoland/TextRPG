using System;
using System.IO;

namespace TextRPG.Utils
{
    public class Logger
    {
        private static Logger Instance
        {
            get
            {
                if(_Instance == null)
                    _Instance = new Logger();
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }

        private static Logger _Instance = null;
        private StreamWriter Writer;

        public Logger()
        {
            Writer = new StreamWriter(new FileStream("game.log", FileMode.Create));
        }

        public static void Log(string format, params object[] args)
        {
            var logger = Instance.Writer;
            logger.Write(string.Format("[ {0} ] ", DateTime.Now.ToString("HH:mm:ss")));
            logger.WriteLine(string.Format(format, args));
            logger.Flush();
        }
    }
}