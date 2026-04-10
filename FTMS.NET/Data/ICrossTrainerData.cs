namespace FTMS.NET.Data;

public interface ICrossTrainerData : IFitnessMachineData
{
	IFitnessMachineValue? InstantaneousSpeed { get; }
	IFitnessMachineValue? AverageSpeed { get; }
	IFitnessMachineValue? TotalDistance { get; }
	IFitnessMachineValue? StepsPerMinute { get; }
	IFitnessMachineValue? AverageStepRate { get; }
	IFitnessMachineValue? StrideCount { get; }
	IFitnessMachineValue? PositiveElevationGain { get; }
	IFitnessMachineValue? NegativeElevationGain { get; }
	IFitnessMachineValue? Inclination { get; }
	IFitnessMachineValue? RampAngleSetting { get; }
	IFitnessMachineValue? ResistantLevel { get; }
	IFitnessMachineValue? InstantaneousPower { get; }
	IFitnessMachineValue? AveragePower { get; }
	IFitnessMachineValue? TotalEnergy { get; }
	IFitnessMachineValue? EnergyPerHour { get; }
	IFitnessMachineValue? EnergyPerMinute { get; }
	IFitnessMachineValue? HeartRate { get; }
	IFitnessMachineValue? MetabolicEquivalent { get; }
	IFitnessMachineValue? ElapsedTime { get; }
	IFitnessMachineValue? RemainingTime { get; }
}
