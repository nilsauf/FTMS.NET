namespace FTMS.NET.Control;
public record ControlResponse(EControlOpCode RequestedOpCode, EControlResultCode ResultCode, byte[] ResponseParameter)
{
}
