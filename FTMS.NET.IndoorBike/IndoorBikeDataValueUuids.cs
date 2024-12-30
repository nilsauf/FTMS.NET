namespace FTMS.NET.IndoorBike;
using System;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

internal static class IndoorBikeDataValueUuids
{
	private static readonly FrozenDictionary<Guid, string> allFieldsByUuid;
	private static readonly FrozenDictionary<string, Guid> allFieldsByName;

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

	static IndoorBikeDataValueUuids()
	{
		var uuidType = typeof(IndoorBikeDataValueUuids);
		var guidType = typeof(Guid);
		allFieldsByUuid = uuidType
			.GetFields(BindingFlags.Static | BindingFlags.Public)
			.Where(f => f.FieldType == guidType)
			.ToFrozenDictionary(f => (Guid)f.GetValue(null)!, f => f.Name);

		allFieldsByName = allFieldsByUuid.ToFrozenDictionary(f => f.Value, f => f.Key);
	}

	public static string GetName(Guid uuid)
	{
		return allFieldsByUuid.GetValueOrDefault(uuid) ?? string.Empty;
	}

	public static Guid GetUuid(string name)
	{
		return allFieldsByName.GetValueOrDefault(name);
	}
}
