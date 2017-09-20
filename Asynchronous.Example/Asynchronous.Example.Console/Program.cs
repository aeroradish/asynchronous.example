using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronous.Example.Console
{
    class Program
    {
        private static DALAsync dalAsync = new DALAsync();
        public const int RecordLength = 10000;
        static void Main(string[] args)
        {
           

            List<Information> list = null;
            list = GetTenThousandRecords();

            InsertTenThousandRecordsAysnc(list);
            //DownloadStuff();

            //RepeatStuff();

            //int i = 0;
            //Information model = new Information();
            //i = dalAsync.InformationInsertAsync(model).Result;

        }

        private static void RepeatStuff()
        {
            RinseAndRepeat repeat = new RinseAndRepeat();
            System.Console.WriteLine("Click to do something. Quit with 'q'.");
            while (true)
            {
                string message = System.Console.ReadLine();
                if (message.ToLower() == "q") break;
                                                 
                
                repeat.StartRinseCycle();
                

            }
        }

        private static void DownloadStuff()
        {
            AsyncAny anyExample = new AsyncAny();
            System.Console.WriteLine("Click to do something. Quit with 'q'.");
            while (true)
            {
                string message = System.Console.ReadLine();
                if (message.ToLower() == "q") break;
                if (message.ToLower() == "c")
                {
                    anyExample.cancelAsync();
                } else
                {
                    anyExample.startAsync();
                }


            }
        }


        private static List<Information> GetTenThousandRecords()
        {
            List<Information> list = new List<Information>();
            Information item = null;

            for (int i = 0; i < RecordLength; i++)
            {

                item = new Information();
                item.Description = String.Format("description {0}", i);

                item.Col1 = String.Format("{0}000", i);
                item.Col2 = String.Format("{0}100", i);
                item.Col3 = String.Format("{0}200", i);
                item.Col4 = String.Format("{0}300", i);

                item.Col5 = String.Format("{0}400", i);
                item.Col6 = String.Format("{0}500", i);
                item.Col7 = String.Format("{0}600", i);
                item.Col8 = String.Format("{0}700", i);

                list.Add(item);

            }

            return list;

        }
        
        private static void InsertTenThousandRecordsAysnc(List<Information> list)
        {
            int skip = 0;
            List<Information> subList = null;
            
            int take = 10;

            while (skip < RecordLength)
            {

                subList = list.Skip(skip).Take(take).ToList();


                List<Task> TaskList = new List<Task>();

                var task1 = Insert(subList[0]);
                var task2 = Insert(subList[1]);
                var task3 = Insert(subList[2]);
                var task4 = Insert(subList[3]);
                var task5 = Insert(subList[4]);
                var task6 = Insert(subList[5]);
                var task7 = Insert(subList[6]);
                var task8 = Insert(subList[7]);
                var task9 = Insert(subList[8]);
                var task0 = Insert(subList[9]);

                TaskList.Add(task1);
                TaskList.Add(task2);
                TaskList.Add(task3);
                TaskList.Add(task4);
                TaskList.Add(task5);
                TaskList.Add(task6);
                TaskList.Add(task7);
                TaskList.Add(task8);
                TaskList.Add(task9);
                TaskList.Add(task0);

                Task.WaitAll(TaskList.ToArray());

                skip += take;

            }

        }

        public static async Task Insert(Information model)
        {
            
            var ddaAcct = await dalAsync.InformationInsertAsync(model);

        }

    }
}
