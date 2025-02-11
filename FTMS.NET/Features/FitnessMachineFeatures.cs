namespace FTMS.NET.Features;

using FTMS.NET.Utils;
using System;

internal class FitnessMachineFeatures(
	ReadOnlySpan<byte> featuresField,
	ReadOnlySpan<byte> targetSettingsField) : IFitnessMachineFeatures
{
	public bool AverageSpeedSupported { get; } = featuresField.IsBitSet(0);
	public bool CadenceSupported { get; } = featuresField.IsBitSet(1);
	public bool TotalDistanceSupported { get; } = featuresField.IsBitSet(2);
	public bool InclinationSupported { get; } = featuresField.IsBitSet(3);
	public bool ElevationGainSupported { get; } = featuresField.IsBitSet(4);
	public bool PaceSupported { get; } = featuresField.IsBitSet(5);
	public bool StepCountSupported { get; } = featuresField.IsBitSet(6);
	public bool ResistanceLevelSupported { get; } = featuresField.IsBitSet(7);
	public bool StrideCountSupported { get; } = featuresField.IsBitSet(8);
	public bool ExpendedEnergySupported { get; } = featuresField.IsBitSet(9);
	public bool HeartRateMeasurementSupported { get; } = featuresField.IsBitSet(10);
	public bool MetabolicEquivalentSupported { get; } = featuresField.IsBitSet(11);
	public bool ElapsedTimeSupported { get; } = featuresField.IsBitSet(12);
	public bool RemainingTimeSupported { get; } = featuresField.IsBitSet(13);
	public bool PowerMeasurementSupported { get; } = featuresField.IsBitSet(14);
	public bool ForceOnBeltAndPowerOutputSupported { get; } = featuresField.IsBitSet(15);
	public bool UserDataRetentionSupported { get; } = featuresField.IsBitSet(16);

	public bool SpeedTargetSettingSupported { get; } = targetSettingsField.IsBitSet(0);
	public bool InclinationTargetSettingSupported { get; } = targetSettingsField.IsBitSet(1);
	public bool ResistanceTargetSettingSupported { get; } = targetSettingsField.IsBitSet(2);
	public bool PowerTargetSettingSupported { get; } = targetSettingsField.IsBitSet(3);
	public bool HeartRateTargetSettingSupported { get; } = targetSettingsField.IsBitSet(4);
	public bool TargetedExpendedEnergyConfigurationSupported { get; } = targetSettingsField.IsBitSet(5);
	public bool TargetedStepNumberConfigurationSupported { get; } = targetSettingsField.IsBitSet(6);
	public bool TargetedStrideNumberConfigurationSupported { get; } = targetSettingsField.IsBitSet(7);
	public bool TargetedDistanceConfigurationSupported { get; } = targetSettingsField.IsBitSet(8);
	public bool TargetedTrainingTimeConfigurationSupported { get; } = targetSettingsField.IsBitSet(9);
	public bool TargetedTimeInTwoHeartRateZonesConfigurationSupported { get; } = targetSettingsField.IsBitSet(10);
	public bool TargetedTimeInThreeHeartRateZonesConfigurationSupported { get; } = targetSettingsField.IsBitSet(11);
	public bool TargetedTimeInFiveHeartRateZonesConfigurationSupported { get; } = targetSettingsField.IsBitSet(12);
	public bool IndoorBikeSimulationParametersSupported { get; } = targetSettingsField.IsBitSet(13);
	public bool WheelCircumferenceConfigurationSupported { get; } = targetSettingsField.IsBitSet(14);
	public bool SpinDownControlSupported { get; } = targetSettingsField.IsBitSet(15);
	public bool TargetedCadenceConfigurationSupported { get; } = targetSettingsField.IsBitSet(16);

	public ISupportedRange? Speed { get; init; }

	public ISupportedRange? Inclination { get; init; }

	public ISupportedRange? ResistanceLevel { get; init; }

	public ISupportedRange? Power { get; init; }

	public ISupportedRange? HeartRange { get; init; }
}
