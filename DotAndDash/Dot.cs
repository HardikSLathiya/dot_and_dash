using System;
using System.Threading;

namespace DotAndDash
{
    public class Dot
    {
        private readonly CancellationToken cancellationToken;
        private readonly ManualResetEvent manualResetEvent;

        public Dot(CancellationToken cancellationToken, ManualResetEvent manualResetEvent)
        {
            this.cancellationToken = cancellationToken;
            this.manualResetEvent = manualResetEvent;
        }

        public void ShowDot()
        {
            try
            {
                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    manualResetEvent.WaitOne();

                    Console.Write(".");
                    Thread.Sleep(1000);
                }
            }
            catch (OperationCanceledException)
            {
                Thread.CurrentThread.Interrupt();
            }
        }
    }
}