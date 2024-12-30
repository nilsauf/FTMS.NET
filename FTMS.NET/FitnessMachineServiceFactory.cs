namespace FTMS.NET;
using System;

using FTMS.NET.Control;
using FTMS.NET.Data;
using FTMS.NET.Exceptions;
using FTMS.NET.State;
using FTMS.NET.Utils;

public static class FitnessMachineService
{
	public static IFitnessMachineService<TFitnessMachineData> Create<TFitnessMachineData>(
		IFitnessMachineServiceConnection<TFitnessMachineData> connection)
		where TFitnessMachineData : FitnessMachineData<TFitnessMachineData>
	{
		CheckAvailability();

		TFitnessMachineData data = CreateFitnessMachineData(connection);
		//CheckType();

		FitnessMachineControl control = new(connection.ControlPointObservable, connection.WriteToControlPoint);
		FitnessMachineStateProvider stateProvider = new(connection.StateObservable);

		return new FitnessMachineService<TFitnessMachineData>(data, control, stateProvider);

		void CheckAvailability()
		{
			var available = connection.ServiceData[2].IsBitSet(0);
			if (available == false)
				throw new FitnessMachineNotAvailableException();
		}

		//void CheckType()
		//{
		//	EFitnessMachineType serviceType = ReadType();
		//	if (serviceType != data.Type)
		//		throw new InvalidOperationException();
		//}

		//EFitnessMachineType ReadType() => (EFitnessMachineType)BitConverter.ToUInt16([.. serviceData[3..]]);


	}

	public static TFitnessMachineData CreateFitnessMachineData<TFitnessMachineData>(
		IFitnessMachineServiceConnection<TFitnessMachineData> connection)
		where TFitnessMachineData : FitnessMachineData<TFitnessMachineData>
	{
		try
		{
			return Activator.CreateInstance(typeof(TFitnessMachineData), connection.DataObservable)
				as TFitnessMachineData ?? throw new NullReferenceException();
		}
		catch (Exception ex)
		{
			throw new FitnessMachineDataCreationException(ex);
		}
	}
}
