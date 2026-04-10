namespace FTMS.NET.Data;

public interface IStairClimberData : IFitnessMachineData
{
	IFitnessMachineValue? Floors { get; }
	IFitnessMachineValue? StepsPerMinute { get; }
	IFitnessMachineValue? AverageStepRate { get; }
	IFitnessMachineValue? PositiveElevationGain { get; }
	IFitnessMachineValue? StrideCount { get; }
	IFitnessMachineValue? TotalEnergy { get; }
	IFitnessMachineValue? EnergyPerHour { get; }
	IFitnessMachineValue? EnergyPerMinute { get; }
	IFitnessMachineValue? HeartRate { get; }
	IFitnessMachineValue? MetabolicEquivalent { get; }
	IFitnessMachineValue? ElapsedTime { get; }
	IFitnessMachineValue? RemainingTime { get; }
}
