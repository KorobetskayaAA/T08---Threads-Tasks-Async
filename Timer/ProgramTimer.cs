using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Timer
{
    static class Clock
    {
        static readonly string[] bigDigits = new[] {
            "####   ##########  ##### ## ############",
            "#  #  ##   #   ##  ##   #      ##  ##  #",
            "#  # # #  #  ## ############  # ########",
            "#  #   # #     #   #   ##  #  # #  #   #",
            "####   #########   #########  # ####  # "
        };
        static void PrintBigDigit(int x, int y, char digit)
        {
            string[] rows = bigDigits
                .Select(row => row.Substring(4 * (digit - '0'), 4))
                .ToArray();
            for (int i = 0; i < rows.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(rows[i]);
            }
        }

        public static void PrintTime(bool dots)
        {
            string time = DateTime.Now.ToString("HHmm");
            int top = Console.WindowHeight / 2 - 3;
            int left = Console.WindowWidth / 2 - 10;
            PrintBigDigit(left, top, time[0]);
            PrintBigDigit(left + 5, top, time[1]);
            PrintBigDigit(left + 10 + 3, top, time[2]);
            PrintBigDigit(left + 15 + 3, top, time[3]);
            if (dots)
            {
                Console.SetCursorPosition(left + 10 + 1, top + 1);
                Console.Write("#");
                Console.SetCursorPosition(left + 10 + 1, top + 3);
                Console.Write("#");
            }
            else
            {
                Console.SetCursorPosition(left + 10 + 1, 3);
                Console.Write(" ");
                Console.SetCursorPosition(left + 10 + 1, 5);
                Console.Write(" ");
            }
        }
    }


    class ProgramTimer
    {
        static void Timer(TimeSpan time, string message)
        {
            Thread.Sleep((int)time.TotalMilliseconds);
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write($"{DateTime.Now.ToString("HH:mm")}: {message}");
        }

        static void SetTimer()
        {
            Console.Clear();
            Console.Write("Продолжительность таймера в сек.: ");
            if (int.TryParse(Console.ReadLine(), out int seconds) && seconds >= 0)
            {
                Console.Write("Cooбщение: ");
                string message = Console.ReadLine();
                Task.Run(() => Timer(new TimeSpan(0, 0, seconds), message));
            }
            else
            {
                Console.Write("Продолжительность должна быть целым неотрицательным числом. Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {
            // часы
            var clock = Task.Run(() => {
                bool dots = true;
                while (true)
                {
                    Clock.PrintTime(dots);
                    dots = !dots;
                    Thread.Sleep(500);
                }
            });

            // управление
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("Нажмите ПРОБЕЛ, чтобы установить таймер");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Spacebar: SetTimer();  break;
                    case ConsoleKey.Escape: return;
                }
            }
        }
    }
}
