namespace FTMS.NET;

using SourceGeneration.Reflection;
using System;
using System.Collections.Frozen;

[SourceReflection]
public static class FtmsUuids
{
	private static readonly FrozenDictionary<Guid, string> allFieldsByUuid;

	public readonly static Guid Service = Guid.ParseExact("00001826-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid Feature = Guid.ParseExact("00002acc-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid MachineState = Guid.ParseExact("00002ada-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid TrainingState = Guid.ParseExact("00002ad3-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid ControlPoint = Guid.ParseExact("00002ad9-0000-1000-8000-00805f9b34fb", "d");

	public readonly static Guid SupportedSpeedRange = Guid.ParseExact("00002ad4-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid SupportedInclinationRange = Guid.ParseExact("00002ad5-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid SupportedResistanceLevelRange = Guid.ParseExact("00002ad6-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid SupportedPowerRange = Guid.ParseExact("00002ad8-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid SupportedHeartRateRange = Guid.ParseExact("00002ad7-0000-1000-8000-00805f9b34fb", "d");

	public readonly static Guid TreadmillData = Guid.ParseExact("00002acd-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid CrossTrainerData = Guid.ParseExact("00002ace-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid StepClimberData = Guid.ParseExact("00002acf-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid StairClimberData = Guid.ParseExact("00002ad0-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid RowerData = Guid.ParseExact("00002ad1-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid IndoorBikeData = Guid.ParseExact("00002ad2-0000-1000-8000-00805f9b34fb", "d");

	[SourceReflection]
	public static class IndoorBike
	{
		public static readonly Guid InstantaneousSpeed = new("765050cd-a033-4972-a71b-29c013c0845b");
		public static readonly Guid AverageSpeed = new("fe4f0fda-caa8-4a91-b4f6-049a7c74a68f");
		public static readonly Guid InstantaneousCadence = new("d5f34315-184e-4d9d-a3dd-b70858475f1c");
		public static readonly Guid AverageCadence = new("549c3fa5-caea-4db5-8b08-8d621db5177c");
		public static readonly Guid TotalDistance = new("20cd1373-cb1c-40ac-90f2-9b2ff0c6e74b");
		public static readonly Guid ResistantLevel = new("acba72d3-4e64-4b36-8675-202bcf43e761");
		public static readonly Guid InstantaneousPower = new("a9b3e3f6-fe62-45e0-9f82-f7a26a33378a");
		public static readonly Guid AveragePower = new("183da544-af04-42b8-861d-07ce96e261ef");
		public static readonly Guid TotalEnergy = new("ed9a5962-6b12-4679-894a-379131e12122");
		public static readonly Guid EnergyPerHour = new("8d799ac5-9c50-4119-96c9-da99ae22f91f");
		public static readonly Guid EnergyPerMinute = new("73d9c26e-356b-4ebc-83da-0f2719fca421");
		public static readonly Guid HeartRate = new("636a7fef-dd35-49ec-83df-35d52a34d4cc");
		public static readonly Guid MetabolicEquivalent = new("166f2f4c-9cff-4050-985b-2fea8b1a95bc");
		public static readonly Guid ElapsedTime = new("1eb082c4-e3ae-46f9-b4cb-d55a2f81e27b");
		public static readonly Guid RemainingTime = new("02185a25-968e-40e0-a55c-b2acf71e50fb");
	}

	static FtmsUuids()
	{
		var thisType = typeof(FtmsUuids);
		var guidType = typeof(Guid);
		allFieldsByUuid = SourceReflector.GetTypes()
			.SelectMany(t => t.GetFieldsAndProperties()
				.Where(fop => fop.IsStatic && fop.Accessibility == SourceAccessibility.Public)
				.Where(fop => fop.MemberType == guidType))
			.ToFrozenDictionary(fop => (Guid)fop.GetValue(null)!, fop => fop.Name);
	}

	public static string GetName(Guid uuid)
	{
		return allFieldsByUuid.GetValueOrDefault(uuid) ?? string.Empty;
	}
}
