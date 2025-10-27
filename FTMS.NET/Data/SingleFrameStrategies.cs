namespace FTMS.NET.Data;

using System;

internal static class SingleFrameStrategies
{
	private readonly static Dictionary<EFitnessMachineType, Lazy<SingleFrameStrategy>> strategies = new()
	{
		{ EFitnessMachineType.Threadmill, new(() => throw new NotImplementedException()) },
		{ EFitnessMachineType.CrossTrainer, new(() => throw new NotImplementedException()) },
		{ EFitnessMachineType.StepClimber, new(() => throw new NotImplementedException()) },
		{ EFitnessMachineType.StairClimber, new(() => throw new NotImplementedException()) },
		{ EFitnessMachineType.Rower, new(() => throw new NotImplementedException()) },
		{ EFitnessMachineType.IndoorBike, new(CreateIndoorBikeStrategy) }
	};

	public static SingleFrameStrategy GetFor(EFitnessMachineType type)
		=> strategies.GetValueOrDefault(type)?.Value
			?? throw new NotSupportedException($"No SingleFrameStrategy defined for Fitness Machine Type: {type}");

	private static SingleFrameStrategy CreateIndoorBikeStrategy() => new()
	{
		FlagFieldLength = 2,
		SingleValueRules =
		[
			new(FtmsUuids.InstantaneousSpeed, false, 0, typeof(ushort), new(1, -2, 0)),
			new(FtmsUuids.AverageSpeed, true, 1, typeof(ushort), new(1, -2, 0)),
			new(FtmsUuids.InstantaneousCadence, true, 2, typeof(ushort), new(1, 0, -1)),
			new(FtmsUuids.AverageCadence, true, 3, typeof(ushort), new(1, 0, -1)),
			new(FtmsUuids.TotalDistance, true, 4, typeof(uint), new(1, 0, 0)),
			new(FtmsUuids.ResistantLevel, true, 5, typeof(byte), new(1, 1, 0)),
			new(FtmsUuids.InstantaneousPower, true, 6, typeof(short), new(1, 0, 0)),
			new(FtmsUuids.AveragePower, true, 7, typeof(short), new(1, 0, 0)),
			new(FtmsUuids.TotalEnergy, true, 8, typeof(ushort), new(1, 0, 0)),
			new(FtmsUuids.EnergyPerHour, true, 8, typeof(ushort), new(1, 0, 0)),
			new(FtmsUuids.EnergyPerMinute, true, 8, typeof(byte), new(1, 0, 0)),
			new(FtmsUuids.HeartRate, true, 9, typeof(byte), new(1, 0, 0)),
			new(FtmsUuids.MetabolicEquivalent, true, 10, typeof(byte), new(1, -1, 0)),
			new(FtmsUuids.ElapsedTime, true, 11, typeof(ushort), new(1, 0, 0)),
			new(FtmsUuids.RemainingTime, true, 12, typeof(ushort), new(1, 0, 0))
		]
	};
}
