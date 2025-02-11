namespace FTMS.NET.Features;
public interface IFitnessMachineFeatures
{
	bool AverageSpeedSupported { get; }
	bool CadenceSupported { get; }
	bool TotalDistanceSupported { get; }
	bool InclinationSupported { get; }
	bool ElevationGainSupported { get; }
	bool PaceSupported { get; }
	bool StepCountSupported { get; }
	bool ResistanceLevelSupported { get; }
	bool StrideCountSupported { get; }
	bool ExpendedEnergySupported { get; }
	bool HeartRateMeasurementSupported { get; }
	bool MetabolicEquivalentSupported { get; }
	bool ElapsedTimeSupported { get; }
	bool RemainingTimeSupported { get; }
	bool PowerMeasurementSupported { get; }
	bool ForceOnBeltAndPowerOutputSupported { get; }
	bool UserDataRetentionSupported { get; }

	bool SpeedTargetSettingSupported { get; }
	bool InclinationTargetSettingSupported { get; }
	bool ResistanceTargetSettingSupported { get; }
	bool PowerTargetSettingSupported { get; }
	bool HeartRateTargetSettingSupported { get; }
	bool TargetedExpendedEnergyConfigurationSupported { get; }
	bool TargetedStepNumberConfigurationSupported { get; }
	bool TargetedStrideNumberConfigurationSupported { get; }
	bool TargetedDistanceConfigurationSupported { get; }
	bool TargetedTrainingTimeConfigurationSupported { get; }
	bool TargetedTimeInTwoHeartRateZonesConfigurationSupported { get; }
	bool TargetedTimeInThreeHeartRateZonesConfigurationSupported { get; }
	bool TargetedTimeInFiveHeartRateZonesConfigurationSupported { get; }
	bool IndoorBikeSimulationParametersSupported { get; }
	bool WheelCircumferenceConfigurationSupported { get; }
	bool SpinDownControlSupported { get; }
	bool TargetedCadenceConfigurationSupported { get; }

	ISupportedRange? Speed { get; }
	ISupportedRange? Inclination { get; }
	ISupportedRange? ResistanceLevel { get; }
	ISupportedRange? Power { get; }
	ISupportedRange? HeartRange { get; }
}
