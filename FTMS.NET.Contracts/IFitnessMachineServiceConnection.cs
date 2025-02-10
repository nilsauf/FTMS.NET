namespace FTMS.NET;
using System;
using System.Threading.Tasks;

public interface IFitnessMachineServiceConnection
{
	byte[] ServiceData { get; }

	Task<IFitnessMaschineCharacteristic> GetCharacteristicAsync(Guid id);
}
