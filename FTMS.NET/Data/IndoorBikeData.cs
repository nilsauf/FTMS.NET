namespace FTMS.NET.Data;

using DynamicData;
using System;

public class IndoorBikeData(IFitnessMachineData data) : IIndoorBikeData
{
	public IFitnessMachineValue? InstantaneousSpeed
		=> data.GetValue(FtmsUuids.IndoorBike.InstantaneousSpeed);

	public IFitnessMachineValue? AverageSpeed
		=> data.GetValue(FtmsUuids.IndoorBike.AverageSpeed);

	public IFitnessMachineValue? InstantaneousCadence
		=> data.GetValue(FtmsUuids.IndoorBike.InstantaneousCadence);

	public IFitnessMachineValue? AverageCadence
		=> data.GetValue(FtmsUuids.IndoorBike.AverageCadence);

	public IFitnessMachineValue? TotalDistance
		=> data.GetValue(FtmsUuids.IndoorBike.TotalDistance);

	public IFitnessMachineValue? ResistantLevel
		=> data.GetValue(FtmsUuids.IndoorBike.ResistantLevel);

	public IFitnessMachineValue? InstantaneousPower
		=> data.GetValue(FtmsUuids.IndoorBike.InstantaneousPower);

	public IFitnessMachineValue? AveragePower
		=> data.GetValue(FtmsUuids.IndoorBike.AveragePower);

	public IFitnessMachineValue? TotalEnergy
		=> data.GetValue(FtmsUuids.IndoorBike.TotalEnergy);

	public IFitnessMachineValue? EnergyPerHour
		=> data.GetValue(FtmsUuids.IndoorBike.EnergyPerHour);

	public IFitnessMachineValue? EnergyPerMinute
		=> data.GetValue(FtmsUuids.IndoorBike.EnergyPerMinute);

	public IFitnessMachineValue? HeartRate
		=> data.GetValue(FtmsUuids.IndoorBike.HeartRate);

	public IFitnessMachineValue? MetabolicEquivalent
		=> data.GetValue(FtmsUuids.IndoorBike.MetabolicEquivalent);

	public IFitnessMachineValue? ElapsedTime
		=> data.GetValue(FtmsUuids.IndoorBike.ElapsedTime);

	public IFitnessMachineValue? RemainingTime
		=> data.GetValue(FtmsUuids.IndoorBike.RemainingTime);

	public IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect() => data.Connect();

	public IFitnessMachineValue? GetValue(Guid uuid) => data.GetValue(uuid);
}
