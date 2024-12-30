namespace FTMS.NET.Data;
using System;

public record ValueCalculation(Guid Uuid, int Multiplier = 1, int DecimalExponent = 0, int BinaryExponent = 0)
{
	private readonly double constantMultiplier = Multiplier * Math.Pow(10, DecimalExponent) * Math.Pow(2, BinaryExponent);

	public double Calculate(long rawValue) => rawValue * this.constantMultiplier;
}
