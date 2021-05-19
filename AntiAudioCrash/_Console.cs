using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAudioCrash
{
    class _Console
    {
        public enum Colors
        {
            Red,
            Blue,
            Orange,
            Black,
            White,
            Green,
            Pink,
            Cyan,
            LimeGreen,
            DarkRed,
            DarkGreen,
            DarkOrange,
            DarkBlue,
            Default
        }

        public static ConsoleColor getColor(Colors color)
        {
            if (color == Colors.Default)
            {
                return ConsoleColor.White;
            }
            else
            {
                switch (color)
                {
                    case Colors.Red:
                        return ConsoleColor.Red;
                    case Colors.Green:
                        return ConsoleColor.Green;
                    case Colors.Blue:
                        return ConsoleColor.Blue;
                    case Colors.Cyan:
                        return ConsoleColor.Cyan;
                    case Colors.Black:
                        return ConsoleColor.Black;
                    default:
                        return ConsoleColor.White;
                }
            }
        }

        [Obsolete]
        public static void Log(Colors color, string message, bool timeStamp = false) => Msg(color, message, timeStamp);
        public static void Write(Colors color, string message, bool timeStamp = false) => Msg(color, message, timeStamp);
        public static void Msg(Colors color, string message, bool timeStamp = false)
        {
            if (timeStamp)
                message = "" + message;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("AntiAudioCrash");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = getColor(color);
            Console.Write(message + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
