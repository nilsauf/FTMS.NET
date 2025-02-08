namespace FTMS.NET;
using FTMS.NET.Control;
using FTMS.NET.Data;
using FTMS.NET.Exceptions;
using FTMS.NET.State;
using FTMS.NET.Utils;
using System;
using System.Numerics;

public static class FitnessMachineService
{
	public static IFitnessMachineService<TFitnessMachineData> Create<TFitnessMachineData>(
		IFitnessMachineServiceConnection connection)
		where TFitnessMachineData : FitnessMachineData<TFitnessMachineData>
	{
		CheckAvailability();

		TFitnessMachineData data = CreateFitnessMachineData<TFitnessMachineData>(connection);
		CheckType();

		FitnessMachineControl control = new(connection.ControlPointObservable, connection.WriteToControlPoint);
		FitnessMachineStateProvider stateProvider = new(connection.StateObservable);

		return new FitnessMachineService<TFitnessMachineData>(data, control, stateProvider);

		void CheckAvailability()
		{
			var available = connection.ServiceData[2].IsBitSet(0);
			if (available == false)
				throw new FitnessMachineNotAvailableException();
		}

		void CheckType()
		{
			EFitnessMachineType serviceType = ReadType();
			if (serviceType != data.Type)
				throw new InvalidOperationException();
		}

		EFitnessMachineType ReadType()
			=> (EFitnessMachineType)BitOperations.TrailingZeroCount(BitConverter.ToUInt16(connection.ServiceData.AsSpan()[3..]));


	}

	public static TFitnessMachineData CreateFitnessMachineData<TFitnessMachineData>(
		IFitnessMachineServiceConnection connection)
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
