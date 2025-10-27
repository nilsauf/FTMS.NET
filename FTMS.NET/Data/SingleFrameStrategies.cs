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
			new(FtmsUuids.IndoorBike.InstantaneousSpeed, false, 0, typeof(ushort), new(1, -2, 0)),
			new(FtmsUuids.IndoorBike.AverageSpeed, true, 1, typeof(ushort), new(1, -2, 0)),
			new(FtmsUuids.IndoorBike.InstantaneousCadence, true, 2, typeof(ushort), new(1, 0, -1)),
			new(FtmsUuids.IndoorBike.AverageCadence, true, 3, typeof(ushort), new(1, 0, -1)),
			new(FtmsUuids.IndoorBike.TotalDistance, true, 4, typeof(uint), new(1, 0, 0)),
			new(FtmsUuids.IndoorBike.ResistantLevel, true, 5, typeof(byte), new(1, 1, 0)),
			new(FtmsUuids.IndoorBike.InstantaneousPower, true, 6, typeof(short), new(1, 0, 0)),
			new(FtmsUuids.IndoorBike.AveragePower, true, 7, typeof(short), new(1, 0, 0)),
			new(FtmsUuids.IndoorBike.TotalEnergy, true, 8, typeof(ushort), new(1, 0, 0)),
			new(FtmsUuids.IndoorBike.EnergyPerHour, true, 8, typeof(ushort), new(1, 0, 0)),
			new(FtmsUuids.IndoorBike.EnergyPerMinute, true, 8, typeof(byte), new(1, 0, 0)),
			new(FtmsUuids.IndoorBike.HeartRate, true, 9, typeof(byte), new(1, 0, 0)),
			new(FtmsUuids.IndoorBike.MetabolicEquivalent, true, 10, typeof(byte), new(1, -1, 0)),
			new(FtmsUuids.IndoorBike.ElapsedTime, true, 11, typeof(ushort), new(1, 0, 0)),
			new(FtmsUuids.IndoorBike.RemainingTime, true, 12, typeof(ushort), new(1, 0, 0))
		]
	};
}
