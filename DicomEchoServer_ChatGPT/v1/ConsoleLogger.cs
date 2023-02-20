public class ConsoleLogger
{
    public static void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {message}");
    }
}
