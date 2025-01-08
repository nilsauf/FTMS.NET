namespace FTMS.NET;
using System;
using System.Threading.Tasks;

public interface IFitnessMachineServiceConnection
{
	byte[] ServiceData { get; }
	IObservable<byte[]> DataObservable { get; }
	IObservable<byte[]> StateObservable { get; }
	IObservable<byte[]> ControlPointObservable { get; }
	Func<byte[], Task> WriteToControlPoint { get; }
}
