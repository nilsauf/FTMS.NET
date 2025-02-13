namespace FTMS.NET.Utils;
using System;

internal record ValueCalculation(int Multiplier = 1, int DecimalExponent = 0, int BinaryExponent = 0)
{
	private readonly double constantMultiplier = Multiplier * Math.Pow(10, DecimalExponent) * Math.Pow(2, BinaryExponent);

	public double Calculate(long rawValue) => rawValue * this.constantMultiplier;
}
