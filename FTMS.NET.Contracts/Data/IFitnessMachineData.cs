namespace FTMS.NET.Data;

using DynamicData;

public interface IFitnessMachineData
{
	IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect();

	IFitnessMachineValue? GetValue(Guid uuid);
}
