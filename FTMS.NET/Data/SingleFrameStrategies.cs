namespace FTMS.NET.Data;

using FTMS.NET.Utils;
using System;

internal static class SingleFrameStrategies
{
	private readonly static Dictionary<EFitnessMachineType, Lazy<SingleFrameStrategy>> strategies = new()
	{
		{ EFitnessMachineType.Threadmill, new(CreateThreadmillStrategy) },
		{ EFitnessMachineType.CrossTrainer, new(CreateCrossTrainerStrategy) },
		{ EFitnessMachineType.StepClimber, new(CreateStepClimberStrategy) },
		{ EFitnessMachineType.StairClimber, new(CreateStairClimberStrategy) },
		{ EFitnessMachineType.Rower, new(CreateRowerStrategy) },
		{ EFitnessMachineType.IndoorBike, new(CreateIndoorBikeStrategy) }
	};

	public static SingleFrameStrategy GetFor(EFitnessMachineType type)
		=> strategies.GetValueOrDefault(type)?.Value
			?? throw new NotSupportedException($"No SingleFrameStrategy defined for Fitness Machine Type: {type}");

	private static SingleFrameStrategy CreateThreadmillStrategy() => new()
	{
		FlagFieldLength = 2,
		SingleValueRules =
		[
            new(FtmsUuids.InstantaneousSpeed, false, 0, RawValueType.UShort, new(1, -2, 0)),
            new(FtmsUuids.AverageSpeed, true, 1, RawValueType.UShort, new(1, -2, 0)),
            new(FtmsUuids.TotalDistance, true, 2, RawValueType.UInt24, new(1, 0, 0)),
            new(FtmsUuids.Inclination, true, 3, RawValueType.Short, new(1, -1, 0)),
            new(FtmsUuids.RampAngleSetting, true, 3, RawValueType.Short, new(1, -1, 0)),
            new(FtmsUuids.PositiveElevationGain, true, 4, RawValueType.UShort, new(1, -1, 0)),
            new(FtmsUuids.NegativeElevationGain, true, 4, RawValueType.UShort, new(1, -1, 0)),
            new(FtmsUuids.InstantaneousPace, true, 5, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.AveragePace, true, 6, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.TotalEnergy, true, 7, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerHour, true, 7, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerMinute, true, 7, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.HeartRate, true, 8, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.MetabolicEquivalent, true, 9, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.ElapsedTime, true, 10, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.RemainingTime, true, 11, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.ForceOnBelt, true, 12, RawValueType.Short, new(1, 0, 0)),
            new(FtmsUuids.PowerOutput, true, 12, RawValueType.Short, new(1, 0, 0)),
		]
	};

	private static SingleFrameStrategy CreateCrossTrainerStrategy() => new()
	{
		FlagFieldLength = 3,
		SingleValueRules =
		[
            new(FtmsUuids.InstantaneousSpeed, false, 0, RawValueType.UShort, new(1, -2, 0)),
            new(FtmsUuids.AverageSpeed, true, 1, RawValueType.UShort, new(1, -2, 0)),
            new(FtmsUuids.TotalDistance, true, 2, RawValueType.UInt24, new(1, 0, 0)),
            new(FtmsUuids.StepsPerMinute, true, 3, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.AverageStepRate, true, 3, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.StrideCount, true, 4, RawValueType.UShort, new(1, -1, 0)),
            new(FtmsUuids.PositiveElevationGain, true, 5, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.NegativeElevationGain, true, 5, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.Inclination, true, 6, RawValueType.Short, new(1, -1, 0)),
            new(FtmsUuids.RampAngleSetting, true, 6, RawValueType.Short, new(1, -1, 0)),
            new(FtmsUuids.ResistantLevel, true, 7, RawValueType.Byte, new(1, 1, 0)),
            new(FtmsUuids.InstantaneousPower, true, 8, RawValueType.Short, new(1, 0, 0)),
            new(FtmsUuids.AveragePower, true, 9, RawValueType.Short, new(1, 0, 0)),
            new(FtmsUuids.TotalEnergy, true, 10, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerHour, true, 10, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerMinute, true, 10, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.HeartRate, true, 11, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.MetabolicEquivalent, true, 12, RawValueType.Byte, new(1, -1, 0)),
            new(FtmsUuids.ElapsedTime, true, 13, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.RemainingTime, true, 14, RawValueType.UShort, new(1, 0, 0)),
		]
	};

