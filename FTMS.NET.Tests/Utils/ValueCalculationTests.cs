
namespace FTMS.NET.Utils.UnitTests;

/// <summary>
/// Unit tests for the <see cref="ValueCalculation"/> record's Calculate method.
/// </summary>
public partial class ValueCalculationTests
{
    /// <summary>
    /// Tests that Calculate returns the correct result with default parameters (multiplier=1, exponents=0).
    /// The constant multiplier should be 1, so the result should equal the raw value.
    /// </summary>
    [Theory]
    [InlineData(0L, 0.0)]
    [InlineData(1L, 1.0)]
    [InlineData(-1L, -1.0)]
    [InlineData(100L, 100.0)]
    [InlineData(-100L, -100.0)]
    [InlineData(long.MaxValue, 9223372036854775807.0)]
    [InlineData(long.MinValue, -9223372036854775808.0)]
    public void Calculate_WithDefaultParameters_ReturnsRawValue(long rawValue, double expected)
    {
        // Arrange
        var calculation = new ValueCalculation();

        // Act
        double result = calculation.Calculate(rawValue);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Calculate returns zero when the raw value is zero, regardless of the multiplier.
    /// </summary>
    [Theory]
    [InlineData(1, 0, 0)]
    [InlineData(5, 0, 0)]
    [InlineData(-5, 0, 0)]
    [InlineData(100, 2, 3)]
    [InlineData(int.MaxValue, 10, 10)]
    public void Calculate_WithZeroRawValue_ReturnsZero(int multiplier, int decimalExponent, int binaryExponent)
    {
        // Arrange
        var calculation = new ValueCalculation(multiplier, decimalExponent, binaryExponent);

        // Act
        double result = calculation.Calculate(0L);

        // Assert
        Assert.Equal(0.0, result);
    }

    /// <summary>
    /// Tests that Calculate correctly applies the multiplier to the raw value.
    /// With decimal and binary exponents at 0, the constant multiplier equals the multiplier parameter.
    /// </summary>
    [Theory]
    [InlineData(2, 10L, 20.0)]
    [InlineData(5, 3L, 15.0)]
    [InlineData(-2, 10L, -20.0)]
    [InlineData(-3, -5L, 15.0)]
    [InlineData(0, 100L, 0.0)]
    [InlineData(1, long.MaxValue, 9223372036854775807.0)]
    [InlineData(10, 100L, 1000.0)]
    public void Calculate_WithMultiplier_ReturnsScaledValue(int multiplier, long rawValue, double expected)
    {
        // Arrange
        var calculation = new ValueCalculation(Multiplier: multiplier, DecimalExponent: 0, BinaryExponent: 0);

        // Act
        double result = calculation.Calculate(rawValue);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Calculate correctly applies the decimal exponent (power of 10).
    /// The constant multiplier should be multiplied by 10^decimalExponent.
    /// </summary>
    [Theory]
    [InlineData(1, 2, 0, 5L, 500.0)]           // 5 * (1 * 10^2 * 2^0) = 5 * 100 = 500
    [InlineData(1, 3, 0, 2L, 2000.0)]          // 2 * (1 * 10^3 * 2^0) = 2 * 1000 = 2000
    [InlineData(1, -2, 0, 100L, 1.0)]          // 100 * (1 * 10^-2 * 2^0) = 100 * 0.01 = 1
    [InlineData(2, 1, 0, 5L, 100.0)]           // 5 * (2 * 10^1 * 2^0) = 5 * 20 = 100
    [InlineData(1, 0, 0, 10L, 10.0)]           // 10 * (1 * 10^0 * 2^0) = 10 * 1 = 10
    public void Calculate_WithDecimalExponent_ReturnsCorrectValue(int multiplier, int decimalExponent, int binaryExponent, long rawValue, double expected)
    {
        // Arrange
        var calculation = new ValueCalculation(multiplier, decimalExponent, binaryExponent);

        // Act
        double result = calculation.Calculate(rawValue);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Calculate correctly applies the binary exponent (power of 2).
    /// The constant multiplier should be multiplied by 2^binaryExponent.
    /// </summary>
    [Theory]
    [InlineData(1, 0, 3, 5L, 40.0)]            // 5 * (1 * 10^0 * 2^3) = 5 * 8 = 40
    [InlineData(1, 0, 4, 2L, 32.0)]            // 2 * (1 * 10^0 * 2^4) = 2 * 16 = 32
    [InlineData(1, 0, -2, 16L, 4.0)]           // 16 * (1 * 10^0 * 2^-2) = 16 * 0.25 = 4
    [InlineData(2, 0, 2, 5L, 40.0)]            // 5 * (2 * 10^0 * 2^2) = 5 * 8 = 40
    [InlineData(1, 0, 0, 10L, 10.0)]           // 10 * (1 * 10^0 * 2^0) = 10 * 1 = 10
    public void Calculate_WithBinaryExponent_ReturnsCorrectValue(int multiplier, int decimalExponent, int binaryExponent, long rawValue, double expected)
    {
        // Arrange
        var calculation = new ValueCalculation(multiplier, decimalExponent, binaryExponent);

        // Act
        double result = calculation.Calculate(rawValue);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Calculate correctly combines all three parameters (multiplier, decimal exponent, binary exponent).
    /// </summary>
    [Theory]
    [InlineData(2, 1, 2, 5L, 400.0)]           // 5 * (2 * 10^1 * 2^2) = 5 * (2 * 10 * 4) = 5 * 80 = 400
    [InlineData(3, 2, 3, 1L, 2400.0)]          // 1 * (3 * 10^2 * 2^3) = 3 * 100 * 8 = 2400
    [InlineData(5, -1, 1, 10L, 10.0)]          // 10 * (5 * 10^-1 * 2^1) = 10 * (0.5 * 2) = 10 * 1 = 10
    [InlineData(1, 1, 1, 50L, 1000.0)]         // 50 * (1 * 10^1 * 2^1) = 50 * 20 = 1000
    [InlineData(-2, 1, 1, 5L, -200.0)]         // 5 * (-2 * 10^1 * 2^1) = 5 * (-40) = -200
    public void Calculate_WithCombinedParameters_ReturnsCorrectValue(int multiplier, int decimalExponent, int binaryExponent, long rawValue, double expected)
    {
        // Arrange
        var calculation = new ValueCalculation(multiplier, decimalExponent, binaryExponent);

        // Act
        double result = calculation.Calculate(rawValue);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Calculate handles negative raw values correctly.
    /// The sign of the raw value should be preserved in the result.
    /// </summary>
    [Theory]
    [InlineData(1, 0, 0, -10L, -10.0)]
    [InlineData(2, 0, 0, -5L, -10.0)]
    [InlineData(1, 2, 0, -1L, -100.0)]
    [InlineData(-2, 0, 0, -10L, 20.0)]         // Negative multiplier with negative rawValue = positive
    [InlineData(5, 1, 1, -2L, -200.0)]
    public void Calculate_WithNegativeRawValue_ReturnsCorrectSignedResult(int multiplier, int decimalExponent, int binaryExponent, long rawValue, double expected)
    {
        // Arrange
        var calculation = new ValueCalculation(multiplier, decimalExponent, binaryExponent);

        // Act
        double result = calculation.Calculate(rawValue);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Calculate handles extreme raw values (long.MinValue and long.MaxValue) correctly.
    /// These tests verify that the calculation doesn't cause unexpected overflow behavior.
    /// </summary>
    [Theory]
    [InlineData(2, 0, 0, long.MaxValue, 1.8446744073709552E+19)]     // long.MaxValue * 2
    [InlineData(2, 0, 0, long.MinValue, -1.8446744073709552E+19)]    // long.MinValue * 2
    [InlineData(1, 1, 0, long.MaxValue, 9.223372036854776E+19)]      // long.MaxValue * 10
    [InlineData(1, -10, 0, long.MaxValue, 922337203.6854776)]        // long.MaxValue * 10^-10
    public void Calculate_WithExtremeRawValues_ReturnsCorrectValue(int multiplier, int decimalExponent, int binaryExponent, long rawValue, double expected)
    {
        // Arrange
        var calculation = new ValueCalculation(multiplier, decimalExponent, binaryExponent);

        // Act
        double result = calculation.Calculate(rawValue);

        // Assert
        Assert.Equal(expected, result, precision: 10);
    }

    /// <summary>
    /// Tests that Calculate can produce positive infinity when the result exceeds double's maximum value.
    /// </summary>
    [Fact]
    public void Calculate_WithVeryLargeMultiplier_ReturnsPositiveInfinity()
    {
        // Arrange
        var calculation = new ValueCalculation(Multiplier: 1, DecimalExponent: 300, BinaryExponent: 10);

        // Act
        double result = calculation.Calculate(long.MaxValue);

        // Assert
        Assert.Equal(double.PositiveInfinity, result);
    }

    /// <summary>
    /// Tests that Calculate can produce negative infinity when the result is below double's minimum value.
    /// </summary>
    [Fact]
    public void Calculate_WithVeryLargeMultiplierAndNegativeValue_ReturnsNegativeInfinity()
    {
        // Arrange
        var calculation = new ValueCalculation(Multiplier: 1, DecimalExponent: 300, BinaryExponent: 10);

        // Act
        double result = calculation.Calculate(long.MinValue);

        // Assert
        Assert.Equal(double.NegativeInfinity, result);
    }

    /// <summary>
    /// Tests that Calculate returns values very close to zero when using large negative exponents.
    /// </summary>
    [Theory]
    [InlineData(1, -300, 0, 100L)]
    [InlineData(1, 0, -1000, 100L)]
    [InlineData(1, -150, -150, 1000L)]
    public void Calculate_WithVerySmallMultiplier_ReturnsValueCloseToZero(int multiplier, int decimalExponent, int binaryExponent, long rawValue)
    {
        // Arrange
        var calculation = new ValueCalculation(multiplier, decimalExponent, binaryExponent);

        // Act
        double result = calculation.Calculate(rawValue);

        // Assert
        Assert.True(result >= 0.0 && result < 1e-100);
    }

    /// <summary>
    /// Tests that Calculate with a zero multiplier always returns zero.
    /// </summary>
    [Theory]
    [InlineData(0, 0, 0, 100L)]
    [InlineData(0, 5, 3, long.MaxValue)]
    [InlineData(0, -5, -3, long.MinValue)]
    [InlineData(0, 10, 10, -1000L)]
    public void Calculate_WithZeroMultiplier_ReturnsZero(int multiplier, int decimalExponent, int binaryExponent, long rawValue)
    {
        // Arrange
        var calculation = new ValueCalculation(multiplier, decimalExponent, binaryExponent);

        // Act
        double result = calculation.Calculate(rawValue);

        // Assert
        Assert.Equal(0.0, result);
    }
}