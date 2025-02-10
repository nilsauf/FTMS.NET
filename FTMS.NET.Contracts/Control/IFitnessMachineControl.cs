namespace FTMS.NET.Control;
using System.Threading.Tasks;

public interface IFitnessMachineControl
{
	Task<ControlResponse> Execute(ControlRequest request);
}
