using System.Diagnostics;

public static class Timer
{
    public static Stopwatch stopwatch = new Stopwatch();  

    /// <summary>
    /// starts the stopwatch
    /// </summary>
    public static void start()
    {
        stopwatch.Start();
    }

    /// <summary>
    /// stops the stopwatch
    /// </summary>
    public static void stop()
    {
        stopwatch.Stop();
    }

    /// <summary>
    /// prints the elapsed time, formatted as DD:HH:MM:SS
    /// </summary>
    public static void printLongFormTime()
    {
        TimeSpan timespan = stopwatch.Elapsed;
        string elapsed_time = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", 
                                            timespan.Days, timespan.Hours, 
                                            timespan.Minutes, timespan.Seconds);
        Console.WriteLine("execution time: {0}", elapsed_time);
    }

    /// <summary>
    /// prints the elapsed time, formatted as MM:SS:MS
    /// </summary>
    public static void printShortFormTime()
    {
        TimeSpan timespan = stopwatch.Elapsed;
        string elapsed_time = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", 
                                            timespan.Minutes, timespan.Seconds, 
                                            timespan.Milliseconds / 10);
        Console.WriteLine("execution time: {0}", elapsed_time);
    }

    /// <summary>
    /// resets the stopwatch
    /// </summary>
    public static void reset()
    {
        stopwatch.Reset();
    }
}