	private static SingleFrameStrategy CreateStepClimberStrategy() => new()
	{
		FlagFieldLength = 2,
		SingleValueRules =
		[
            new(FtmsUuids.Floors, false, 0, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.StepCount, false, 0, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.StepsPerMinute, true, 1, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.AverageStepRate, true, 2, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.PositiveElevationGain, true, 3, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.TotalEnergy, true, 4, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerHour, true, 4, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerMinute, true, 4, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.HeartRate, true, 5, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.MetabolicEquivalent, true, 6, RawValueType.Byte, new(1, -1, 0)),
            new(FtmsUuids.ElapsedTime, true, 7, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.RemainingTime, true, 8, RawValueType.UShort, new(1, 0, 0))
		]
	};

	private static SingleFrameStrategy CreateStairClimberStrategy() => new()
	{
		FlagFieldLength = 2,
		SingleValueRules =
		[
            new(FtmsUuids.Floors, false, 0, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.StepsPerMinute, true, 1, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.AverageStepRate, true, 2, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.PositiveElevationGain, true, 3, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.StrideCount, true, 4, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.TotalEnergy, true, 5, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerHour, true, 5, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerMinute, true, 5, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.HeartRate, true, 6, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.MetabolicEquivalent, true, 7, RawValueType.Byte, new(1, -1, 0)),
            new(FtmsUuids.ElapsedTime, true, 8, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.RemainingTime, true, 9, RawValueType.UShort, new(1, 0, 0))
		]
	};

	private static SingleFrameStrategy CreateRowerStrategy() => new()
	{
		FlagFieldLength = 2,
		SingleValueRules =
		[
            new(FtmsUuids.StrokeRate, false, 0, RawValueType.Byte, new(1, 0, -1)),
            new(FtmsUuids.StrokeCount, false, 0, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.AverageStrokeRate, true, 1, RawValueType.Byte, new(1, 0, -1)),
            new(FtmsUuids.TotalDistance, true, 2, RawValueType.UInt24, new(1, 0, 0)),
            new(FtmsUuids.InstantaneousPace, true, 3, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.AveragePace, true, 4, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.InstantaneousPower, true, 5, RawValueType.Short, new(1, 0, 0)),
            new(FtmsUuids.AveragePower, true, 6, RawValueType.Short, new(1, 0, 0)),
            new(FtmsUuids.ResistantLevel, true, 7, RawValueType.Byte, new(1, 1, 0)),
            new(FtmsUuids.TotalEnergy, true, 8, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerHour, true, 8, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerMinute, true, 8, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.HeartRate, true, 9, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.MetabolicEquivalent, true, 10, RawValueType.Byte, new(1, -1, 0)),
            new(FtmsUuids.ElapsedTime, true, 11, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.RemainingTime, true, 12, RawValueType.UShort, new(1, 0, 0))
		]
	};

	private static SingleFrameStrategy CreateIndoorBikeStrategy() => new()
	{
		FlagFieldLength = 2,
		SingleValueRules =
		[
            new(FtmsUuids.InstantaneousSpeed, false, 0, RawValueType.UShort, new(1, -2, 0)),
            new(FtmsUuids.AverageSpeed, true, 1, RawValueType.UShort, new(1, -2, 0)),
            new(FtmsUuids.InstantaneousCadence, true, 2, RawValueType.UShort, new(1, 0, -1)),
            new(FtmsUuids.AverageCadence, true, 3, RawValueType.UShort, new(1, 0, -1)),
            new(FtmsUuids.TotalDistance, true, 4, RawValueType.UInt24, new(1, 0, 0)),
            new(FtmsUuids.ResistantLevel, true, 5, RawValueType.Byte, new(1, 1, 0)),
            new(FtmsUuids.InstantaneousPower, true, 6, RawValueType.Short, new(1, 0, 0)),
            new(FtmsUuids.AveragePower, true, 7, RawValueType.Short, new(1, 0, 0)),
            new(FtmsUuids.TotalEnergy, true, 8, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerHour, true, 8, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.EnergyPerMinute, true, 8, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.HeartRate, true, 9, RawValueType.Byte, new(1, 0, 0)),
            new(FtmsUuids.MetabolicEquivalent, true, 10, RawValueType.Byte, new(1, -1, 0)),
            new(FtmsUuids.ElapsedTime, true, 11, RawValueType.UShort, new(1, 0, 0)),
            new(FtmsUuids.RemainingTime, true, 12, RawValueType.UShort, new(1, 0, 0))
		]
	};
}
