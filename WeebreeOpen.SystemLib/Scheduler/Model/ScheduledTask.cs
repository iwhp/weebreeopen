namespace WeebreeOpen.SystemLib.Scheduler.Model
{
    using System;
    using System.Linq;
    using WeebreeOpen.SystemLib.Scheduler.Model;

    public class ScheduledTask
    {
        public FunctionName FunctionName { get; set; }

        public Trigger Trigger { get; set; }
    }
}
