using System;
using System.Threading;

namespace CV19Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "MainThread";

            var secondThread = new Thread(ThreadMethod)
            {
                Name = "SecondThread",
                IsBackground = true,
                Priority = ThreadPriority.AboveNormal
            };

            secondThread.Start(42);

            var count = 5;
            var message = "Hello World!";
            var timeout = 150;

            new Thread(() => PrintMethod(message, count, timeout)) { IsBackground = true }
                .Start();

            CheckThread();

            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }

        private static void PrintMethod(string message, int count, int timeout)
        {
            for (var i = 0; i < count; i++)
            {
                Console.WriteLine(message);
                Thread.Sleep(timeout);
            }
        }

        private static void ThreadMethod(object parameter)
        {
            var value = (int) parameter;
            Console.WriteLine(value);

            CheckThread();

            while (true)
            {
                Thread.Sleep(100);
                Console.Title = DateTime.Now.ToString();
            }
        }

        private static void CheckThread()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("ID: {0} - {1}", thread.ManagedThreadId, thread.Name);
        }
    }
}
