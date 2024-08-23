using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    public static class Logger
    {
        public enum Type
        {
            Incoming,
            Outgoing,
            Normal,
            Warning,
            Error,
            Debug,
            Ping
        };

        private static ConsoleColor[] TypeColor = new ConsoleColor[7]
        {
            ConsoleColor.Blue,
            ConsoleColor.Green,
            ConsoleColor.Gray,
            ConsoleColor.Yellow,
            ConsoleColor.Red,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta
        };

        public static void Log(Type type, string msg, Exception ex = null)
        {
            string logMessage = $"[{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}] {msg}";

            ConsoleColor cl = TypeColor[(int)type];
            Console.ForegroundColor = cl;
            Console.WriteLine(logMessage);
            Console.ResetColor();
        }
    }
}
