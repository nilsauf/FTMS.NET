namespace FTMS.NET.IndoorBike;

using FTMS.NET.Data;

internal static class IndoorBikeDataValue
{
	public static readonly Dictionary<Guid, ValueCalculation> Calculations = new List<ValueCalculation>()
	{
		new(IndoorBikeDataValueUuids.InstantaneousSpeed, 1, -2, 0),
		new(IndoorBikeDataValueUuids.AverageSpeed, 1, -2, 0),
		new(IndoorBikeDataValueUuids.InstantaneousCadence, 1, 0, -1),
		new(IndoorBikeDataValueUuids.AverageCadence, 1, 0, -1),
		new(IndoorBikeDataValueUuids.TotalDistance, 1, 0, 0),
		new(IndoorBikeDataValueUuids.ResistantLevel, 1, 1, 0),
		new(IndoorBikeDataValueUuids.InstantaneousPower, 1, 0, 0),
		new(IndoorBikeDataValueUuids.AveragePower, 1, 0, 0),
		new(IndoorBikeDataValueUuids.TotalEnergy, 1, 0, 0),
		new(IndoorBikeDataValueUuids.EnergyPerHour, 1, 0, 0),
		new(IndoorBikeDataValueUuids.EnergyPerMinute, 1, 0, 0),
		new(IndoorBikeDataValueUuids.HeartRate, 1, 0, 0),
		new(IndoorBikeDataValueUuids.MetabolicEquivalent, 1, -1, 0),
		new(IndoorBikeDataValueUuids.ElapsedTime, 1, 0, 0),
		new(IndoorBikeDataValueUuids.RemainingTime, 1, 0, 0),
	}.ToDictionary(calc => calc.Uuid);
}
