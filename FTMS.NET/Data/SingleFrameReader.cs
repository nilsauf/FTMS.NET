namespace FTMS.NET.Data;

using FTMS.NET.Utils;
using System;
using System.Data;

internal sealed class SingleFrameReader : IDisposable
{
	private readonly MemoryStream dataStream;
	private readonly BinaryReader dataReader;
	private readonly SingleFrameStrategy singleFrameStrategy;

	public SingleFrameReader(byte[] dataFrame, SingleFrameStrategy singleFrameStrategy)
	{
		this.dataStream = new(dataFrame);
		this.dataReader = new(this.dataStream);
		this.singleFrameStrategy = singleFrameStrategy;
	}

	public IReadOnlyList<IFitnessMachineValue> ReadFrame()
	{
		byte[] flagFields = this.dataReader.ReadBytes(this.singleFrameStrategy.FlagFieldLength);

		return [.. this.singleFrameStrategy
			.SingleValueRules
			.Where(rule => flagFields.IsBitSet(rule.BitPostion) == rule.CheckIfBitIsSet)
			.Select(rule =>
			{
				long readValue = ReadValue(rule.RawValueType);
				double calculatedValue = rule.Calculation.Calculate(readValue);
				return new FitnessMachineValue(
					rule.ValueUuid,
					calculatedValue,
					FtmsUuids.GetName(rule.ValueUuid));
			})];

		long ReadValue(Type rawValueType) => rawValueType switch
		{
			Type t when t == typeof(byte) => this.dataReader.ReadByte(),
			Type t when t == typeof(sbyte) => this.dataReader.ReadSByte(),
			Type t when t == typeof(short) => this.dataReader.ReadInt16(),
			Type t when t == typeof(ushort) => this.dataReader.ReadUInt16(),
			Type t when t == typeof(int) => this.dataReader.ReadInt32(),
			Type t when t == typeof(uint) => this.dataReader.ReadUInt32(),
			//Type t when t == typeof(long)   => this.dataReader.ReadInt64(),
			//Type t when t == typeof(ulong)  => (long)this.dataReader.ReadUInt64(),
			//Type t when t == typeof(float)  => (long)this.dataReader.ReadSingle(),
			//Type t when t == typeof(double) => (long)this.dataReader.ReadDouble(),
			_ => throw new NotSupportedException($"Unsupported RawValueType: {rawValueType.FullName}")
		};
	}

	public void Dispose()
	{
		this.dataReader.Dispose();
		this.dataStream.Dispose();
		GC.SuppressFinalize(this);
	}
}
