namespace FTMS.NET.State;

using System.Collections.Immutable;

internal record FitnessMachineState(EStateOpCode OpCode, byte[] RawData) : IFitnessMachineState
{
	private readonly Lazy<IImmutableList<object>> parameters = new(
		() => FitnessMachineStateParameterFactory.ReadParameters(OpCode, RawData).ToImmutableList());

	public IImmutableList<object> ReadParameters()
		=> this.parameters.Value;
}
