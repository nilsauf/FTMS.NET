namespace FTMS.NET.Data;

using DynamicData;

public interface IFitnessMachineData : IDisposable
{
	IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect();

	IFitnessMachineValue? GetValue(Guid uuid);
}
