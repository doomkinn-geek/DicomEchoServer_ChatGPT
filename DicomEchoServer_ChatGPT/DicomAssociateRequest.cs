using System.Text;

public class DicomAssociateRequest
{
    public byte ProtocolVersion { get; set; }
    public string CallingAE { get; set; }
    public string CalledAE { get; set; }
    public ushort MaxPduLength { get; set; }
    // Add more properties as needed

    public byte[] Serialize()
    {
        // Implement serialization logic to generate the byte array representation of the association request
        // For example:
        List<byte> bytes = new List<byte>();
        bytes.Add(0x01); // PDU type
        bytes.Add(0x00); // Reserved
        bytes.AddRange(BitConverter.GetBytes((ushort)bytes.Count)); // PDU length
        bytes.Add(0x01); // Protocol version
        bytes.AddRange(Encoding.ASCII.GetBytes(CallingAE.PadRight(16, ' '))); // Calling AE title
        bytes.AddRange(Encoding.ASCII.GetBytes(CalledAE.PadRight(16, ' '))); // Called AE title
        bytes.AddRange(BitConverter.GetBytes((ushort)0)); // Reserved
        bytes.AddRange(BitConverter.GetBytes(MaxPduLength)); // Max PDU length
        // Add more fields as needed
        return bytes.ToArray();
    }
}
