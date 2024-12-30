namespace FTMS.NET.IndoorBike;
using System.Collections.Generic;
using System.IO;

using FTMS.NET.Data;
using FTMS.NET.Utils;

internal class IndoorBikeDataReader : FitnessMachineDataReader
{
	public override EFitnessMachineType Type => EFitnessMachineType.IndoorBike;

	protected override string GetValueName(Guid uuid)
		=> IndoorBikeDataValueUuids.GetName(uuid);

	protected override IEnumerable<IFitnessMachineValue> ReadCore(BinaryReader dataReader)
	{
		byte[] flagField = dataReader.ReadBytes(2);

		if (TryGetInstantaneousSpeed(out var instantaneousSpeed))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.InstantaneousSpeed, instantaneousSpeed);
		}

		if (TryGetUInt16(IndoorBikeDataValueUuids.AveragePower, 1, out var averageSpeed))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.AverageSpeed, averageSpeed);
		}

		if (TryGetUInt16(IndoorBikeDataValueUuids.InstantaneousCadence, 2, out var instantaneousCadence))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.InstantaneousCadence, instantaneousCadence);
		}

		if (TryGetUInt16(IndoorBikeDataValueUuids.AverageCadence, 3, out var averageCadence))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.AverageCadence, averageCadence);
		}

		if (TryGetUInt32(IndoorBikeDataValueUuids.TotalDistance, 4, out var totalDistance))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.TotalDistance, totalDistance);
		}

		if (TryGetByte(IndoorBikeDataValueUuids.ResistantLevel, 5, out var resistantLevel))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.ResistantLevel, resistantLevel);
		}

		if (TryGetInt16(IndoorBikeDataValueUuids.InstantaneousPower, 6, out var instantaneousPower))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.InstantaneousPower, instantaneousPower);
		}

		if (TryGetInt16(IndoorBikeDataValueUuids.AveragePower, 7, out var averagePower))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.AveragePower, averagePower);
		}

		if (TryGetUInt16(IndoorBikeDataValueUuids.TotalEnergy, 8, out var totalEnergy))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.TotalEnergy, totalEnergy);
		}

		if (TryGetUInt16(IndoorBikeDataValueUuids.EnergyPerHour, 8, out var energyPerHour))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.EnergyPerHour, energyPerHour);
		}

		if (TryGetByte(IndoorBikeDataValueUuids.EnergyPerMinute, 8, out var energyPerMinute))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.EnergyPerMinute, energyPerMinute);
		}

		if (TryGetByte(IndoorBikeDataValueUuids.HeartRate, 9, out var heartRate))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.HeartRate, heartRate);
		}

		if (TryGetByte(IndoorBikeDataValueUuids.MetabolicEquivalent, 10, out var metabolicEquivalent))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.MetabolicEquivalent, metabolicEquivalent);
		}

		if (TryGetUInt16(IndoorBikeDataValueUuids.ElapsedTime, 11, out var elapsedTime))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.ElapsedTime, elapsedTime);
		}

		if (TryGetUInt16(IndoorBikeDataValueUuids.RemainingTime, 12, out var remainingTime))
		{
			yield return this.CreateNewValue(IndoorBikeDataValueUuids.RemainingTime, remainingTime);
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
				IndoorBikeDataValue.Calculations.TryGetValue(uui, out var calculation))
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
				IndoorBikeDataValue.Calculations.TryGetValue(
					IndoorBikeDataValueUuids.InstantaneousSpeed, out var calculation))
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
