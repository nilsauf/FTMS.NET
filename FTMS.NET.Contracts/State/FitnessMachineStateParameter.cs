namespace FTMS.NET.State;

public sealed record FitnessMachineStateParameter(
	string Name,
	FitnessMachineUnit Unit,
	double Value);