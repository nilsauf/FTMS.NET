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
	public static async Task<IFitnessMachineService<TFitnessMachineData>> CreateAsync<TFitnessMachineData>(
		IFitnessMachineServiceConnection connection)
		where TFitnessMachineData : FitnessMachineData<TFitnessMachineData>
	{
		CheckAvailability();

		TFitnessMachineData data = await CreateFitnessMachineDataAsync<TFitnessMachineData>(connection);

		IFitnessMachineControl control = await CreateFitnessMachineControlAsync(connection);
		IFitnessMachineStateProvider stateProvider = await CreateFitnessMachineStateProviderAsync(connection);

		return new FitnessMachineService<TFitnessMachineData>(data, control, stateProvider);

		void CheckAvailability()
		{
			var available = connection.ServiceData[2].IsBitSet(0);
			if (available == false)
				throw new FitnessMachineNotAvailableException();
		}
	}

	public static async Task<TFitnessMachineData> CreateFitnessMachineDataAsync<TFitnessMachineData>(
		IFitnessMachineServiceConnection connection)
		where TFitnessMachineData : FitnessMachineData<TFitnessMachineData>
	{
		var fitnessMaschineType = ReadType();
		var dataCharacteristicId = GetDataCharacteristicId();
		var dataObservable = await connection.GetCharacteristicAsync(dataCharacteristicId);

		var fitnessMaschineData = TryCreateData();

		CheckType();

		return fitnessMaschineData;

		EFitnessMachineType ReadType()
			=> (EFitnessMachineType)BitOperations.TrailingZeroCount(BitConverter.ToUInt16(connection.ServiceData.AsSpan()[3..]));

		Guid GetDataCharacteristicId() => fitnessMaschineType switch
		{
			EFitnessMachineType.Threadmill => throw new NotImplementedException(),
			EFitnessMachineType.CrossTrainer => throw new NotImplementedException(),
			EFitnessMachineType.StepClimber => throw new NotImplementedException(),
			EFitnessMachineType.StairClimber => throw new NotImplementedException(),
			EFitnessMachineType.Rower => throw new NotImplementedException(),
			EFitnessMachineType.IndoorBike => FtmsUuids.IndoorBikeData,
			_ => throw new InvalidOperationException()
		};

		TFitnessMachineData TryCreateData()
		{
			try
			{
				return Activator.CreateInstance(typeof(TFitnessMachineData), dataObservable)
					as TFitnessMachineData ?? throw new NullReferenceException();
			}
			catch (Exception ex)
			{
				throw new FitnessMachineDataCreationException(ex);
			}
		}

		void CheckType()
		{
			if (fitnessMaschineType != fitnessMaschineData.Type)
				throw new InvalidOperationException();
		}
	}

	public static async Task<IFitnessMachineControl> CreateFitnessMachineControlAsync(
		IFitnessMachineServiceConnection connection)
	{
		var controlPointCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.ControlPoint);
		return new FitnessMachineControl(
			controlPointCharacteristic.ObserveValue(),
			controlPointCharacteristic.WriteValueAsync);
	}

	public static async Task<IFitnessMachineStateProvider> CreateFitnessMachineStateProviderAsync(
		IFitnessMachineServiceConnection connection)
	{
		var stateCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.State);
		return new FitnessMachineStateProvider(stateCharacteristic.ObserveValue());
	}
}
