using System.Net;
using System.Net.Sockets;

public class CEchoScp
{
    private int _port;

    public CEchoScp(int port)
    {
        _port = port;
    }

    public void Start()
    {
        // Log that the service has started.
        ConsoleLogger.Log($"DICOM C-Echo SCP service started on port {_port}");

        TcpListener listener = new TcpListener(IPAddress.Any, _port);
        listener.Start();

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();

            // Log that a connection has been received.
            ConsoleLogger.Log($"Connection received from {((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()}");

            DicomMessage request = ReceiveMessage(client);

            // Log the received message.
            ConsoleLogger.Log($"Received message of length {request.Length} bytes");

            DicomMessage response = ProcessMessage(request);

            // Log the response message.
            ConsoleLogger.Log($"Sending message of length {response.Length} bytes");

            SendMessage(client, response);

            // Log that the connection has been closed.
            ConsoleLogger.Log($"Connection closed");

            client.Close();
        }
    }

    private DicomMessage ReceiveMessage(TcpClient client)
    {
        byte[] buffer = new byte[1024];

        MemoryStream stream = new MemoryStream();
        NetworkStream networkStream = client.GetStream();
        int bytesRead;
        do
        {
            bytesRead = networkStream.Read(buffer, 0, buffer.Length);
            stream.Write(buffer, 0, bytesRead);
        } while (bytesRead == buffer.Length);

        return new DicomMessage(stream.ToArray());
    }

    private void SendMessage(TcpClient client, DicomMessage message)
    {
        NetworkStream networkStream = client.GetStream();
        networkStream.Write(message.Data, 0, message.Length);
    }

    private DicomMessage ProcessMessage(DicomMessage request)
    {
        if (IsCEchoRequest(request))
        {
            byte[] responseBytes = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            return new DicomMessage(responseBytes);
        }
        else
        {
            return new DicomMessage(new byte[0]);
        }
    }

    private bool IsCEchoRequest(DicomMessage message)
    {
        if (message.Data.Length >= 12 && message.Data[0] == 0x02 && message.Data[1] == 0x00
            && message.Data[4] == 0x00 && message.Data[5] == 0x00 && message.Data[6] == 0x00 && message.Data[7] == 0x00
            && message.Data[8] == 'E' && message.Data[9] == 'C' && message.Data[10] == 'H' && message.Data[11] == 'O')
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
