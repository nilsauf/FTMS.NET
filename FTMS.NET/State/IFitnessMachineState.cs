namespace FTMS.NET.State;

using System.Collections.Immutable;

public interface IFitnessMachineState
{
	EStateOpCode OpCode { get; }
	byte[] RawData { get; }
	IImmutableList<object> ReadParameters();
}
