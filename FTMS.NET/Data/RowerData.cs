namespace FTMS.NET.Data;

using DynamicData;
using System;

public sealed class RowerData(IFitnessMachineData data) : IRowerData
{
	public IFitnessMachineValue? StrokeRate => data.GetValue(FtmsUuids.StrokeRate);
	public IFitnessMachineValue? StrokeCount => data.GetValue(FtmsUuids.StrokeCount);
	public IFitnessMachineValue? AverageStrokeRate => data.GetValue(FtmsUuids.AverageStrokeRate);
	public IFitnessMachineValue? TotalDistance => data.GetValue(FtmsUuids.TotalDistance);
	public IFitnessMachineValue? InstantaneousPace => data.GetValue(FtmsUuids.InstantaneousPace);
	public IFitnessMachineValue? AveragePace => data.GetValue(FtmsUuids.AveragePace);
	public IFitnessMachineValue? InstantaneousPower => data.GetValue(FtmsUuids.InstantaneousPower);
	public IFitnessMachineValue? AveragePower => data.GetValue(FtmsUuids.AveragePower);
	public IFitnessMachineValue? ResistantLevel => data.GetValue(FtmsUuids.ResistantLevel);
	public IFitnessMachineValue? TotalEnergy => data.GetValue(FtmsUuids.TotalEnergy);
	public IFitnessMachineValue? EnergyPerHour => data.GetValue(FtmsUuids.EnergyPerHour);
	public IFitnessMachineValue? EnergyPerMinute => data.GetValue(FtmsUuids.EnergyPerMinute);
	public IFitnessMachineValue? HeartRate => data.GetValue(FtmsUuids.HeartRate);
	public IFitnessMachineValue? MetabolicEquivalent => data.GetValue(FtmsUuids.MetabolicEquivalent);
	public IFitnessMachineValue? ElapsedTime => data.GetValue(FtmsUuids.ElapsedTime);
	public IFitnessMachineValue? RemainingTime => data.GetValue(FtmsUuids.RemainingTime);

	public IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect() => data.Connect();
	public IFitnessMachineValue? GetValue(Guid uuid) => data.GetValue(uuid);
	public void Dispose() => data.Dispose();
}
