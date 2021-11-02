using System;
using System.Threading;

namespace Threads
{
    class ProgramThreads
    {
        const int n = 1000;

        static void Print(string msg)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i}: {msg}");
            }
        }


        static void Main(string[] args)
        {
            var t1 = new Thread(() => Print("x"));
            var t2 = new Thread(() => Print("y"));
            t1.Priority = ThreadPriority.BelowNormal;
            t2.Priority = ThreadPriority.Highest;
            t1.Start();
            t2.Start();
            Console.ReadKey();
        }
    }
}
