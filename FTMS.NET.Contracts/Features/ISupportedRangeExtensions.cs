namespace FTMS.NET.Features;

using FTMS.NET.Utils;
using System.Numerics;

public static class ISupportedRangeExtensions
{
	public static bool IsInRange<TValue>(this ISupportedRange range, TValue value)
		where TValue : struct, INumber<TValue>
		=> GenericMath.IsInRange(value, range.MinimumValue, range.MaximumValue);
	public static TValue Clamp<TValue>(this ISupportedRange range, TValue value)
		where TValue : struct, INumber<TValue>
		=> GenericMath.Clamp(value, range.MinimumValue, range.MaximumValue);
}
