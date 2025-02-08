namespace FTMS.NET.State;

using FTMS.NET.Control;
using FTMS.NET.Utils;
using System;

public static class FitnessMachineStateParameterFactory
{
	private static readonly Dictionary<EStateOpCode, Func<byte[], object>> parameterCalculations = new()
	{
		{ EStateOpCode.Reset, ReadEmpty },
		{ EStateOpCode.FitnessMachineStoppedOrPausedByTheUser, ReadStopOrPauseCode },
		{ EStateOpCode.FitnessMachineStoppedBySafetyKey, ReadEmpty },
		{ EStateOpCode.FitnessMachineStartedOrResumedByTheUser, ReadEmpty },
		{ EStateOpCode.TargetSpeedChanged, ReadTargetSpeed },
		{ EStateOpCode.TargetInclineChanged, ReadTargetIncline },
		{ EStateOpCode.TargetResistanceLevelChanged, ReadTargetResistanceLevel },
		{ EStateOpCode.TargetPowerChanged, ReadTargetPower },
		{ EStateOpCode.TargetHeartRateChanged, ReadTargetHeartRate },
		{ EStateOpCode.TargetedExpendedEnergyChanged, ReadTargetedExpendedEnergy },
		{ EStateOpCode.TargetedNumberOfStepsChanged, ReadTargetedNumberOfSteps },
		{ EStateOpCode.TargetedNumberOfStridesChanged, ReadTargetedNumberOfStrides },
		{ EStateOpCode.TargetedDistanceChanged, ReadTargetedDistance },
		{ EStateOpCode.TargetedTrainingTimeChanged, ReadTargetedTrainingTime },
		{ EStateOpCode.TargetedTimeInTwoHeartRateZonesChanged, ReadTargetedTimeInTwoHeartRateZones },
		{ EStateOpCode.TargetedTimeInThreeHeartRateZonesChanged, ReadTargetedTimeInThreeHeartRateZones },
		{ EStateOpCode.TargetedTimeInFiveHeartRateZonesChanged, ReadTargetedTimeInFiveHeartRateZones },
		{ EStateOpCode.IndoorBikeSimulationParametersChanged, ReadIndoorBikeSimulationParameters },
		{ EStateOpCode.WheelCircumferenceChanged, ReadWheelCircumference },
		{ EStateOpCode.SpinDownState, ReadSpinDownState },
		{ EStateOpCode.TargetedCadenceChanged, ReadTargetedCadence },
		{ EStateOpCode.ControlPermissionLost, ReadEmpty },
	};

	public static IEnumerable<object> ReadParameters(EStateOpCode opCode, byte[] rawData)
	{
		var parameter = parameterCalculations[opCode].Invoke(rawData);
		if (parameter is IEnumerable<object> multipleParameters)
			return multipleParameters;
		return [parameter];
	}

	private static IEnumerable<object> ReadEmpty(byte[] rawData) => [];

	private static object ReadStopOrPauseCode(byte[] rawData)
		=> (EStopOrPauseCode)rawData[0];

	private static FitnessMaschineStateParameter ReadTargetSpeed(byte[] rawData)
		=> new("Target Speed", FitnessMaschineUnit.KilometersPerHour, BitConverter.ToUInt16(rawData) * 0.01);

	private static FitnessMaschineStateParameter ReadTargetIncline(byte[] rawData)
		=> new("Target Incline", FitnessMaschineUnit.Percent, BitConverter.ToUInt16(rawData) * 0.1);

	private static FitnessMaschineStateParameter ReadTargetResistanceLevel(byte[] rawData)
		=> new("Target Resistance Level", FitnessMaschineUnit.None, rawData[0]);

	private static FitnessMaschineStateParameter ReadTargetPower(byte[] rawData)
		=> new("Target Power", FitnessMaschineUnit.Watt, BitConverter.ToInt16(rawData));

