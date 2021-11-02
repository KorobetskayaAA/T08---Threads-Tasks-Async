using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static void LongJob()
        {
            Thread.Sleep(10000);
        }

        static async void LongJobAsync()
        {
            Task.Run(LongJob);
        }

        static async void Main(string[] args)
        {
            Console.WriteLine("Task start");
            LongJobAsync();
            Console.WriteLine("Task end");
        }
    }
}
