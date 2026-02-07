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
			.Where(rule => flagFields.IsBitSet(rule.BitPosition) == rule.CheckIfBitIsSet)
			.Select(rule =>
			{
                long readValue = ReadValue(rule.RawValueType);
				double calculatedValue = rule.Calculation.Calculate(readValue);
				return new FitnessMachineValue(
					rule.ValueUuid,
					calculatedValue,
					FtmsUuids.GetName(rule.ValueUuid));
			})];

        long ReadValue(RawValueType rawValueType) => rawValueType switch
        {
            RawValueType.Byte => this.dataReader.ReadByte(),
            RawValueType.SByte => this.dataReader.ReadSByte(),
            RawValueType.Short => this.dataReader.ReadInt16(),
            RawValueType.UShort => this.dataReader.ReadUInt16(),
            RawValueType.Int => this.dataReader.ReadInt32(),
            RawValueType.UInt => this.dataReader.ReadUInt32(),
            //RawValueType.Long => this.dataReader.ReadInt64(),
            //RawValueType.ULong => (long)this.dataReader.ReadUInt64(),
            //RawValueType.Float => (long)this.dataReader.ReadSingle(),
            //RawValueType.Double => (long)this.dataReader.ReadDouble(),
            RawValueType.UInt24 => this.ReadUInt24(),
            _ => throw new NotSupportedException($"Unsupported RawValueType: {rawValueType}")
        };
	}

	private UInt24 ReadUInt24()
	{
		byte b0 = this.dataReader.ReadByte();
		byte b1 = this.dataReader.ReadByte();
		byte b2 = this.dataReader.ReadByte();
		return new UInt24(b0, b1, b2);
	}

	public void Dispose()
	{
		this.dataReader.Dispose();
		this.dataStream.Dispose();
		GC.SuppressFinalize(this);
	}
}
