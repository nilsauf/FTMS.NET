namespace FTMS.NET;
using System.Threading.Tasks;

using FTMS.NET.Control;
using FTMS.NET.Data;
using FTMS.NET.State;

internal class FitnessMachineService<TFitnessMachineData>(
	TFitnessMachineData data,
	IFitnessMachineControl control,
	IFitnessMachineStateProvider stateProvider)
	: IFitnessMachineService<TFitnessMachineData>
	where TFitnessMachineData : FitnessMachineData<TFitnessMachineData>
{
	public TFitnessMachineData Data { get; } = data;
	public IFitnessMachineControl Control { get; } = control;
	public IFitnessMachineStateProvider State { get; } = stateProvider;

	IFitnessMachineData IFitnessMachineService.Data => this.Data;

	public Task<ControlResponse> Execute(ControlRequest request)
	{
		ArgumentNullException.ThrowIfNull(request);

		return this.Control.Execute(request);
	}

	public IObservable<IFitnessMachineState> Connect()
		=> this.State.Connect();

	public void Dispose()
	{
		this.Data.Dispose();
		this.Control.Dispose();
		this.State.Dispose();
		GC.SuppressFinalize(this);
	}
}
