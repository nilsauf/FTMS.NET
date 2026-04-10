namespace FTMS.NET.Data;

public interface IStepClimberData : IFitnessMachineData
{
	IFitnessMachineValue? Floors { get; }
	IFitnessMachineValue? StepCount { get; }
	IFitnessMachineValue? StepsPerMinute { get; }
	IFitnessMachineValue? AverageStepRate { get; }
	IFitnessMachineValue? PositiveElevationGain { get; }
	IFitnessMachineValue? TotalEnergy { get; }
	IFitnessMachineValue? EnergyPerHour { get; }
	IFitnessMachineValue? EnergyPerMinute { get; }
	IFitnessMachineValue? HeartRate { get; }
	IFitnessMachineValue? MetabolicEquivalent { get; }
	IFitnessMachineValue? ElapsedTime { get; }
	IFitnessMachineValue? RemainingTime { get; }
}
