public class DicomMessage
{
    // The byte array representation of the DICOM message.
    public byte[] Data { get; set; }

    // The length of the message.
    public int Length { get { return Data.Length; } }

    public DicomMessage(byte[] data)
    {
        Data = data;
    }
}
