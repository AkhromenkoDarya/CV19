using System;
using System.Collections.Generic;
using System.Threading;

namespace CV19Console
{
    class Program
    {
        private static bool _isClockThreadExecuted = true;

        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "MainThread";

            var clockThread = new Thread(ThreadMethod)
            {
                Name = "ClockThread",
                IsBackground = true,
                Priority = ThreadPriority.AboveNormal
            };

            //secondThread.Start(42);

            //var count = 5;
            //var message = "Hello World!";
            //var timeout = 150;

            //new Thread(() => PrintMethod(message, count, timeout)) { IsBackground = true }
            //    .Start();

            //CheckThread();

            //for (var i = 0; i < 5; i++)
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine(i);
            //}

            var values = new List<int>();
            var threads = new Thread[10];

            var lockObject = new object();

            for (var i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (var j = 0; j < 10; j++)
                    {
                        //lock(values)
                        lock (lockObject)
                            values.Add(Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(1);
                    }
                });
            }

            #region LockRealization

            Monitor.Enter(lockObject);

            try
            {
                // Критическая секция.
            }
            finally
            {
                Monitor.Exit(lockObject);
            }

            #endregion

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            //clockThread.Join();

            if (!clockThread.Join(100))
            {
                // Прерывает поток в любой точке процесса его выполнения.
                //clockThread.Abort();

                // Альтернативный вариант.
                clockThread.Interrupt();
            }

            Console.WriteLine(string.Join(",", values));
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

            while (_isClockThreadExecuted)
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
