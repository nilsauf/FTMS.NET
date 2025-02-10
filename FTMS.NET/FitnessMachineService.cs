namespace FTMS.NET;
using FTMS.NET.Control;
using FTMS.NET.Data;
using FTMS.NET.State;
using System.Threading.Tasks;

public sealed partial class FitnessMachineService : IFitnessMachineService, IDisposable
{
	private readonly FitnessMachineData data;
	private readonly FitnessMachineControl control;
	private readonly FitnessMachineStateProvider stateProvider;

	public IFitnessMachineData Data => this.data;
	public IFitnessMachineControl Control => this.control;
	public IFitnessMachineStateProvider State => this.stateProvider;

	private FitnessMachineService(
		FitnessMachineData data,
		FitnessMachineControl control,
		FitnessMachineStateProvider stateProvider)
	{
		this.data = data;
		this.control = control;
		this.stateProvider = stateProvider;
	}

	public Task<ControlResponse> Execute(ControlRequest request)
	{
		ArgumentNullException.ThrowIfNull(request);
		return this.Control.Execute(request);
	}

	public IObservable<IFitnessMachineState> Connect()
		=> this.State.Connect();

	public void Dispose()
	{
		this.data.Dispose();
		this.control.Dispose();
		this.stateProvider.Dispose();
	}
}
