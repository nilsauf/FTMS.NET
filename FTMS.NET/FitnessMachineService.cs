namespace FTMS.NET;
using FTMS.NET.Control;
using FTMS.NET.Data;
using FTMS.NET.Features;
using FTMS.NET.State;
using System.Threading.Tasks;

internal sealed class FitnessMachineService(
		IFitnessMachineData data,
		IFitnessMachineControl control,
		IFitnessMachineStateProvider state,
		IFitnessMachineFeatures features)
	: IFitnessMachineService
{
	public IFitnessMachineData Data { get; } = data;
	public IFitnessMachineControl Control { get; } = control;
	public IFitnessMachineStateProvider State { get; } = state;
	public IFitnessMachineFeatures Features { get; } = features;

	public Task<ControlResponse> Execute(ControlRequest request)
	{
		ArgumentNullException.ThrowIfNull(request);
		return this.Control.Execute(request);
	}

	public IObservable<IFitnessMachineState> ObserveMachineState()
		=> this.State.ObserveMachineState();

	public IObservable<ITrainingState> ObserveTrainingState()
		=> this.State.ObserveTrainingState();

	public void Dispose()
	{
		this.Data.Dispose();
		this.Control.Dispose();
		this.State.Dispose();
	}
}
