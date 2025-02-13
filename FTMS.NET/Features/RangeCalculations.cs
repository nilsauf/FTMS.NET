namespace FTMS.NET.Features;

using FTMS.NET.Utils;

internal static class RangeCalculations
{
	public static ValueCalculation Speed => new(1, -2, 0);
	public static ValueCalculation Inclination => new(1, -1, 0);
	public static ValueCalculation ResistanceLevel => new(1, 1, 0);
	public static ValueCalculation Power => new(1, 0, 0);
	public static ValueCalculation HeartRange => new(1, 0, 0);
}
