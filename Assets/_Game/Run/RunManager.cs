public static class RunManager
{
    public static RunModel CurrentRun { get; private set; }

    public static void SetRun(RunModel run)
    {
        CurrentRun = run;
    }
}