namespace FTMS.NET;
using System;

public static class FtmsUuids
{
	public readonly static Guid Service = Guid.ParseExact("00001826-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid State = Guid.ParseExact("00002ada-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid ControlPoint = Guid.ParseExact("00002ad9-0000-1000-8000-00805f9b34fb", "d");
	public readonly static Guid IndoorBikeData = Guid.ParseExact("00002ad2-0000-1000-8000-00805f9b34fb", "d");
}
