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

	public IObservable<IFitnessMachineState> ObserveMachineState()
		=> this.machineStateObservable.AsObservable();

	public IObservable<ITrainingState> ObserveTrainingState()
		=> this.trainingStateObservable.AsObservable();

	private IFitnessMachineState ReadMachineStateData(byte[] data)
	{
		var opCode = (EStateOpCode)data.First();
		byte[] parameter = [.. data.Skip(1)];

		return new FitnessMachineState(opCode, parameter);
	}

	private async Task<ITrainingState> ReadTrainingStateDataAsync(byte[] data)
	{
		var flags = data[0];
		var trainingStateValue = (ETrainingState)data[1];
		string? detailString = await ReadDetailString();

		return new TrainingState(trainingStateValue, detailString);

		async Task<string?> ReadDetailString()
		{
			if (flags.IsBitSet(0))
				return Encoding.UTF8.GetString(data.AsSpan()[2..]);
			else if (flags.IsBitSet(1))
			{
				var detailBytes = await this.readTrainingStateAsync();
				return Encoding.UTF8.GetString(detailBytes.AsSpan());
			}
			return null;
		}
	}

	public void Dispose() => this.cancellationDisposable.Dispose();
}
