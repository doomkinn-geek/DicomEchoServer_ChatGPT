public class DicomEchoResponse
{
    public ushort MessageId { get; set; }
    public byte Status { get; set; }
    // Add more properties as needed

    public byte[] Serialize()
    {
        // Implement serialization logic to generate the byte array representation of the echo response
        // For example:
        List<byte> bytes = new List<byte>();
        bytes.Add(0x03); // PDU type
        bytes.Add(0x00); // Reserved
        bytes.AddRange(BitConverter.GetBytes((ushort)10)); // PDU length
        bytes.AddRange(BitConverter.GetBytes(MessageId)); // Message ID
        bytes.AddRange(BitConverter.GetBytes((ushort)0)); // Command data set type
        bytes.AddRange(BitConverter.GetBytes((uint)0)); // Command data set length
        bytes.Add(Status); // Status
        // Add more fields as needed
        return bytes.ToArray();
    }
}
