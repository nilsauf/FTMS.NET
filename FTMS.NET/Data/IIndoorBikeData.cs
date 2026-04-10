namespace FTMS.NET.Data;
public interface IIndoorBikeData : IFitnessMachineData
{
	IFitnessMachineValue? InstantaneousSpeed { get; }
	IFitnessMachineValue? AverageSpeed { get; }
	IFitnessMachineValue? InstantaneousCadence { get; }
	IFitnessMachineValue? AverageCadence { get; }
	IFitnessMachineValue? TotalDistance { get; }
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
