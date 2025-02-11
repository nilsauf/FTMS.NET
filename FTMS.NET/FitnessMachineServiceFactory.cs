namespace FTMS.NET;
using FTMS.NET.Control;
using FTMS.NET.Data;
using FTMS.NET.Data.Reader;
using FTMS.NET.Exceptions;
using FTMS.NET.State;
using FTMS.NET.Utils;
using System;
using System.Numerics;

public partial class FitnessMachineService
{
	public static async Task<IFitnessMachineService> CreateAsync(
		IFitnessMachineServiceConnection connection)
	{
		CheckAvailability();

		FitnessMachineData data = await CreateFitnessMachineDataAsync(connection);
		FitnessMachineControl control = await CreateFitnessMachineControlAsync(connection);
		FitnessMachineStateProvider stateProvider = await CreateFitnessMachineStateProviderAsync(connection);

		return new FitnessMachineService(data, control, stateProvider);

		void CheckAvailability()
		{
			var available = connection.ServiceData[2].IsBitSet(0);
			if (available == false)
				throw new FitnessMachineNotAvailableException();
		}
	}

	private static async Task<FitnessMachineData> CreateFitnessMachineDataAsync(
		IFitnessMachineServiceConnection connection)
	{
		var fitnessMaschineType = ReadType();
		var dataCharacteristicId = GetDataCharacteristicId();
		var dataCharacteristic = await connection.GetCharacteristicAsync(dataCharacteristicId);

		var fitnessMaschineDataReader = GetDataReader();

		return new FitnessMachineData(dataCharacteristic.ObserveValue(), fitnessMaschineDataReader);

		EFitnessMachineType ReadType()
			=> (EFitnessMachineType)BitOperations.TrailingZeroCount(BitConverter.ToUInt16(connection.ServiceData.AsSpan()[3..]));

		FitnessMachineDataReader GetDataReader() => fitnessMaschineType switch
		{
			EFitnessMachineType.Threadmill => throw new NotImplementedException(),
			EFitnessMachineType.CrossTrainer => throw new NotImplementedException(),
			EFitnessMachineType.StepClimber => throw new NotImplementedException(),
			EFitnessMachineType.StairClimber => throw new NotImplementedException(),
			EFitnessMachineType.Rower => throw new NotImplementedException(),
			EFitnessMachineType.IndoorBike => new IndoorBikeDataReader(),
			_ => throw new InvalidOperationException()
		};

		Guid GetDataCharacteristicId() => fitnessMaschineType switch
		{
			EFitnessMachineType.Threadmill => FtmsUuids.TreadmillData,
			EFitnessMachineType.CrossTrainer => FtmsUuids.CrossTrainerData,
			EFitnessMachineType.StepClimber => FtmsUuids.StepClimberData,
			EFitnessMachineType.StairClimber => FtmsUuids.StairClimberData,
			EFitnessMachineType.Rower => FtmsUuids.RowerData,
			EFitnessMachineType.IndoorBike => FtmsUuids.IndoorBikeData,
			_ => throw new InvalidOperationException()
		};
	}

	private static async Task<FitnessMachineControl> CreateFitnessMachineControlAsync(
		IFitnessMachineServiceConnection connection)
	{
		var controlPointCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.ControlPoint);
		return new FitnessMachineControl(
			controlPointCharacteristic.ObserveValue(),
			controlPointCharacteristic.WriteValueAsync);
	}

	private static async Task<FitnessMachineStateProvider> CreateFitnessMachineStateProviderAsync(
		IFitnessMachineServiceConnection connection)
	{
		var machineStateCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.MachineState);
		var trainingStateCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.TrainingState);
		return new FitnessMachineStateProvider(
			machineStateCharacteristic.ObserveValue(),
			trainingStateCharacteristic.ObserveValue(),
			trainingStateCharacteristic.ReadValueAsync);
	}
}
