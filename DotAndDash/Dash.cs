using System;
using System.Threading;

namespace DotAndDash
{
    public class Dash
    {
        public void ShowDash(object state)
        {
            var (cancellationToken, manualResetEvent) = ((CancellationToken, ManualResetEvent))state;

            try
            {
                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    manualResetEvent.WaitOne();

                    Console.Write("-");
                    Thread.Sleep(3000);
                }
            }
            catch (OperationCanceledException)
            {
                Thread.CurrentThread.Interrupt();
            }
        }
    }
}