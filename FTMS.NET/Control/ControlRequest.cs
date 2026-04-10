namespace FTMS.NET.Control;

public sealed record ControlRequest(
	EControlOpCode OpCode,
	byte[] Parameter);