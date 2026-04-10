namespace FTMS.NET.Data;

public interface IRowerData : IFitnessMachineData
{
	IFitnessMachineValue? StrokeRate { get; }
	IFitnessMachineValue? StrokeCount { get; }
	IFitnessMachineValue? AverageStrokeRate { get; }
	IFitnessMachineValue? TotalDistance { get; }
	IFitnessMachineValue? InstantaneousPace { get; }
	IFitnessMachineValue? AveragePace { get; }
	IFitnessMachineValue? InstantaneousPower { get; }
	IFitnessMachineValue? AveragePower { get; }
	IFitnessMachineValue? ResistantLevel { get; }
	IFitnessMachineValue? TotalEnergy { get; }
	IFitnessMachineValue? EnergyPerHour { get; }
	IFitnessMachineValue? EnergyPerMinute { get; }
	IFitnessMachineValue? HeartRate { get; }
	IFitnessMachineValue? MetabolicEquivalent { get; }
	IFitnessMachineValue? ElapsedTime { get; }
	IFitnessMachineValue? RemainingTime { get; }
}
