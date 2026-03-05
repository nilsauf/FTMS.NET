namespace FTMS.NET.Data;

public interface IThreadmillData : IFitnessMachineData
{
	IFitnessMachineValue? InstantaneousSpeed { get; }
	IFitnessMachineValue? AverageSpeed { get; }
	IFitnessMachineValue? TotalDistance { get; }
	IFitnessMachineValue? Inclination { get; }
	IFitnessMachineValue? RampAngleSetting { get; }
	IFitnessMachineValue? PositiveElevationGain { get; }
	IFitnessMachineValue? NegativeElevationGain { get; }
	IFitnessMachineValue? InstantaneousPace { get; }
	IFitnessMachineValue? AveragePace { get; }
	IFitnessMachineValue? TotalEnergy { get; }
	IFitnessMachineValue? EnergyPerHour { get; }
	IFitnessMachineValue? EnergyPerMinute { get; }
	IFitnessMachineValue? HeartRate { get; }
	IFitnessMachineValue? MetabolicEquivalent { get; }
	IFitnessMachineValue? ElapsedTime { get; }
	IFitnessMachineValue? RemainingTime { get; }
	IFitnessMachineValue? ForceOnBelt { get; }
	IFitnessMachineValue? PowerOutput { get; }
}
