using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//example derived from https://stackoverflow.com/questions/22449518/how-to-use-async-and-await-in-timer
namespace Asynchronous.Example.Console
{
    public class RinseAndRepeat
    {

        CancellationTokenSource cts;

        int duration = 12;
        int wait = 3;

        public RinseAndRepeat()
        {

        }

        public void StartRinseCycle()
        {
            cts = new CancellationTokenSource(TimeSpan.FromSeconds(duration));
            System.Console.WriteLine("Starting action loop.");
            RepeatActionEvery(() => System.Console.WriteLine("Action"), TimeSpan.FromSeconds(wait), cts.Token).Wait();
            System.Console.WriteLine("Finished action loop.");

        }
        
        public async Task RepeatActionEvery(Action action, TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                action();
                Task task = Task.Delay(interval, cancellationToken);

                try
                {
                    await task;
                }

                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }

    }
}
