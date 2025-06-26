namespace WeebreeOpen.SystemLib.Scheduler.Model;

public class ScheduledTask
{
    public FunctionName FunctionName { get; set; }

    public Trigger Trigger { get; set; }
}
