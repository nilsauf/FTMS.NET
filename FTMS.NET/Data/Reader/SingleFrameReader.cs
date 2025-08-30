namespace FTMS.NET.Data.Reader;

using FTMS.NET.Utils;
using System;

internal abstract class SingleFrameReader : IDisposable
{
	private readonly MemoryStream dataStream;
	private readonly byte[] flagField;

	protected BinaryReader DataReader { get; }
	protected abstract Dictionary<Guid, ValueCalculation> ValueCalculations { get; }

	protected SingleFrameReader(byte[] dataFrame, int flagFieldLength = 2)
	{
		this.dataStream = new(dataFrame);
		this.DataReader = new(this.dataStream);
		this.flagField = this.DataReader.ReadBytes(flagFieldLength);
	}

	public IEnumerable<IFitnessMachineValue> ReadFrame()
	{
		return [.. this.ReadFrameCore().OfType<IFitnessMachineValue>()];
	}

	protected abstract IEnumerable<IFitnessMachineValue?> ReadFrameCore();

	protected IFitnessMachineValue? TryReadUInt16(Guid uuid)
		=> this.TryReadValue(uuid, r => r.ReadUInt16());

	protected IFitnessMachineValue? TryReadUInt32(Guid uuid)
		=> this.TryReadValue(uuid, r => r.ReadUInt32());

	protected IFitnessMachineValue? TryReadByte(Guid uuid)
		=> this.TryReadValue(uuid, r => r.ReadByte());

	protected IFitnessMachineValue? TryReadInt16(Guid uuid)
		=> this.TryReadValue(uuid, r => r.ReadInt16());

	protected IFitnessMachineValue? TryReadInt32(Guid uuid)
		=> this.TryReadValue(uuid, r => r.ReadInt32());

	protected IFitnessMachineValue? TryReadValue(
		Guid uuid,
		Func<BinaryReader, long> read)
	{
		if (this.ValueCalculations.TryGetValue(uuid, out var calculation))
		{
			var rawValue = read(this.DataReader);
			var calculatedValue = calculation.Calculate(rawValue);

			return CreateNewValue(uuid, calculatedValue);
		}

		return null;
	}

	protected IFitnessMachineValue? IfBitIsSet(Guid uuid, int bitPosition, Func<Guid, IFitnessMachineValue?> read)
		=> this.flagField.IsBitSet(bitPosition) is true ? read(uuid) : null;

	protected IFitnessMachineValue? IfBitIsNotSet(Guid uuid, int bitPosition, Func<Guid, IFitnessMachineValue?> read)
		=> this.flagField.IsBitSet(bitPosition) is false ? read(uuid) : null;

	protected static IFitnessMachineValue CreateNewValue(Guid uuid, double value)
		=> new FitnessMachineValue(uuid, value, FtmsUuids.GetName(uuid));

	public void Dispose()
	{
		this.DataReader.Dispose();
		this.dataStream.Dispose();
		GC.SuppressFinalize(this);
	}
}
