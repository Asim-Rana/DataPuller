using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataPuller.BAL
{
    public class Puller
    {
        public Puller()
        {
        }
        public void PullData(List<Site> siteList , bool updateFlag)
        {
            List<Task<dynamic>> taskArray = new List<Task<dynamic>>();

            for (int i = 0; i < siteList.Count; i++)
            {
                Console.WriteLine("starting......" + siteList[i].name);

                Task<dynamic> task = Task.Factory.StartNew(

                    () => PullBulkData(siteList[i] , updateFlag)

                );

                taskArray.Add(task);

                Thread.Sleep(5000);
            }

            while (!taskArray.All(i => i.IsCompleted))
            {
                Task<dynamic> completedTask = Task<dynamic>.WhenAny(taskArray).Result;

                dynamic result = completedTask.Result;

                taskArray.Remove(completedTask);
            }
        }
        public dynamic PullData(Site site , bool updateFlag)
        {
            PullData(new List<Site> { site } , updateFlag);

            return site.data;
        }
        private dynamic PullBulkData(Site site, bool updateFlag)
        {
            site.browser.Open();

            site.status = DataPuller.BAL.Site.SiteStatus.RUNNING;

            site.browser.MaximizeWindow();
            

            site.GetData(updateFlag);

            site.status = DataPuller.BAL.Site.SiteStatus.COMPLETED;

            site.browser.Close();

            site.status = DataPuller.BAL.Site.SiteStatus.CLOSED;

            return site.data;
        }
    }
}
