namespace FTMS.NET.Features;

using System;

public static class ISupportedRangeExtensions
{
	public static bool IsInRange(this ISupportedRange range, double value)
		=> value >= range.MinimumValue && value <= range.MaximumValue;
	public static double Clamp(this ISupportedRange range, double value)
		=> Math.Clamp(value, range.MinimumValue, range.MaximumValue);
}
