namespace FTMS.NET.Control;

public sealed record ControlResponse(
	EControlOpCode RequestedOpCode,
	EControlResultCode ResultCode,
	byte[] ResponseParameter);