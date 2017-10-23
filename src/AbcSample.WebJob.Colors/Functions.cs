using System;
using System.IO;
using System.Threading;
using AbcSample.DAL;
using AbcSample.Service;
using Microsoft.Azure.WebJobs;

namespace AbcSample.WebJob.Colors
{
    public class Functions
    {
        [NoAutomaticTrigger]
        public static void UpdateColors(TextWriter log)
        {
            ColorService service = new ColorService(new ColorRepository());
            while (true)
            {
                var updateTask = service.UpdateRandomColors();
                updateTask.Wait();
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }
}