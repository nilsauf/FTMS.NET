namespace FTMS.NET.State;
public interface IFitnessMachineState
{
	EStateOpCode OpCode { get; }

	byte[] Parameter { get; }
}
