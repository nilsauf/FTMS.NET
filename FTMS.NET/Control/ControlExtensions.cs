namespace FTMS.NET.Control;

using FTMS.NET.Utils;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public static class ControlExtensions
{
	public static Task RequestControl(this IFitnessMachineControl fitnessMachineControl)
		 => fitnessMachineControl.ExecuteWithoutValue(EControlOpCode.RequestControl);

	public static Task Reset(this IFitnessMachineControl fitnessMachineControl)
		=> fitnessMachineControl.ExecuteWithoutValue(EControlOpCode.Reset);

	public static Task SetTargetSpeed(this IFitnessMachineControl fitnessMachineControl, ushort speed)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetSpeed, speed);

	public static Task SetTargetInclination(this IFitnessMachineControl fitnessMachineControl, short inclination)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetInclination, inclination);

	public static Task SetTargetResistanceLevel(this IFitnessMachineControl fitnessMachineControl, byte resistanceLevel)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetResistanceLevel, resistanceLevel);

	public static Task SetTargetPower(this IFitnessMachineControl fitnessMachineControl, short power)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetPower, power);

	public static Task SetTargetHeartRate(this IFitnessMachineControl fitnessMachineControl, byte beatsPerMinute)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetHeartRate, beatsPerMinute);

	public static Task StartOrResume(this IFitnessMachineControl fitnessMachineControl)
		=> fitnessMachineControl.ExecuteWithoutValue(EControlOpCode.StartOrResume);

	public static Task Stop(this IFitnessMachineControl fitnessMachineControl)
		=> fitnessMachineControl.StopOrPause(0x01);

	public static Task Pause(this IFitnessMachineControl fitnessMachineControl)
		=> fitnessMachineControl.StopOrPause(0x02);

	public static Task SetTargetedExpendedEnergy(this IFitnessMachineControl fitnessMachineControl, ushort expendedEnergy)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetedExpendedEnergy, expendedEnergy);

	public static Task SetTargetedNumberOfSteps(this IFitnessMachineControl fitnessMachineControl, ushort steps)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetedNumberOfSteps, steps);

	public static Task SetTargetedNumberOfStrides(this IFitnessMachineControl fitnessMachineControl, ushort strides)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetedNumberOfStrides, strides);

	public static Task SetTargetedDistance(this IFitnessMachineControl fitnessMachineControl, UInt24 distance)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetedDistance, distance);

	public static Task SetTargetedTrainingTime(this IFitnessMachineControl fitnessMachineControl, ushort time)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetedTrainingTime, time);

	public static Task SetTargetedTimeInTwoHeartRateZones(this IFitnessMachineControl fitnessMachineControl, ushort timeInFatBurnZone, ushort timeInFitnessZone)
		=> fitnessMachineControl.ExecuteWithMultipleValues(EControlOpCode.SetTargetedTimeInTwoHeartRateZones, timeInFatBurnZone, timeInFitnessZone);

	public static Task SetTargetedTimeInThreeHeartRateZones(this IFitnessMachineControl fitnessMachineControl, ushort timeInLightZone, ushort timeInModerateZone, ushort timeInHardZone)
		=> fitnessMachineControl.ExecuteWithMultipleValues(EControlOpCode.SetTargetedTimeInThreeHeartRateZones, timeInLightZone, timeInModerateZone, timeInHardZone);

	public static Task SetTargetedTimeInFiveHeartRateZones(this IFitnessMachineControl fitnessMachineControl, ushort timeInVeryLightZone, ushort timeInLightZone, ushort timeInModerateZone, ushort timeInHardZone, ushort timeInMaximumZone)
		=> fitnessMachineControl.ExecuteWithMultipleValues(EControlOpCode.SetTargetedTimeInFiveHeartRateZones, timeInVeryLightZone, timeInLightZone, timeInModerateZone, timeInHardZone, timeInMaximumZone);

	public static Task SetIndoorBikeSimulationParameters(this IFitnessMachineControl fitnessMachineControl, short windspeed, short grade, byte crr, byte cw)
	{
		var windspeedBytes = BitConverter.GetBytes(windspeed);
		var gradeBytes = BitConverter.GetBytes(grade);

		byte[] parameterBytes = [.. windspeedBytes, .. gradeBytes, crr, cw];
		var request = new ControlRequest(EControlOpCode.SetIndoorBikeSimulationParameters, parameterBytes);
		return fitnessMachineControl.Execute(request);
	}

	public static Task SetWheelCircumference(this IFitnessMachineControl fitnessMachineControl, ushort wheelCircumference)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetWheelCircumference, wheelCircumference);

	public static async Task<(ushort targetSpeedLow, ushort targetSpeedHigh)> StartSpinDownControl(this IFitnessMachineControl fitnessMachineControl)
	{
		var response = await fitnessMachineControl.SendSpinDownControl(0x01);
		using MemoryStream responseStream = new(response.ResponseParameter);
		using BinaryReader responseReader = new(responseStream);

		ushort targetSpeedLow = responseReader.ReadUInt16();
		ushort targetSpeedHigh = responseReader.ReadUInt16();

		return (targetSpeedLow, targetSpeedHigh);
	}

	public static Task IgnoreSpinDownControl(this IFitnessMachineControl fitnessMachineControl)
		=> fitnessMachineControl.SendSpinDownControl(0x02);

	private static Task<ControlResponse> SendSpinDownControl(this IFitnessMachineControl fitnessMachineControl, byte startOrIgnore)
	{
		var request = new ControlRequest(EControlOpCode.SpinDownControl, [startOrIgnore]);
		return fitnessMachineControl.Execute(request);
	}

	public static Task SetTargetedCadence(this IFitnessMachineControl fitnessMachineControl, ushort cadence)
		=> fitnessMachineControl.ExecuteWithValue(EControlOpCode.SetTargetedCadence, cadence);

	private static Task<ControlResponse> ExecuteWithoutValue(this IFitnessMachineControl fitnessMachineControl, EControlOpCode opCode)
	{
		var request = new ControlRequest(opCode, []);
		return fitnessMachineControl.Execute(request);
	}

	private static Task<ControlResponse> ExecuteWithValue<T>(this IFitnessMachineControl fitnessMachineControl, EControlOpCode opCode, T value)
	{
		var valueBytes = GetBytes();
		var request = new ControlRequest(opCode, valueBytes);
		return fitnessMachineControl.Execute(request);

		byte[] GetBytes()
		{
			if (typeof(T).IsPrimitive)
			{
				int size = Marshal.SizeOf<T>();
				byte[] bytes = new byte[size];
				Unsafe.As<byte, T>(ref bytes[0]) = value;
				return bytes;
			}

			throw new InvalidOperationException();
		}
	}

	private static Task<ControlResponse> ExecuteWithMultipleValues(
		this IFitnessMachineControl fitnessMachineControl,
		EControlOpCode opCode,
		params ushort[] values)
	{
		var valuesBytes = values.SelectMany(BitConverter.GetBytes).ToArray();
		var request = new ControlRequest(opCode, valuesBytes);
		return fitnessMachineControl.Execute(request);
	}

	private static Task<ControlResponse> StopOrPause(this IFitnessMachineControl fitnessMachineControl, byte stopOrPause)
	{
		var request = new ControlRequest(EControlOpCode.StopOrPause, [stopOrPause]);
		return fitnessMachineControl.Execute(request);
	}
}
