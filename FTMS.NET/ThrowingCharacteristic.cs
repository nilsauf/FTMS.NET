namespace FTMS.NET;

using FTMS.NET.Exceptions;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

internal sealed class ThrowingCharacteristic(Guid uuid) : IFitnessMachineCharacteristic
{
	private readonly NeededCharacteristicNotAvailableException exception = new(uuid);

	public Guid Id => throw this.exception;

	public IObservable<byte[]> ObserveValue() => Observable.Throw<byte[]>(this.exception);

	public Task<byte[]> ReadValueAsync() => throw this.exception;

	public Task WriteValueAsync(byte[] value) => throw this.exception;
}
