using System.Net.Sockets;
using System.Net;
using System.Text;

public class DicomEchoServer
{
    public string AETitle { get; set; }
    public ushort Port { get; set; }
    // Add more properties as needed

    public void Start()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            bool associationEstablished = false;
            while (client.Connected && !stream.DataAvailable)
            {
                // Wait for association request
            }
            byte[] buffer = new byte[stream.ReadByte()];
            stream.Read(buffer, 0, buffer.Length);
            DicomAssociateRequest assocRequest = DeserializeAssociateRequest(buffer);
            if (assocRequest.CalledAE == AETitle)
            {
                associationEstablished = true;
                DicomAssociateAcceptance assocAccept = new DicomAssociateAcceptance()
                {
                    ProtocolVersion = 1,
                    RespondingAE = AETitle,
                    MaxPduLength = assocRequest.MaxPduLength
                };
                SendPdu(stream, assocAccept.Serialize());
            }
            while (client.Connected && associationEstablished)
            {
                if (stream.DataAvailable)
                {
                    buffer = new byte[stream.ReadByte()];
                    stream.Read(buffer, 0, buffer.Length);
                    byte pduType = buffer[0];
                    switch (pduType)
                    {
                        case 0x01: // Association request
                            DicomAssociateRequest newAssocRequest = DeserializeAssociateRequest(buffer);
                            DicomAssociateRejectReason rejectReason = DicomAssociateRejectReason.NoReasonGiven;
                            // Validate new association request, set rejectReason if invalid
                            // ...
                            if (rejectReason != DicomAssociateRejectReason.NoReasonGiven)
                            {
                                DicomAssociateReject assocReject = new DicomAssociateReject()
                                {
                                    Result = DicomAssociateRejectResult.RejectedPermanent,
                                    Source = DicomAssociateRejectSource.ServiceUser,
                                    Reason = rejectReason
                                };
                                SendPdu(stream, assocReject.Serialize());
                                client.Close();
                                associationEstablished = false;
                            }
                            else
                            {
                                // Accept new association request
                                DicomAssociateAcceptance newAssocAccept = new DicomAssociateAcceptance()
                                {
                                    ProtocolVersion = 1,
                                    RespondingAE = AETitle,
                                    MaxPduLength = newAssocRequest.MaxPduLength
                                };
                                SendPdu(stream, newAssocAccept.Serialize());
                            }
                            break;
                        case 0x03: // C-Echo request
                                   // ...
                            break;
                        case 0x05: // Association release request
                                   // Release association
                                   // ...
                            break;
                        case 0x07: // Abort message
                                   // ...
                            break;
                    }

                }
            }
        }
    }

    private void SendPdu(NetworkStream stream, byte[] pduBytes)
    {
        byte[] lengthBytes = BitConverter.GetBytes((ushort)pduBytes.Length);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(lengthBytes);
        }
        byte[] messageBytes = new byte[lengthBytes.Length + pduBytes.Length];
        Array.Copy(lengthBytes, messageBytes, lengthBytes.Length);
        Array.Copy(pduBytes, 0, messageBytes, lengthBytes.Length, pduBytes.Length);
        stream.Write(messageBytes, 0, messageBytes.Length);
    }

    private DicomAssociateRequest DeserializeAssociateRequest(byte[] bytes)
    {
        // Implement deserialization logic to convert the byte array representation of an association request to a DicomAssociateRequest object
        // For example:
        DicomAssociateRequest assocRequest = new DicomAssociateRequest()
        {
            ProtocolVersion = bytes[2],
            CalledAE = Encoding.ASCII.GetString(bytes, 4, 16).TrimEnd('\0'),
            CallingAE = Encoding.ASCII.GetString(bytes, 20, 16).TrimEnd('\0'),
            MaxPduLength = BitConverter.ToUInt16(bytes, 36),
            // Add more fields as needed
        };
        return assocRequest;
    }

    private DicomEchoRequest DeserializeEchoRequest(byte[] bytes)
    {
        // Implement deserialization logic to convert the byte array representation of a C-Echo request to a DicomEchoRequest object
        // For example:
        DicomEchoRequest echoRequest = new DicomEchoRequest()
        {
            MessageId = BitConverter.ToUInt16(bytes, 4),
            // Add more fields as needed
        };
        return echoRequest;
    }
}
