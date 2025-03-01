﻿namespace FTMS.NET.Data.Reader;

using FTMS.NET.Utils;
using System;
using System.Collections.Generic;

internal class IndoorBikeDataReader : FitnessMachineDataReader
{
	private readonly Dictionary<Guid, ValueCalculation> indoorBikeValueCalculations = new()
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

	protected override IEnumerable<IFitnessMachineValue> ReadCore(BinaryReader dataReader)
	{
		byte[] flagField = dataReader.ReadBytes(2);

		if (TryGetInstantaneousSpeed(out var instantaneousSpeed))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.InstantaneousSpeed, instantaneousSpeed);
		}

		if (TryGetUInt16(FtmsUuids.IndoorBike.AveragePower, 1, out var averageSpeed))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.AverageSpeed, averageSpeed);
		}

		if (TryGetUInt16(FtmsUuids.IndoorBike.InstantaneousCadence, 2, out var instantaneousCadence))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.InstantaneousCadence, instantaneousCadence);
		}

		if (TryGetUInt16(FtmsUuids.IndoorBike.AverageCadence, 3, out var averageCadence))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.AverageCadence, averageCadence);
		}

		if (TryGetUInt32(FtmsUuids.IndoorBike.TotalDistance, 4, out var totalDistance))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.TotalDistance, totalDistance);
		}

		if (TryGetByte(FtmsUuids.IndoorBike.ResistantLevel, 5, out var resistantLevel))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.ResistantLevel, resistantLevel);
		}

		if (TryGetInt16(FtmsUuids.IndoorBike.InstantaneousPower, 6, out var instantaneousPower))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.InstantaneousPower, instantaneousPower);
		}

		if (TryGetInt16(FtmsUuids.IndoorBike.AveragePower, 7, out var averagePower))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.AveragePower, averagePower);
		}

		if (TryGetUInt16(FtmsUuids.IndoorBike.TotalEnergy, 8, out var totalEnergy))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.TotalEnergy, totalEnergy);
		}

		if (TryGetUInt16(FtmsUuids.IndoorBike.EnergyPerHour, 8, out var energyPerHour))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.EnergyPerHour, energyPerHour);
		}

		if (TryGetByte(FtmsUuids.IndoorBike.EnergyPerMinute, 8, out var energyPerMinute))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.EnergyPerMinute, energyPerMinute);
		}

		if (TryGetByte(FtmsUuids.IndoorBike.HeartRate, 9, out var heartRate))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.HeartRate, heartRate);
		}

		if (TryGetByte(FtmsUuids.IndoorBike.MetabolicEquivalent, 10, out var metabolicEquivalent))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.MetabolicEquivalent, metabolicEquivalent);
		}

		if (TryGetUInt16(FtmsUuids.IndoorBike.ElapsedTime, 11, out var elapsedTime))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.ElapsedTime, elapsedTime);
		}

		if (TryGetUInt16(FtmsUuids.IndoorBike.RemainingTime, 12, out var remainingTime))
		{
			yield return this.CreateNewValue(FtmsUuids.IndoorBike.RemainingTime, remainingTime);
		}

		bool TryGetUInt16(
			Guid uui,
			int bitPosition,
			out double value)
			=> TryGetValue(
				uui,
				bitPosition,
				r => r.ReadUInt16(),
				out value);

		bool TryGetUInt32(
			Guid uui,
			int bitPosition,
			out double value)
			=> TryGetValue(
				uui,
				bitPosition,
				r => r.ReadUInt32(),
				out value);

		bool TryGetByte(
			Guid uui,
			int bitPosition,
			out double value)
			=> TryGetValue(
				uui,
				bitPosition,
				r => r.ReadByte(),
				out value);

		bool TryGetInt16(
			Guid uui,
			int bitPosition,
			out double value)
			=> TryGetValue(
				uui,
				bitPosition,
				r => r.ReadInt16(),
				out value);

		bool TryGetValue(
			Guid uui,
			int bitPosition,
			Func<BinaryReader, long> read,
			out double value)
		{
			if (flagField.IsBitSet(bitPosition) &&
				this.indoorBikeValueCalculations.TryGetValue(uui, out var calculation))
			{
				var rawValue = read(dataReader);
				value = calculation.Calculate(rawValue);
				return true;
			}

			value = 0;
			return false;
		}

		bool TryGetInstantaneousSpeed(out double instantaneousSpeed)
		{
			if (flagField.IsBitSet(0) == false &&
				this.indoorBikeValueCalculations.TryGetValue(
					FtmsUuids.IndoorBike.InstantaneousSpeed, out var calculation))
			{
				var rawValue = dataReader.ReadUInt16();
				instantaneousSpeed = calculation.Calculate(rawValue);
				return true;
			}

			instantaneousSpeed = 0;
			return false;
		}
	}
}

