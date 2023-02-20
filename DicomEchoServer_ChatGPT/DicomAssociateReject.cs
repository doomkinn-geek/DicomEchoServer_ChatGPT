public class DicomAssociateReject : DicomPDU
{
    public DicomAssociateRejectResult Result { get; set; }
    public DicomAssociateRejectSource Source { get; set; }
    public DicomAssociateRejectReason Reason { get; set; }

    public override byte[] Serialize()
    {
        List<byte> pduBytes = new List<byte>();

        // Add PDU type and reserved field
        pduBytes.Add(0x03);
        pduBytes.Add(0x00);

        // Add PDU length
        ushort pduLength = (ushort)(4 + 2 + 2 + 1); // Result, Source, Reason, and reserved field
        pduBytes.AddRange(BitConverter.GetBytes(pduLength).Reverse());

        // Add result, source, reason, and reserved fields
        pduBytes.Add((byte)this.Result);
        pduBytes.Add((byte)this.Source);
        pduBytes.Add((byte)this.Reason);
        pduBytes.Add(0x00);

        return pduBytes.ToArray();
    }
}

public enum DicomAssociateRejectResult : byte
{
    RejectedPermanent = 0x01,
    RejectedTransient = 0x02
}

public enum DicomAssociateRejectSource : byte
{
    ServiceUser = 0x01,
    ServiceProviderACSE = 0x02,
    ServiceProviderPresentation = 0x03
}
