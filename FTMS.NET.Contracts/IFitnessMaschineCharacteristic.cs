namespace FTMS.NET;
using System;
using System.Threading.Tasks;

public interface IFitnessMaschineCharacteristic
{
	public Guid Id { get; }

	Task<byte[]> ReadValueAsync();
	Task WriteValueAsync(byte[] value);
	IObservable<byte[]> ObserveValue();
}
