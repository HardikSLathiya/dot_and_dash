using System;
using System.Threading;

namespace DotAndDash
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var dotManualResetEvent = new ManualResetEvent(false);
            var dashManualResetEvent = new ManualResetEvent(false);

            var dot = new Dot(cancellationTokenSource.Token, dotManualResetEvent);

            var dotThread = new Thread(dot.ShowDot);
            dotThread.IsBackground = false;
            dotThread.Start();

            var dash = new Dash();
            ThreadPool.QueueUserWorkItem(dash.ShowDash, (cancellationTokenSource.Token, dashManualResetEvent));

            while (true)
            {
                var keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.A:
                        dotManualResetEvent.Set();
                        break;
                    case ConsoleKey.S:
                        dotManualResetEvent.Reset();
                        break;
                    case ConsoleKey.Z:
                        dashManualResetEvent.Set();
                        break;
                    case ConsoleKey.X:
                        dashManualResetEvent.Reset();
                        break;
                    case ConsoleKey.Q:
                        cancellationTokenSource.Cancel();
                        return;
                }
            }
        }
    }
}