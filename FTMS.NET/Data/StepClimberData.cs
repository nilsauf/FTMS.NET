namespace FTMS.NET.Data;

using DynamicData;
using System;

public sealed class StepClimberData(IFitnessMachineData data) : IStepClimberData
{
	public IFitnessMachineValue? Floors => data.GetValue(FtmsUuids.Floors);
	public IFitnessMachineValue? StepCount => data.GetValue(FtmsUuids.StepCount);
	public IFitnessMachineValue? StepsPerMinute => data.GetValue(FtmsUuids.StepsPerMinute);
	public IFitnessMachineValue? AverageStepRate => data.GetValue(FtmsUuids.AverageStepRate);
	public IFitnessMachineValue? PositiveElevationGain => data.GetValue(FtmsUuids.PositiveElevationGain);
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
