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
        { FtmsUuids.IndoorBike.InstantaneousSpeed, nameof(FtmsUuids.IndoorBike.InstantaneousSpeed) },
		{ FtmsUuids.IndoorBike.AverageSpeed, nameof(FtmsUuids.IndoorBike.AverageSpeed) },
		{ FtmsUuids.IndoorBike.InstantaneousCadence, nameof(FtmsUuids.IndoorBike.InstantaneousCadence) },
		{ FtmsUuids.IndoorBike.AverageCadence, nameof(FtmsUuids.IndoorBike.AverageCadence) },
		{ FtmsUuids.IndoorBike.TotalDistance, nameof(FtmsUuids.IndoorBike.TotalDistance) },
		{ FtmsUuids.IndoorBike.ResistantLevel, nameof(FtmsUuids.IndoorBike.ResistantLevel) },
		{ FtmsUuids.IndoorBike.InstantaneousPower, nameof(FtmsUuids.IndoorBike.InstantaneousPower) },
		{ FtmsUuids.IndoorBike.AveragePower, nameof(FtmsUuids.IndoorBike.AveragePower) },
		{ FtmsUuids.IndoorBike.TotalEnergy, nameof(FtmsUuids.IndoorBike.TotalEnergy) },
		{ FtmsUuids.IndoorBike.EnergyPerHour, nameof(FtmsUuids.IndoorBike.EnergyPerHour) },
		{ FtmsUuids.IndoorBike.EnergyPerMinute, nameof(FtmsUuids.IndoorBike.EnergyPerMinute) },
	};

	[Theory]
	[MemberData(nameof(UuidTestData))]
	public void FtmsUuids_GetName_ReturnsCorrectName(Guid uuid, string nameOfUuid)
	{
		var name = FtmsUuids.GetName(uuid);
		Assert.Equal(nameOfUuid, name);
	}
}
