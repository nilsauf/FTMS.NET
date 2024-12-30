namespace FTMS.NET.State;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

using FTMS.NET.Utils;

internal sealed class FitnessMachineStateProvider : IFitnessMachineStateProvider
{
	private readonly IObservable<IFitnessMachineState> stateObservable;
	private readonly CancellationDisposable cancellationDisposable = new();

	public FitnessMachineStateProvider(IObservable<byte[]> observeState)
	{
		this.stateObservable = observeState
			.TakeUntil(this.cancellationDisposable.Token)
			.Select(this.ReadStateData)
			.Publish()
			.RefCount();
	}

	public IObservable<IFitnessMachineState> Connect() => this.stateObservable.AsObservable();

	private IFitnessMachineState ReadStateData(byte[] data)
	{
		var opCode = (EStateOpCode)data.First();
		byte[] parameter = [.. data.Skip(1)];

		return new FitnessMachineState(opCode, parameter);
	}

	public void Dispose()
	{
		this.cancellationDisposable.Dispose();
	}
}
