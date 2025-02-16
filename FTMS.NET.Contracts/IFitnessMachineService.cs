namespace FTMS.NET;

using FTMS.NET.Control;
using FTMS.NET.Data;
using FTMS.NET.Features;
using FTMS.NET.State;

public interface IFitnessMachineService : IFitnessMachineControl, IFitnessMachineStateProvider, IFitnessMachineData, IDisposable
{
	EFitnessMachineType Type { get; }

	IFitnessMachineData Data { get; }

	IFitnessMachineStateProvider State { get; }

	IFitnessMachineControl Control { get; }

	IFitnessMachineFeatures Features { get; }
}
