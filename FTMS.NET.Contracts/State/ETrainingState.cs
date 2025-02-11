namespace FTMS.NET.State;
public enum ETrainingState
{
	Other = 0x00,
	Idle = 0x01,
	WarmingUp = 0x02,
	LowIntensityInterval = 0x03,
	HighIntensityInterval = 0x04,
	RecoveryInterval = 0x05,
	Isometric = 0x06,
	HeartRateControl = 0x07,
	FitnessTest = 0x08,
	SpeedLowerOfControlRegion = 0x09,
	SpeedHigherOfControlRegion = 0x0A,
	CoolDown = 0x0B,
	WattControl = 0x0C,
	ManualMode = 0x0D,
	PreWorkout = 0x0E,
	PostWorkout = 0x0F,
}
