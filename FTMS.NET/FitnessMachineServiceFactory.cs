namespace FTMS.NET;
using FTMS.NET.Control;
using FTMS.NET.Data;
using FTMS.NET.Data.Reader;
using FTMS.NET.Exceptions;
using FTMS.NET.Features;
using FTMS.NET.State;
using FTMS.NET.Utils;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

public static class FitnessMachineServiceFactory
{
	public static async Task<IFitnessMachineService> CreateFitnessMachineServiceAsync(
		this IFitnessMachineServiceConnection connection)
	{
		connection.EnsureAvailability();
		var fitnessMachineType = connection.ReadType();

		IFitnessMachineFeatures features = await connection.ReadFitnessMachineFeaturesAsync();
		IFitnessMachineData data = await connection.CreateFitnessMachineDataAsync();
		IFitnessMachineControl control = await connection.CreateFitnessMachineControlAsync();
		IFitnessMachineStateProvider stateProvider = await connection.CreateFitnessMachineStateProviderAsync();

		return new FitnessMachineService(fitnessMachineType, data, control, stateProvider, features);
	}

	public static void EnsureAvailability(this IFitnessMachineServiceConnection connection)
	{
		var available = connection.ServiceData[2].IsBitSet(0);
		if (available is false)
			throw new FitnessMachineNotAvailableException();
	}

	public static async Task<IFitnessMachineData> CreateFitnessMachineDataAsync(
		this IFitnessMachineServiceConnection connection)
	{
		var fitnessMaschineType = connection.ReadType();
		var fitnessMaschineDataReader = fitnessMaschineType.GetDataReader();

		var dataCharacteristicId = fitnessMaschineType.GetDataCharacteristicId();
		var dataCharacteristic = await connection.GetCharacteristicAsync(dataCharacteristicId);

		dataCharacteristic.EnsureAvailabieCharacteristic(dataCharacteristicId);
		return new FitnessMachineData(dataCharacteristic.ObserveValue(), fitnessMaschineDataReader);
	}

	public static EFitnessMachineType ReadType(this IFitnessMachineServiceConnection connection)
		=> ((EFitnessMachineType)BitOperations.TrailingZeroCount(
			BitConverter.ToUInt16(
				connection.ServiceData.AsSpan()[3..])))
			.EnsureType();

	private static FitnessMachineDataReader GetDataReader(this EFitnessMachineType fitnessMachineType)
		=> fitnessMachineType switch
		{
			EFitnessMachineType.Threadmill => throw new NotImplementedException(),
			EFitnessMachineType.CrossTrainer => throw new NotImplementedException(),
			EFitnessMachineType.StepClimber => throw new NotImplementedException(),
			EFitnessMachineType.StairClimber => throw new NotImplementedException(),
			EFitnessMachineType.Rower => throw new NotImplementedException(),
			EFitnessMachineType.IndoorBike => new IndoorBikeDataReader(),
			_ => throw new InvalidOperationException()
		};

	public static async Task<IFitnessMachineControl> CreateFitnessMachineControlAsync(
		this IFitnessMachineServiceConnection connection)
	{
		var controlPointCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.ControlPoint);
		controlPointCharacteristic ??= new ThrowingCharacteristic(FtmsUuids.ControlPoint);

		return new FitnessMachineControl(
			controlPointCharacteristic.ObserveValue(),
			controlPointCharacteristic.WriteValueAsync);
	}

	public static async Task<IFitnessMachineStateProvider> CreateFitnessMachineStateProviderAsync(
		this IFitnessMachineServiceConnection connection)
	{
		var machineStateCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.MachineState);
		var trainingStateCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.TrainingState);

		machineStateCharacteristic ??= new ThrowingCharacteristic(FtmsUuids.MachineState);
		trainingStateCharacteristic ??= new ThrowingCharacteristic(FtmsUuids.TrainingState);

		return new FitnessMachineStateProvider(
			machineStateCharacteristic.ObserveValue(),
			trainingStateCharacteristic.ObserveValue(),
			trainingStateCharacteristic.ReadValueAsync);
	}

	public static async Task<IFitnessMachineFeatures> ReadFitnessMachineFeaturesAsync(
		this IFitnessMachineServiceConnection connection)
	{
		var featureCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.Feature);
		featureCharacteristic.EnsureAvailabieCharacteristic(FtmsUuids.Feature);

		var featureData = await featureCharacteristic.ReadValueAsync();
		var featureDataSpan = featureData.AsSpan();

		return new FitnessMachineFeatures(featureDataSpan[..4], featureDataSpan[5..])
		{
			SpeedRange = await ReadSupportedRangeAsync(FtmsUuids.SupportedSpeedRange, SupportedRange.ReadSpeed),
			InclinationRange = await ReadSupportedRangeAsync(FtmsUuids.SupportedInclinationRange, SupportedRange.ReadInclination),
			ResistanceLevelRange = await ReadSupportedRangeAsync(FtmsUuids.SupportedResistanceLevelRange, SupportedRange.ReadResistanceLevel),
			PowerRange = await ReadSupportedRangeAsync(FtmsUuids.SupportedPowerRange, SupportedRange.ReadPower),
			HeartRateRange = await ReadSupportedRangeAsync(FtmsUuids.SupportedHeartRateRange, SupportedRange.ReadHeartRate)
		};

		async Task<ISupportedRange?> ReadSupportedRangeAsync(Guid characteristicId, Func<byte[], ISupportedRange> createRange)
		{
			var characteristic = await connection.GetCharacteristicAsync(characteristicId);
			if (characteristic is null)
				return null;

			var data = await characteristic.ReadValueAsync();
			if (data is null)
				return null;

			return createRange(data);
		}
	}

	public static void EnsureAvailabieCharacteristic(
		[NotNull] this IFitnessMachineCharacteristic? characteristic,
		Guid characteristicUuid)
	{
		if (characteristic is null)
			throw new NeededCharacteristicNotAvailableException(characteristicUuid);
	}
}
