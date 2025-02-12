﻿namespace FTMS.NET;
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

public partial class FitnessMachineService
{
	public static async Task<IFitnessMachineService> CreateAsync(
		IFitnessMachineServiceConnection connection)
	{
		CheckAvailability();

		FitnessMachineFeatures features = await ReadFitnessMachineFeatures(connection);
		FitnessMachineData data = await CreateFitnessMachineDataAsync(connection);
		FitnessMachineControl control = await CreateFitnessMachineControlAsync(connection);
		FitnessMachineStateProvider stateProvider = await CreateFitnessMachineStateProviderAsync(connection);

		return new FitnessMachineService(data, control, stateProvider, features);

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
		EnsureType();

		var dataCharacteristicId = GetDataCharacteristicId();
		var dataCharacteristic = await connection.GetCharacteristicAsync(dataCharacteristicId);

		var fitnessMaschineDataReader = GetDataReader();

		EnsureNotNull(dataCharacteristic, dataCharacteristicId);
		return new FitnessMachineData(dataCharacteristic.ObserveValue(), fitnessMaschineDataReader);

		EFitnessMachineType ReadType()
			=> (EFitnessMachineType)BitOperations.TrailingZeroCount(BitConverter.ToUInt16(connection.ServiceData.AsSpan()[3..]));

		void EnsureType()
		{
			if (Enum.IsDefined(fitnessMaschineType) == false)
				throw new FitnessMachineTypeNotDefinedException(fitnessMaschineType);
		}

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
		EnsureNotNull(controlPointCharacteristic, FtmsUuids.ControlPoint);
		return new FitnessMachineControl(
			controlPointCharacteristic.ObserveValue(),
			controlPointCharacteristic.WriteValueAsync);
	}

	private static async Task<FitnessMachineStateProvider> CreateFitnessMachineStateProviderAsync(
		IFitnessMachineServiceConnection connection)
	{
		var machineStateCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.MachineState);
		var trainingStateCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.TrainingState);

		EnsureNotNull(machineStateCharacteristic, FtmsUuids.MachineState);
		trainingStateCharacteristic ??= new ThrowingCharacteristic(FtmsUuids.TrainingState);
		return new FitnessMachineStateProvider(
			machineStateCharacteristic.ObserveValue(),
			trainingStateCharacteristic.ObserveValue(),
			trainingStateCharacteristic.ReadValueAsync);
	}

	private static async Task<FitnessMachineFeatures> ReadFitnessMachineFeatures(
		IFitnessMachineServiceConnection connection)
	{
		var featureCharacteristic = await connection.GetCharacteristicAsync(FtmsUuids.Feature);
		EnsureNotNull(featureCharacteristic, FtmsUuids.Feature);

		var featureData = await featureCharacteristic.ReadValueAsync();
		var featureDataSpan = featureData.AsSpan();

		return new FitnessMachineFeatures(featureDataSpan[..4], featureDataSpan[5..]);
	}

	private static void EnsureNotNull(
		[NotNull] IFitnessMachineCharacteristic? characteristic,
		Guid characteristicUuid)
	{
		if (characteristic is null)
			throw new NeededCharacteristicNotAvailableException(characteristicUuid);
	}
}
