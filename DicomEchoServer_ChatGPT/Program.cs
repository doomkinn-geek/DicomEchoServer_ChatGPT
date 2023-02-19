namespace DicomEchoServer_ChatGPT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CEchoScp cEchoScp = new CEchoScp(11112);
                cEchoScp.Start();
            }
            catch (Exception ex)
            {
                ConsoleLogger.Log($"An error occurred: {ex.Message}");
            }
        }
    }
}