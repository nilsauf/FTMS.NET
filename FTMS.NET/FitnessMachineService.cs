namespace FTMS.NET;

using DynamicData;
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
		=> this.Control.Execute(request);

	public IObservable<IFitnessMachineState> ObserveMachineState()
		=> this.State.ObserveMachineState();

	public IObservable<ITrainingState> ObserveTrainingState()
		=> this.State.ObserveTrainingState();

	public IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect()
		=> this.Data.Connect();

	public IFitnessMachineValue? GetValue(Guid uuid)
		=> this.Data.GetValue(uuid);

	public void Dispose()
	{
		this.Data.Dispose();
		this.Control.Dispose();
		this.State.Dispose();
	}
}
