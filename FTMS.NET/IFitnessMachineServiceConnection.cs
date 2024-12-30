namespace FTMS.NET;
using System;
using System.Threading.Tasks;

using FTMS.NET.Data;

public interface IFitnessMachineServiceConnection<TFitnessMachineData>
	where TFitnessMachineData : FitnessMachineData<TFitnessMachineData>
{
	byte[] ServiceData { get; }
	IObservable<byte[]> DataObservable { get; }
	IObservable<byte[]> StateObservable { get; }
	IObservable<byte[]> ControlPointObservable { get; }
	Func<byte[], Task> WriteToControlPoint { get; }
}
