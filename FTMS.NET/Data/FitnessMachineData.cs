namespace FTMS.NET.Data;
using DynamicData;
using System;
using System.Reactive.Linq;

internal sealed class FitnessMachineData(
		IObservable<byte[]> observeData,
		FitnessMachineDataReader dataReader)
	: IFitnessMachineData
{
	private readonly IObservableCache<IFitnessMachineValue, Guid> valueCache = observeData
		.Select(dataReader.Read)
		.ToObservableChangeSet(v => v.Uuid)
		.AsObservableCache();

	public IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect()
		=> this.valueCache.Connect();

	public IFitnessMachineValue? GetValue(Guid uuid)
		=> this.valueCache.KeyValues.GetValueOrDefault(uuid);

	public void Dispose() => this.valueCache.Dispose();
}
