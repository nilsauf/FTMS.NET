namespace FTMS.NET.Utils;
using System.Numerics;

internal static class GenericMath
{
	public static TValue Clamp<TValue, TMin, TMax>(TValue value, TMin min, TMax max)
		where TValue : struct, INumber<TValue>
		where TMin : struct, INumber<TMin>
		where TMax : struct, INumber<TMax>
	{
		var tMin = TValue.CreateSaturating(min);
		if (tMin > value)
			return tMin;

		var tMax = TValue.CreateSaturating(max);
		if (tMax < value)
			return tMax;

		return value;
	}

	public static bool IsInRange<TValue, TMin, TMax>(TValue value, TMin min, TMax max)
		where TValue : struct, INumber<TValue>
		where TMin : struct, INumber<TMin>
		where TMax : struct, INumber<TMax>
		=> value >= TValue.CreateSaturating(min) && value <= TValue.CreateSaturating(max);
}
