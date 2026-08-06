namespace FTMS.NET.Tests.State;

using FTMS.NET.State;
using System.Collections.Generic;
using System.Linq;

public sealed class FitnessMachineStateParameterFactoryTests
{
	/// <summary>
	/// Tests that a negative target incline (SINT16) is decoded as a negative percent value.
	/// </summary>
	[Fact]
	public void ReadParameters_TargetInclineChanged_NegativeIncline_ReturnsNegativePercent()
	{
		// -20 as SINT16, little-endian
		byte[] rawData = [0xEC, 0xFF];

		var parameters = FitnessMachineStateParameterFactory
			.ReadParameters(EStateOpCode.TargetInclineChanged, rawData)
			.Cast<FitnessMachineStateParameter>();

		var parameter = Assert.Single(parameters);
		Assert.Equal(FitnessMachineUnit.Percent, parameter.Unit);
		Assert.Equal(-2.0, parameter.Value, precision: 1); // -20 * 0.1
	}

	/// <summary>
	/// Tests that a targeted distance larger than 65,535 m (UINT24) is decoded fully.
	/// </summary>
	[Fact]
	public void ReadParameters_TargetedDistanceChanged_ThreeByteDistance_ReturnsFullValue()
	{
		// UINT24 little-endian: 0x0F4240 = 1,000,000 meters
		byte[] rawData = [0x40, 0x42, 0x0F];

		var parameters = FitnessMachineStateParameterFactory
			.ReadParameters(EStateOpCode.TargetedDistanceChanged, rawData)
			.Cast<FitnessMachineStateParameter>();

		var parameter = Assert.Single(parameters);
		Assert.Equal(FitnessMachineUnit.Meters, parameter.Unit);
		Assert.Equal(1_000_000.0, parameter.Value);
	}

	/// <summary>
	/// Tests that target incline decodes positive, zero and negative SINT16 values correctly.
	/// </summary>
	[Theory]
	[InlineData(new byte[] { 0x14, 0x00 }, 2.0)]   // +20 -> 2.0 %
	[InlineData(new byte[] { 0x00, 0x00 }, 0.0)]
	[InlineData(new byte[] { 0xFF, 0xFF }, -0.1)]  // -1 -> -0.1 %
	public void ReadParameters_TargetInclineChanged_PositiveAndNegative_ReturnsCorrectPercent(byte[] rawData, double expected)
	{
		var parameters = FitnessMachineStateParameterFactory
			.ReadParameters(EStateOpCode.TargetInclineChanged, rawData)
			.Cast<FitnessMachineStateParameter>();

		Assert.Equal(expected, Assert.Single(parameters).Value, precision: 1);
	}

	/// <summary>
	/// Tests that targeted distance decodes three-byte UINT24 values, including the maximum.
	/// </summary>
	[Theory]
	[InlineData(new byte[] { 0x64, 0x00, 0x00 }, 100.0)]
	[InlineData(new byte[] { 0xFF, 0xFF, 0xFF }, 16_777_215.0)]
	public void ReadParameters_TargetedDistanceChanged_EncodesThreeByteValue(byte[] rawData, double expected)
	{
		var parameters = FitnessMachineStateParameterFactory
			.ReadParameters(EStateOpCode.TargetedDistanceChanged, rawData)
			.Cast<FitnessMachineStateParameter>();

		Assert.Equal(expected, Assert.Single(parameters).Value);
	}
}
