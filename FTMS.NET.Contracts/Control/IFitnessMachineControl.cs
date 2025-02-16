namespace FTMS.NET.Control;
using System.Threading.Tasks;

public interface IFitnessMachineControl : IDisposable
{
	Task<ControlResponse> Execute(ControlRequest request);
}
