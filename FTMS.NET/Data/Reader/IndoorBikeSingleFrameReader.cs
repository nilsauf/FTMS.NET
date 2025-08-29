namespace FTMS.NET.Data.Reader;

using FTMS.NET.Utils;
using SourceGeneration.Reflection;
using System;
using System.Collections.Generic;

[SourceReflection]
internal sealed class IndoorBikeSingleFrameReader(byte[] dataFrame) : SingleFrameReader(dataFrame)
{
	protected override Dictionary<Guid, ValueCalculation> ValueCalculations { get; } = new()
	{
		{ FtmsUuids.IndoorBike.InstantaneousSpeed, new(1, -2, 0) },
		{ FtmsUuids.IndoorBike.AverageSpeed, new(1, -2, 0) },
		{ FtmsUuids.IndoorBike.InstantaneousCadence, new(1, 0, -1) },
		{ FtmsUuids.IndoorBike.AverageCadence, new(1, 0, -1) },
		{ FtmsUuids.IndoorBike.TotalDistance, new(1, 0, 0) },
		{ FtmsUuids.IndoorBike.ResistantLevel, new(1, 1, 0) },
		{ FtmsUuids.IndoorBike.InstantaneousPower, new(1, 0, 0) },
		{ FtmsUuids.IndoorBike.AveragePower, new(1, 0, 0) },
		{ FtmsUuids.IndoorBike.TotalEnergy, new(1, 0, 0) },
		{ FtmsUuids.IndoorBike.EnergyPerHour, new(1, 0, 0) },
		{ FtmsUuids.IndoorBike.EnergyPerMinute, new(1, 0, 0) },
		{ FtmsUuids.IndoorBike.HeartRate, new(1, 0, 0) },
		{ FtmsUuids.IndoorBike.MetabolicEquivalent, new(1, -1, 0) },
		{ FtmsUuids.IndoorBike.ElapsedTime, new(1, 0, 0) },
		{ FtmsUuids.IndoorBike.RemainingTime, new(1, 0, 0) },
	};

	protected override IEnumerable<IFitnessMachineValue?> ReadFrameCore()
	{
		yield return this.IfBitIsNotSet(FtmsUuids.IndoorBike.InstantaneousSpeed, 0, this.TryReadUInt16);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.AverageSpeed, 1, this.TryReadUInt16);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.InstantaneousCadence, 2, this.TryReadUInt16);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.AverageCadence, 3, this.TryReadUInt16);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.TotalDistance, 4, this.TryReadUInt32);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.ResistantLevel, 5, this.TryReadByte);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.InstantaneousPower, 6, this.TryReadInt16);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.AveragePower, 7, this.TryReadInt16);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.TotalEnergy, 8, this.TryReadUInt16);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.EnergyPerHour, 8, this.TryReadUInt16);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.EnergyPerMinute, 8, this.TryReadByte);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.HeartRate, 9, this.TryReadByte);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.MetabolicEquivalent, 10, this.TryReadByte);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.ElapsedTime, 11, this.TryReadUInt16);
		yield return this.IfBitIsSet(FtmsUuids.IndoorBike.RemainingTime, 12, this.TryReadUInt16);
	}
}

