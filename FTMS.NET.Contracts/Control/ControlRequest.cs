namespace FTMS.NET.Control;
public record ControlRequest(EControlOpCode OpCode, byte[] Parameter)
{
}
