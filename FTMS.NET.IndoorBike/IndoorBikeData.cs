namespace FTMS.NET.IndoorBike;
using System;

using FTMS.NET.Data;

public class IndoorBikeData(IObservable<byte[]> observeData) :
	FitnessMachineData<IndoorBikeData>(observeData, new IndoorBikeDataReader()),
	IIndoorBikeData
{
	public override EFitnessMachineType Type => EFitnessMachineType.IndoorBike;

	public IFitnessMachineValue? InstantaneousSpeed
		=> this.GetValue(IndoorBikeDataValueUuids.InstantaneousSpeed);

	public IFitnessMachineValue? AverageSpeed
		=> this.GetValue(IndoorBikeDataValueUuids.AverageSpeed);

	public IFitnessMachineValue? InstantaneousCadence
		=> this.GetValue(IndoorBikeDataValueUuids.InstantaneousCadence);

	public IFitnessMachineValue? AverageCadence
		=> this.GetValue(IndoorBikeDataValueUuids.AverageCadence);

	public IFitnessMachineValue? TotalDistance
		=> this.GetValue(IndoorBikeDataValueUuids.TotalDistance);

	public IFitnessMachineValue? ResistantLevel
		=> this.GetValue(IndoorBikeDataValueUuids.ResistantLevel);

	public IFitnessMachineValue? InstantaneousPower
		=> this.GetValue(IndoorBikeDataValueUuids.InstantaneousPower);

	public IFitnessMachineValue? AveragePower
		=> this.GetValue(IndoorBikeDataValueUuids.AveragePower);

	public IFitnessMachineValue? TotalEnergy
		=> this.GetValue(IndoorBikeDataValueUuids.TotalEnergy);

	public IFitnessMachineValue? EnergyPerHour
		=> this.GetValue(IndoorBikeDataValueUuids.EnergyPerHour);

	public IFitnessMachineValue? EnergyPerMinute
		=> this.GetValue(IndoorBikeDataValueUuids.EnergyPerMinute);

	public IFitnessMachineValue? HeartRate
		=> this.GetValue(IndoorBikeDataValueUuids.HeartRate);

	public IFitnessMachineValue? MetabolicEquivalent
		=> this.GetValue(IndoorBikeDataValueUuids.MetabolicEquivalent);

	public IFitnessMachineValue? ElapsedTime
		=> this.GetValue(IndoorBikeDataValueUuids.ElapsedTime);

	public IFitnessMachineValue? RemainingTime
		=> this.GetValue(IndoorBikeDataValueUuids.RemainingTime);

	protected override Guid GetValueUuid(string name)
		=> IndoorBikeDataValueUuids.GetUuid(name);
}
