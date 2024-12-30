namespace FTMS.NET.Data;

using FTMS.NET;

public interface IFitnessMachineDataReader
{
	EFitnessMachineType Type { get; }
	IEnumerable<IFitnessMachineValue> Read(byte[] data);
}
