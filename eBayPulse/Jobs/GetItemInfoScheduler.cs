using Quartz;
using System.Threading.Tasks;
using eBayPulse.Models;
using eBayPulse.Tools;
using Quartz.Impl;

namespace eBayPulse.Jobs
{
    public class GetItemScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ItemDataGetter>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(5)
                    .RepeatForever())
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }
    }
    
    public class ItemDataGetter : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            foreach (var item in DBConnector.getConnection().context.Item)
            {
                eBayItemDataHelper eBayItem = new eBayItemDataHelper(item.eBayId);
                await eBayItem.GeteBayItemDataHelper();
                Pulse pulse = new Pulse(eBayItem, item);
                DBConnector.getConnection().context.Pulse.Add(pulse);
                DBConnector.getConnection().context.SaveChanges();
            }
        }
    }
}