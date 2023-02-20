using System.Text;

public class DicomAssociateAcceptance
{
    public byte ProtocolVersion { get; set; }
    public string RespondingAE { get; set; }
    public ushort MaxPduLength { get; set; }
    // Add more properties as needed

    public byte[] Serialize()
    {
        // Implement serialization logic to generate the byte array representation of the association acceptance
        // For example:
        List<byte> bytes = new List<byte>();
        bytes.Add(0x02); // PDU type
        bytes.Add(0x00); // Reserved
        bytes.AddRange(BitConverter.GetBytes((ushort)bytes.Count)); // PDU length
        bytes.Add(0x01); // Protocol version
        bytes.AddRange(Encoding.ASCII.GetBytes(RespondingAE.PadRight(16, ' '))); // Responding AE title
        bytes.AddRange(BitConverter.GetBytes((ushort)0)); // Reserved
        bytes.AddRange(BitConverter.GetBytes(MaxPduLength)); // Max PDU length
        // Add more fields as needed
        return bytes.ToArray();
    }
}
