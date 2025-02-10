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

	private static FitnessMachineStateParameter ReadTargetSpeed(byte[] rawData)
		=> new("Target Speed", FitnessMachineUnit.KilometersPerHour, BitConverter.ToUInt16(rawData) * 0.01);

	private static FitnessMachineStateParameter ReadTargetIncline(byte[] rawData)
		=> new("Target Incline", FitnessMachineUnit.Percent, BitConverter.ToUInt16(rawData) * 0.1);

	private static FitnessMachineStateParameter ReadTargetResistanceLevel(byte[] rawData)
		=> new("Target Resistance Level", FitnessMachineUnit.None, rawData[0]);

	private static FitnessMachineStateParameter ReadTargetPower(byte[] rawData)
		=> new("Target Power", FitnessMachineUnit.Watt, BitConverter.ToInt16(rawData));

	private static FitnessMachineStateParameter ReadTargetHeartRate(byte[] rawData)
		=> new("Target Heart Rate", FitnessMachineUnit.BeatsPerMinute, rawData[0]);

	private static FitnessMachineStateParameter ReadTargetedExpendedEnergy(byte[] rawData)
		=> new("Targeted Expended Energy", FitnessMachineUnit.Calories, BitConverter.ToUInt16(rawData));

	private static FitnessMachineStateParameter ReadTargetedNumberOfSteps(byte[] rawData)
		=> new("Targeted Number Of Steps", FitnessMachineUnit.Steps, BitConverter.ToUInt16(rawData));

	private static FitnessMachineStateParameter ReadTargetedNumberOfStrides(byte[] rawData)
		=> new("Targeted Number Of Strides", FitnessMachineUnit.Stride, BitConverter.ToUInt16(rawData));

	private static FitnessMachineStateParameter ReadTargetedDistance(byte[] rawData)
		=> new("Targeted Distance", FitnessMachineUnit.Meters, new UInt24(BitConverter.ToUInt16(rawData)));

	private static FitnessMachineStateParameter ReadTargetedTrainingTime(byte[] rawData)
		=> new("Targeted Training Time", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(rawData));

	private static IEnumerable<FitnessMachineStateParameter> ReadTargetedTimeInTwoHeartRateZones(byte[] rawData)
	{
		var dataSpan = rawData.AsSpan();
		return [
			new("Targeted Time in Fat Burn Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[..2])),
			new("Targeted Time in Fitness Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[2..]))
		];
	}

	private static IEnumerable<FitnessMachineStateParameter> ReadTargetedTimeInThreeHeartRateZones(byte[] rawData)
	{
		var dataSpan = rawData.AsSpan();
		return [
			new("Targeted Time in Light Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[..2])),
			new("Targeted Time in Moderate Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[2..4])),
			new("Targeted Time in Hard Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[4..]))
		];
	}

	private static IEnumerable<FitnessMachineStateParameter> ReadTargetedTimeInFiveHeartRateZones(byte[] rawData)
	{
		var dataSpan = rawData.AsSpan();
		return [
			new("Targeted Time in Very Light Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[..2])),
			new("Targeted Time in Light Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[2..4])),
			new("Targeted Time in Moderate Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[4..6])),
			new("Targeted Time in Hard Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[6..8])),
			new("Targeted Time in Maximum Zone", FitnessMachineUnit.Seconds, BitConverter.ToUInt16(dataSpan[8..]))
		];
	}

	private static IEnumerable<FitnessMachineStateParameter> ReadIndoorBikeSimulationParameters(byte[] rawData)
	{
		var dataSpan = rawData.AsSpan();
		return [
			new("Wind Speed", FitnessMachineUnit.MetersPerSecond, BitConverter.ToInt16(dataSpan[..2]) * 0.001),
			new("Grade", FitnessMachineUnit.Percent, BitConverter.ToInt16(dataSpan[2..4]) * 0.01),
			new("Coefficient of Rolling Resistance", FitnessMachineUnit.None, dataSpan[4] * 0.0001),
			new("Wind Resistance Coefficient", FitnessMachineUnit.KilogramPerMeter, dataSpan[5] * 0.01)
		];
	}

	private static FitnessMachineStateParameter ReadWheelCircumference(byte[] rawData)
		=> new("Wheel Circumference", FitnessMachineUnit.Millimeters, BitConverter.ToUInt16(rawData) * 0.1);

	private static object ReadSpinDownState(byte[] rawData)
		=> (ESpinDownState)rawData[0];

	private static FitnessMachineStateParameter ReadTargetedCadence(byte[] rawData)
		=> new("Targeted Cadence", FitnessMachineUnit.PerMinute, BitConverter.ToUInt16(rawData) * 0.5);
}
