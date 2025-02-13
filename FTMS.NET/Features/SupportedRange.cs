namespace FTMS.NET.Features;

using FTMS.NET;
using FTMS.NET.Utils;

internal record SupportedRange(
	double MinimumValue,
	double MaximumValue,
	double MinimumIncrement,
	FitnessMachineUnit Unit)
	: ISupportedRange
{
	public static ISupportedRange ReadSpeed(byte[] data) =>
		Read(data, RangeCalculations.Speed, FitnessMachineUnit.KilometersPerHour, r => r.ReadUInt16());

	public static ISupportedRange ReadInclination(byte[] data) =>
		Read(data, RangeCalculations.Inclination, FitnessMachineUnit.Percent, r => r.ReadInt16(), r => r.ReadInt16(), r => r.ReadUInt16());

	public static ISupportedRange ReadResistanceLevel(byte[] data) =>
		Read(data, RangeCalculations.ResistanceLevel, FitnessMachineUnit.None, r => r.ReadByte());

	public static ISupportedRange ReadPower(byte[] data) =>
		Read(data, RangeCalculations.Power, FitnessMachineUnit.Watt, r => r.ReadInt16(), r => r.ReadInt16(), r => r.ReadUInt16());

	public static ISupportedRange ReadHeartRateRange(byte[] data) =>
		Read(data, RangeCalculations.HeartRange, FitnessMachineUnit.BeatsPerMinute, r => r.ReadByte());

	private static SupportedRange Read(
		byte[] data,
		ValueCalculation calculation,
		FitnessMachineUnit unit,
		Func<BinaryReader, long> readMinimum,
		Func<BinaryReader, long>? readMaximum = null,
		Func<BinaryReader, long>? readIncrement = null)
	{
		readMaximum ??= readMinimum;
		readIncrement ??= readMinimum;

		using MemoryStream stream = new(data);
		using BinaryReader reader = new(stream);
		var minimumValue = calculation.Calculate(readMinimum(reader));
		var maximumValue = calculation.Calculate(readMaximum(reader));
		var minimumIncrement = calculation.Calculate(readIncrement(reader));
		return new(minimumValue, maximumValue, minimumIncrement, unit);
	}
}
