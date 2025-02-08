namespace FTMS.NET.State;
public enum EStateOpCode
{
	Reset = 0x01,
	FitnessMachineStoppedOrPausedByTheUser = 0x02,
	FitnessMachineStoppedBySafetyKey = 0x03,
	FitnessMachineStartedOrResumedByTheUser = 0x04,
	TargetSpeedChanged = 0x05,
	TargetInclineChanged = 0x06,
	TargetResistanceLevelChanged = 0x07,
	TargetPowerChanged = 0x08,
	TargetHeartRateChanged = 0x09,
	TargetedExpendedEnergyChanged = 0x0A,
	TargetedNumberOfStepsChanged = 0x0B,
	TargetedNumberOfStridesChanged = 0x0C,
	TargetedDistanceChanged = 0x0D,
	TargetedTrainingTimeChanged = 0x0E,
	TargetedTimeInTwoHeartRateZonesChanged = 0x0F,
	TargetedTimeInThreeHeartRateZonesChanged = 0x10,
	TargetedTimeInFiveHeartRateZonesChanged = 0x11,
	IndoorBikeSimulationParametersChanged = 0x12,
	WheelCircumferenceChanged = 0x13,
	SpinDownState = 0x14,
	TargetedCadenceChanged = 0x15,

	ControlPermissionLost = 0xFF
}
