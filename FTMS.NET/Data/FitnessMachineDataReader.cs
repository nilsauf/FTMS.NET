namespace FTMS.NET.Data;
using FTMS.NET;
using System.Collections.Generic;

internal abstract class FitnessMachineDataReader
{
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
		=> new FitnessMachineValue(uuid, value, FtmsUuids.GetName(uuid));

	protected abstract IEnumerable<IFitnessMachineValue> ReadCore(BinaryReader dataReader);
}