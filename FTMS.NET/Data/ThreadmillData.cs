namespace FTMS.NET.Data;

using DynamicData;
using System;

public sealed class ThreadmillData(IFitnessMachineData data) : IThreadmillData
{
	public IFitnessMachineValue? InstantaneousSpeed => data.GetValue(FtmsUuids.InstantaneousSpeed);
	public IFitnessMachineValue? AverageSpeed => data.GetValue(FtmsUuids.AverageSpeed);
	public IFitnessMachineValue? TotalDistance => data.GetValue(FtmsUuids.TotalDistance);
	public IFitnessMachineValue? Inclination => data.GetValue(FtmsUuids.Inclination);
	public IFitnessMachineValue? RampAngleSetting => data.GetValue(FtmsUuids.RampAngleSetting);
	public IFitnessMachineValue? PositiveElevationGain => data.GetValue(FtmsUuids.PositiveElevationGain);
	public IFitnessMachineValue? NegativeElevationGain => data.GetValue(FtmsUuids.NegativeElevationGain);
	public IFitnessMachineValue? InstantaneousPace => data.GetValue(FtmsUuids.InstantaneousPace);
	public IFitnessMachineValue? AveragePace => data.GetValue(FtmsUuids.AveragePace);
	public IFitnessMachineValue? TotalEnergy => data.GetValue(FtmsUuids.TotalEnergy);
	public IFitnessMachineValue? EnergyPerHour => data.GetValue(FtmsUuids.EnergyPerHour);
	public IFitnessMachineValue? EnergyPerMinute => data.GetValue(FtmsUuids.EnergyPerMinute);
	public IFitnessMachineValue? HeartRate => data.GetValue(FtmsUuids.HeartRate);
	public IFitnessMachineValue? MetabolicEquivalent => data.GetValue(FtmsUuids.MetabolicEquivalent);
	public IFitnessMachineValue? ElapsedTime => data.GetValue(FtmsUuids.ElapsedTime);
	public IFitnessMachineValue? RemainingTime => data.GetValue(FtmsUuids.RemainingTime);
	public IFitnessMachineValue? ForceOnBelt => data.GetValue(FtmsUuids.ForceOnBelt);
	public IFitnessMachineValue? PowerOutput => data.GetValue(FtmsUuids.PowerOutput);

	public IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect() => data.Connect();
	public IFitnessMachineValue? GetValue(Guid uuid) => data.GetValue(uuid);
	public void Dispose() => data.Dispose();
}
