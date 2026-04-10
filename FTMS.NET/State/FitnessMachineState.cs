namespace FTMS.NET.State;

using System.Collections.Immutable;

internal sealed record FitnessMachineState(
		EStateOpCode OpCode, 
		byte[] RawData) 
	: IFitnessMachineState
{
	private IImmutableList<object>? parameters;

	public IImmutableList<object> ReadParameters()
		=> this.parameters ??= FitnessMachineStateParameterFactory.ReadParameters(OpCode, RawData).ToImmutableList();
}
