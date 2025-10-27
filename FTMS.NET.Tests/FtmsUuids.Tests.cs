namespace FTMS.NET.Tests;
using System;

public class FtmsUuids_Tests
{
	public static readonly TheoryData<Guid, string> UuidTestData = new()
	{
		{ FtmsUuids.Service, nameof(FtmsUuids.Service) },
		{ FtmsUuids.Feature, nameof(FtmsUuids.Feature) },
		{ FtmsUuids.MachineState, nameof(FtmsUuids.MachineState) },
		{ FtmsUuids.TrainingState, nameof(FtmsUuids.TrainingState) },
		{ FtmsUuids.ControlPoint, nameof(FtmsUuids.ControlPoint) },
		{ FtmsUuids.SupportedSpeedRange, nameof(FtmsUuids.SupportedSpeedRange) },
		{ FtmsUuids.SupportedInclinationRange, nameof(FtmsUuids.SupportedInclinationRange) },
		{ FtmsUuids.SupportedResistanceLevelRange, nameof(FtmsUuids.SupportedResistanceLevelRange) },
		{ FtmsUuids.SupportedPowerRange, nameof(FtmsUuids.SupportedPowerRange) },
		{ FtmsUuids.SupportedHeartRateRange, nameof(FtmsUuids.SupportedHeartRateRange) },
		{ FtmsUuids.TreadmillData, nameof(FtmsUuids.TreadmillData) },
		{ FtmsUuids.CrossTrainerData, nameof(FtmsUuids.CrossTrainerData) },
		{ FtmsUuids.StepClimberData, nameof(FtmsUuids.StepClimberData) },
		{ FtmsUuids.StairClimberData, nameof(FtmsUuids.StairClimberData) },
		{ FtmsUuids.RowerData, nameof(FtmsUuids.RowerData) },
		{ FtmsUuids.IndoorBikeData, nameof(FtmsUuids.IndoorBikeData) },

        // Indoor Bike specific
        { FtmsUuids.InstantaneousSpeed, nameof(FtmsUuids.InstantaneousSpeed) },
		{ FtmsUuids.AverageSpeed, nameof(FtmsUuids.AverageSpeed) },
		{ FtmsUuids.InstantaneousCadence, nameof(FtmsUuids.InstantaneousCadence) },
		{ FtmsUuids.AverageCadence, nameof(FtmsUuids.AverageCadence) },
		{ FtmsUuids.TotalDistance, nameof(FtmsUuids.TotalDistance) },
		{ FtmsUuids.ResistantLevel, nameof(FtmsUuids.ResistantLevel) },
		{ FtmsUuids.InstantaneousPower, nameof(FtmsUuids.InstantaneousPower) },
		{ FtmsUuids.AveragePower, nameof(FtmsUuids.AveragePower) },
		{ FtmsUuids.TotalEnergy, nameof(FtmsUuids.TotalEnergy) },
		{ FtmsUuids.EnergyPerHour, nameof(FtmsUuids.EnergyPerHour) },
		{ FtmsUuids.EnergyPerMinute, nameof(FtmsUuids.EnergyPerMinute) },
	};

	[Theory]
	[MemberData(nameof(UuidTestData))]
	public void FtmsUuids_GetName_ReturnsCorrectName(Guid uuid, string nameOfUuid)
	{
		var name = FtmsUuids.GetName(uuid);
		Assert.Equal(nameOfUuid, name);
	}
}
