namespace FTMS.NET.Exceptions;

using System;

public sealed class NeededCharacteristicNotAvailableException(
	Guid characteristicUuid)
	: Exception
{
	public string CharacteristicName { get; } = FtmsUuids.GetName(characteristicUuid);
	public Guid CharacteristicUuid { get; } = characteristicUuid;
}
