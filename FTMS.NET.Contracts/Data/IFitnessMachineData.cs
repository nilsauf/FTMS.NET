namespace FTMS.NET.Data;

using DynamicData;

using FTMS.NET;

public interface IFitnessMachineData : IDisposable
{
	EFitnessMachineType Type { get; }
	IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect();
}
