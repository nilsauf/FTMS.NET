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
	public static readonly Guid Inclination = new("d31143f6-3808-4921-aa24-abc3e714e291");
	public static readonly Guid RampAngleSetting = new("fa0e1933-d7a3-4537-a496-0a7211da6ca0");
	public static readonly Guid PositiveElevationGain = new("ef8234ce-19bd-4596-860e-30e3e632dcc6");
	public static readonly Guid NegativeElevationGain = new("bcb222de-896f-4582-8308-c44fa2c05a0c");
	public static readonly Guid InstantaneousPace = new("cb813306-52ef-463b-bde6-be92e58706ab");
	public static readonly Guid AveragePace = new("4ced1b35-7d75-4911-9afd-9a8ba9ca9d4e");
	public static readonly Guid ForceOnBelt = new("9fa8ab4b-8382-4240-9f71-2a04c221aa7f");
	public static readonly Guid PowerOutput = new("7128776e-0991-44b9-8511-ef969a8ea418");
	public static readonly Guid StepsPerMinute = new("814c417b-95f2-4cea-b86f-30625b4c2777");
	public static readonly Guid AverageStepRate = new("654ecaf1-be99-4256-a764-ff8f556d4392");
	public static readonly Guid StrideCount = new("ecdfab6f-40f4-45ad-9be9-030982a563ff");
	public static readonly Guid Floors = new("ace47522-5ea0-459b-afe7-8b70020f9e15");
	public static readonly Guid StepCount = new("97984a03-ee3f-4fd0-857f-d17500ac9038");
	public static readonly Guid StrokeRate = new("c663b25a-abef-44fe-a852-2b7ccbdb0149");
	public static readonly Guid StrokeCount = new("0119eab6-298b-4c2f-a7c4-26a4933c8365");
	public static readonly Guid AverageStrokeRate = new("07d4df62-397b-4040-ac9a-8fef6ad8a44d");

	static FtmsUuids()
	{
		var thisType = typeof(FtmsUuids);
		var guidType = typeof(Guid);
		allFieldsByUuid = SourceReflector.GetType(thisType)?
			.GetFieldsAndProperties()
			.Where(fop => fop.IsStatic && fop.Accessibility == SourceAccessibility.Public)
			.Where(fop => fop.MemberType == guidType)
			.ToFrozenDictionary(fop => (Guid)fop.GetValue(null)!, fop => fop.Name)
			?? throw new InvalidOperationException($"{nameof(FtmsUuids)} class reflection was not generated!");
	}

	public static string GetName(Guid uuid)
	{
		return allFieldsByUuid.GetValueOrDefault(uuid) ?? string.Empty;
	}
}
