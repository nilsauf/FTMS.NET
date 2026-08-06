namespace FTMS.NET.Tests.Data;

using FTMS.NET.Data;
using System.Collections.Generic;
using System.Linq;

public sealed class FitnessMachineDataReaderTests
{
	/// <summary>
	/// Tests that a treadmill frame with Heart Rate present (bit 8 of the 2-octet flag field)
	/// is parsed correctly using standard bit numbering.
	/// Expected: Both Instantaneous Speed and Heart Rate are parsed from the frame.
	/// </summary>
	[Fact]
	public void Read_TreadmillFrameWithHeartRate_ParsesHeartRate()
	{
		// Flags: bit 0 = 0 (Instantaneous Speed present), bit 8 = 1 (Heart Rate present)
		// LE: byte0 = 0x00, byte1 = 0x01
		byte[] frame = [0x00, 0x01, 0xD2, 0x04, 0x48]; // speed raw 1234, HR 72 bpm

		FitnessMachineDataReader reader = new(
			SingleFrameStrategies.GetFor(EFitnessMachineType.Threadmill));

		List<IFitnessMachineValue> values = reader.Read(frame).ToList();

		IFitnessMachineValue speed = values.Single(v => v.Uuid == FtmsUuids.InstantaneousSpeed);
		Assert.Equal(12.34, speed.Value, precision: 2);

		IFitnessMachineValue heartRate = values.Single(v => v.Uuid == FtmsUuids.HeartRate);
		Assert.Equal(72, heartRate.Value);
	}
}