	private static FitnessMaschineStateParameter ReadTargetHeartRate(byte[] rawData)
		=> new("Target Heart Rate", FitnessMaschineUnit.BeatsPerMinute, rawData[0]);

	private static FitnessMaschineStateParameter ReadTargetedExpendedEnergy(byte[] rawData)
		=> new("Targeted Expended Energy", FitnessMaschineUnit.Calories, BitConverter.ToUInt16(rawData));

	private static FitnessMaschineStateParameter ReadTargetedNumberOfSteps(byte[] rawData)
		=> new("Targeted Number Of Steps", FitnessMaschineUnit.Steps, BitConverter.ToUInt16(rawData));

	private static FitnessMaschineStateParameter ReadTargetedNumberOfStrides(byte[] rawData)
		=> new("Targeted Number Of Strides", FitnessMaschineUnit.Stride, BitConverter.ToUInt16(rawData));

	private static FitnessMaschineStateParameter ReadTargetedDistance(byte[] rawData)
		=> new("Targeted Distance", FitnessMaschineUnit.Meters, new UInt24(BitConverter.ToUInt16(rawData)));

	private static FitnessMaschineStateParameter ReadTargetedTrainingTime(byte[] rawData)
		=> new("Targeted Training Time", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(rawData));

	private static IEnumerable<FitnessMaschineStateParameter> ReadTargetedTimeInTwoHeartRateZones(byte[] rawData)
	{
		var dataSpan = rawData.AsSpan();
		return [
			new("Targeted Time in Fat Burn Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[..2])),
			new("Targeted Time in Fitness Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[2..]))
		];
	}

	private static IEnumerable<FitnessMaschineStateParameter> ReadTargetedTimeInThreeHeartRateZones(byte[] rawData)
	{
		var dataSpan = rawData.AsSpan();
		return [
			new("Targeted Time in Light Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[..2])),
			new("Targeted Time in Moderate Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[2..4])),
			new("Targeted Time in Hard Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[4..]))
		];
	}

	private static IEnumerable<FitnessMaschineStateParameter> ReadTargetedTimeInFiveHeartRateZones(byte[] rawData)
	{
		var dataSpan = rawData.AsSpan();
		return [
			new("Targeted Time in Very Light Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[..2])),
			new("Targeted Time in Light Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[2..4])),
			new("Targeted Time in Moderate Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[4..6])),
			new("Targeted Time in Hard Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[6..8])),
			new("Targeted Time in Maximum Zone", FitnessMaschineUnit.Seconds, BitConverter.ToUInt16(dataSpan[8..]))
		];
	}

	private static IEnumerable<FitnessMaschineStateParameter> ReadIndoorBikeSimulationParameters(byte[] rawData)
	{
		var dataSpan = rawData.AsSpan();
		return [
			new("Wind Speed", FitnessMaschineUnit.MetersPerSecond, BitConverter.ToInt16(dataSpan[..2]) * 0.001),
			new("Grade", FitnessMaschineUnit.Percent, BitConverter.ToInt16(dataSpan[2..4]) * 0.01),
			new("Coefficient of Rolling Resistance", FitnessMaschineUnit.None, dataSpan[4] * 0.0001),
			new("Wind Resistance Coefficient", FitnessMaschineUnit.KilogramPerMeter, dataSpan[5] * 0.01)
		];
	}

	private static FitnessMaschineStateParameter ReadWheelCircumference(byte[] rawData)
		=> new("Wheel Circumference", FitnessMaschineUnit.Millimeters, BitConverter.ToUInt16(rawData) * 0.1);

	private static object ReadSpinDownState(byte[] rawData)
		=> (ESpinDownState)rawData[0];

	private static FitnessMaschineStateParameter ReadTargetedCadence(byte[] rawData)
		=> new("Targeted Cadence", FitnessMaschineUnit.PerMinute, BitConverter.ToUInt16(rawData) * 0.5);
}
