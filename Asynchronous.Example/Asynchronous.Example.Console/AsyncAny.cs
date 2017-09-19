using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronous.Example.Console
{
    public class AsyncAny
    {

        //example adapted from https://msdn.microsoft.com/en-us/library/jj155756(v=vs.110).aspx

        // Declare a System.Threading.CancellationTokenSource.
        CancellationTokenSource cts;

        public AsyncAny()
        {

        }


        public async void startAsync()
        {

            // Instantiate the CancellationTokenSource.
            cts = new CancellationTokenSource();

           // cts.CancelAfter(5000);

            try
            {
                await AccessTheWebAsync(cts.Token);
                System.Console.WriteLine("Downloads complete.");
            }
            catch (OperationCanceledException)
            {
                System.Console.WriteLine("doing some other task.");

                System.Console.WriteLine("Downloads canceled.");
            }
            catch (Exception)
            {
                System.Console.WriteLine("Downloads failed.");
            }

            cts = null;
        }

        public void cancelAsync()
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }

        async Task AccessTheWebAsync(CancellationToken ct)
        {
            HttpClient client = new HttpClient();

            // Make a list of web addresses.
            List<string> urlList = SetUpURLList();

            // ***Create a query that, when executed, returns a collection of tasks.
            IEnumerable<Task<int>> downloadTasksQuery =
                from url in urlList select ProcessURL(url, client, ct);

            // ***Use ToList to execute the query and start the tasks. 
            List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

            // ***Add a loop to process the tasks one at a time until none remain. 
            while (downloadTasks.Count > 0)
            {
                // Identify the first task that completes.
                Task<int> firstFinishedTask = await Task.WhenAny(downloadTasks);

                
                // ***Remove the selected task from the list so that you don't 
                // process it more than once.
                downloadTasks.Remove(firstFinishedTask);

                // Await the completed task. 
                int length = await firstFinishedTask;
                System.Console.WriteLine(String.Format("\r\nLength of the download:  {0}", length));
            }
        }


        private List<string> SetUpURLList()
        {
            List<string> urls = new List<string>
            {
                "http://msdn.microsoft.com",
                "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "http://msdn.microsoft.com/en-us/library/hh290136.aspx",
                "http://msdn.microsoft.com/en-us/library/dd470362.aspx",
                "http://msdn.microsoft.com/en-us/library/aa578028.aspx",
                "http://msdn.microsoft.com/en-us/library/ms404677.aspx",
                "http://msdn.microsoft.com/en-us/library/ff730837.aspx",
                "http://www.lbaware.com"
            };
            return urls;
        }


        async Task<int> ProcessURL(string url, HttpClient client, CancellationToken ct)
        {
            // GetAsync returns a Task<HttpResponseMessage>. 
            HttpResponseMessage response = await client.GetAsync(url, ct);

            // Retrieve the website contents from the HttpResponseMessage. 
            byte[] urlContents = await response.Content.ReadAsByteArrayAsync();

            return urlContents.Length;
        }
    }

}

