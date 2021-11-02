using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class ProgramTasks
    {
        static double MakeWork(double number)
        {
            for (long i = 0; i < 100_000_000; i++)
            {
                if (DateTime.Now.Millisecond < 100)
                {
                    throw new Exception("Поток вылетел");
                }
                number = Math.Cos(number);
            }
            return number;
        }

        public static void Main()
        {
            var task = Task.Run(() => MakeWork(1.5));
            //Task.WaitAll(task);
            while (task.Status == TaskStatus.Running)
                Console.Write(".");
            Console.WriteLine();
            if (task.IsFaulted)
            {
                var ex = task.Exception;
                Console.WriteLine(ex.Message);
                return;
            }
            var returnedValue = task.Result;
            Console.WriteLine(returnedValue);
        }

    }
}
