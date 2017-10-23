using Microsoft.Azure.WebJobs;

namespace AbcSample.WebJob.Colors
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var config = new JobHostConfiguration();

            //if (config.IsDevelopment)
            //{
            //    config.UseDevelopmentSettings();
            //}

            var host = new JobHost(config);
            host.Call(typeof(Functions).GetMethod("UpdateColors"));
            // The following code ensures that the WebJob will be running continuously
            //host.RunAndBlock();
        }

        
    }
}
