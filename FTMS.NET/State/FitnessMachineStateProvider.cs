namespace FTMS.NET.State;
using FTMS.NET.Utils;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

internal sealed class FitnessMachineStateProvider : IFitnessMachineStateProvider
{
	private readonly IObservable<IFitnessMachineState> machineStateObservable;
	private readonly IObservable<ITrainingState> trainingStateObservable;
	private readonly CancellationDisposable cancellationDisposable = new();
	private readonly Func<Task<byte[]>> readTrainingStateAsync;

	public FitnessMachineStateProvider(
		IObservable<byte[]> observeMachineState,
		IObservable<byte[]> observeTrainingState,
		Func<Task<byte[]>> readTrainingStateAsync)
	{
		this.machineStateObservable = observeMachineState
			.TakeUntil(this.cancellationDisposable.Token)
			.Select(this.ReadMachineStateData)
			.Publish()
			.RefCount();

		this.trainingStateObservable = observeTrainingState
			.TakeUntil(this.cancellationDisposable.Token)
			.SelectMany(this.ReadTrainingStateDataAsync)
			.Publish()
			.RefCount();

		this.readTrainingStateAsync = readTrainingStateAsync;
	}

	public IObservable<IFitnessMachineState> ObserveMachineState() => this.machineStateObservable.AsObservable();

	public IObservable<ITrainingState> ObserveTrainingState() => this.trainingStateObservable.AsObservable();

	private IFitnessMachineState ReadMachineStateData(byte[] data)
	{
		var opCode = (EStateOpCode)data.First();
		byte[] parameter = [.. data.Skip(1)];

		return new FitnessMachineState(opCode, parameter);
	}

	private async Task<ITrainingState> ReadTrainingStateDataAsync(byte[] data)
	{
		var dataSpan = data.AsSpan();
		var flags = dataSpan[0];
		var trainingStateValue = (ETrainingState)dataSpan[1];
		string? detailString = null;

		if (flags.IsBitSet(0))
			detailString = Encoding.UTF8.GetString(dataSpan[2..]);
		else if (flags.IsBitSet(1))
		{
			var detailBytes = await this.readTrainingStateAsync().ConfigureAwait(false);
			detailString = Encoding.UTF8.GetString(detailBytes);
		}

		return new TrainingState(trainingStateValue, detailString);
	}

	public void Dispose() => this.cancellationDisposable.Dispose();
}
