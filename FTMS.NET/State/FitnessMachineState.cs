namespace FTMS.NET.State;
internal record FitnessMachineState(EStateOpCode OpCode, byte[] Parameter) : IFitnessMachineState
{
}
