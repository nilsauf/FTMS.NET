namespace FTMS.NET.Data;
using System.Collections.Generic;

using FTMS.NET;

public abstract class FitnessMachineDataReader : IFitnessMachineDataReader
{
	public abstract EFitnessMachineType Type { get; }

	public IEnumerable<IFitnessMachineValue> Read(byte[] data)
	{
		if (data.Length == 0)
			return [];

		using MemoryStream dataStream = new(data);
		using BinaryReader dataReader = new(dataStream);

		var list = this.ReadCore(dataReader).ToList();

		return list;
	}

	protected IFitnessMachineValue CreateNewValue(Guid uuid, double value)
		=> new FitnessMachineValue(uuid, value, this.GetValueName(uuid));

	protected abstract string GetValueName(Guid uuid);

	protected abstract IEnumerable<IFitnessMachineValue> ReadCore(BinaryReader dataReader);
}