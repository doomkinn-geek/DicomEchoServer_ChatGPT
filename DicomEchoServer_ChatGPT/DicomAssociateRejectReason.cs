public enum DicomAssociateRejectReason : byte
{
    ServiceUserAuthenticationFailed = 0x01,
    ServiceProviderAuthenticationFailed = 0x02,
    ServiceUserInitiatedAbort = 0x03,
    ServiceProviderInitiatedAbort = 0x04,
    ServiceProviderInitiatedDisconnect = 0x05,
    ApplicationContextNotSupported = 0x06,
    CallingAENotRecognized = 0x07,
    CalledAENotRecognized = 0x08,
    Reserved = 0x09,
    InvalidPDULength = 0x0A,
    InvalidPDUType = 0x0B,
    ServiceNotSupported = 0x0C,
    ServiceNotImplemented = 0x0D,
    ServiceTemporarilyOutOfOrder = 0x0E,
    ReservedForExtension = 0x0F,
    NoReasonGiven = 0x10
}
