namespace FTMS.NET.Data;
using DynamicData;
using System;
using System.Reactive.Linq;

internal sealed class FitnessMachineData : IFitnessMachineData
{
	private readonly SourceCache<IFitnessMachineValue, Guid> valueCache = new(value => value.Uuid);
	private readonly IDisposable populateSub;

	public FitnessMachineData(IObservable<byte[]> observeData, FitnessMachineDataReader dataReader)
	{
		this.populateSub = this.valueCache.PopulateFrom(observeData.Select(dataReader.Read));
	}

	public IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect()
		=> this.valueCache.Connect();

	public IFitnessMachineValue? GetValue(Guid uuid)
		=> this.valueCache.KeyValues.GetValueOrDefault(uuid);

	public void Dispose()
	{
		this.populateSub.Dispose();
		this.valueCache.Dispose();
	}
}
