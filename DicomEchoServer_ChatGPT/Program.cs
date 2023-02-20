namespace DicomEchoServer_ChatGPT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*try
            {
                CEchoScp cEchoScp = new CEchoScp(11112);
                cEchoScp.Start();
            }
            catch (Exception ex)
            {
                ConsoleLogger.Log($"An error occurred: {ex.Message}");
            }*/
            // Create a new instance of the DicomServer class
            DicomEchoServer server = new DicomEchoServer();
            server.AETitle = "MY_AE_TITLE";
            server.Port = 1234;

            server.Start();
            Console.WriteLine("DicomEchoScpExample: Listening on port 1234...");

           
        }
    }
}