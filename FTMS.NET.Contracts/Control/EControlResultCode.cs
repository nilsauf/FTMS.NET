namespace FTMS.NET.Control;
public enum EControlResultCode : byte
{
	Success = 0x01,
	OpCodeNotSupported = 0x02,
	InvalidParameter = 0x03,
	OperationFailed = 0x04,
	ControlNotPermitted = 0x05
}
