namespace FTMS.NET.Tests.Utils;

using FTMS.NET.Utils;

/// <summary>
/// Unit tests for the <see cref="GenericMath"/> class.
/// </summary>
public class GenericMathTests
{
    /// <summary>
    /// Tests that Clamp returns the minimum value when the input value is below the minimum.
    /// </summary>
    [Theory]
    [InlineData(5, 10, 20, 10)]
    [InlineData(-10, 0, 100, 0)]
    [InlineData(int.MinValue, 0, 100, 0)]
    public void Clamp_ValueBelowMin_ReturnsMin(int value, int min, int max, int expected)
    {
        // Act
        int result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Clamp returns the maximum value when the input value is above the maximum.
    /// </summary>
    [Theory]
    [InlineData(25, 10, 20, 20)]
    [InlineData(150, 0, 100, 100)]
    [InlineData(int.MaxValue, 0, 100, 100)]
    public void Clamp_ValueAboveMax_ReturnsMax(int value, int min, int max, int expected)
    {
        // Act
        int result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Clamp returns the original value when it is within the valid range.
    /// </summary>
    [Theory]
    [InlineData(15, 10, 20, 15)]
    [InlineData(50, 0, 100, 50)]
    [InlineData(0, -100, 100, 0)]
    public void Clamp_ValueWithinRange_ReturnsValue(int value, int min, int max, int expected)
    {
        // Act
        int result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Clamp returns the value when it equals the minimum boundary.
    /// </summary>
    [Theory]
    [InlineData(10, 10, 20, 10)]
    [InlineData(0, 0, 100, 0)]
    [InlineData(int.MinValue, int.MinValue, int.MaxValue, int.MinValue)]
    public void Clamp_ValueEqualsMin_ReturnsValue(int value, int min, int max, int expected)
    {
        // Act
        int result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Clamp returns the value when it equals the maximum boundary.
    /// </summary>
    [Theory]
    [InlineData(20, 10, 20, 20)]
    [InlineData(100, 0, 100, 100)]
    [InlineData(int.MaxValue, int.MinValue, int.MaxValue, int.MaxValue)]
    public void Clamp_ValueEqualsMax_ReturnsValue(int value, int min, int max, int expected)
    {
        // Act
        int result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Clamp handles extreme integer boundary values correctly.
    /// </summary>
    [Fact]
    public void Clamp_IntegerBoundaryValues_HandlesCorrectly()
    {
        // Arrange & Act
        int result1 = GenericMath.Clamp(int.MinValue, int.MinValue, int.MaxValue);
        int result2 = GenericMath.Clamp(int.MaxValue, int.MinValue, int.MaxValue);
        int result3 = GenericMath.Clamp(0, int.MinValue, int.MaxValue);

        // Assert
        Assert.Equal(int.MinValue, result1);
        Assert.Equal(int.MaxValue, result2);
        Assert.Equal(0, result3);
    }

    /// <summary>
    /// Tests that Clamp works with different numeric types using saturating conversion.
    /// </summary>
    [Fact]
    public void Clamp_CrossTypeConversion_UsesCreateSaturating()
    {
        // Arrange - int value with byte min and short max
        int value = 150;
        byte min = 50;
        short max = 200;

        // Act
        int result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(150, result);
    }

    /// <summary>
    /// Tests that Clamp saturates when max value exceeds TValue's representable range.
    /// </summary>
    [Fact]
    public void Clamp_MaxExceedsByteRange_SaturatesToByteMax()
    {
        // Arrange - byte value with int max that exceeds byte range
        byte value = 250;
        byte min = 0;
        int max = 1000; // Exceeds byte.MaxValue (255), should saturate to 255

        // Act
        byte result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(250, result);
    }

    /// <summary>
    /// Tests that Clamp saturates when min value exceeds TValue's representable range.
    /// </summary>
    [Fact]
    public void Clamp_MinExceedsByteRange_SaturatesToByteMax()
    {
        // Arrange - byte value with int min that exceeds byte range
        byte value = 10;
        int min = 300; // Exceeds byte.MaxValue (255), should saturate to 255
        byte max = 255;

        // Act
        byte result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(255, result); // value (10) < saturated min (255), returns saturated min
    }

    /// <summary>
    /// Tests that Clamp handles negative minimum values with unsigned types.
    /// </summary>
    [Fact]
    public void Clamp_NegativeMinWithUnsignedType_SaturatesToZero()
    {
        // Arrange - byte value with negative min (saturates to 0 for unsigned byte)
        byte value = 50;
        int min = -100; // Negative, saturates to 0 for byte
        byte max = 200;

        // Act
        byte result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(50, result);
    }

    /// <summary>
    /// Tests that Clamp works correctly with double precision floating-point values.
    /// </summary>
    [Theory]
    [InlineData(5.5, 1.0, 10.0, 5.5)]
    [InlineData(0.5, 1.0, 10.0, 1.0)]
    [InlineData(15.5, 1.0, 10.0, 10.0)]
    public void Clamp_DoubleValues_ClampsCorrectly(double value, double min, double max, double expected)
    {
        // Act
        double result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Clamp handles positive infinity by clamping to max.
    /// </summary>
    [Fact]
    public void Clamp_PositiveInfinity_ClampsToMax()
    {
        // Arrange
        double value = double.PositiveInfinity;
        double min = 0.0;
        double max = 100.0;

        // Act
        double result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(100.0, result);
    }

    /// <summary>
    /// Tests that Clamp handles negative infinity by clamping to min.
    /// </summary>
    [Fact]
    public void Clamp_NegativeInfinity_ClampsToMin()
    {
        // Arrange
        double value = double.NegativeInfinity;
        double min = -100.0;
        double max = 100.0;

        // Act
        double result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(-100.0, result);
    }

    /// <summary>
    /// Tests that Clamp handles NaN values, which have special comparison behavior.
    /// NaN comparisons always return false, so NaN > tMin is false and NaN < tMax is false.
    /// </summary>
    [Fact]
    public void Clamp_NaNValue_ReturnsNaN()
    {
        // Arrange
        double value = double.NaN;
        double min = 0.0;
        double max = 100.0;

        // Act
        double result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.True(double.IsNaN(result));
    }

    /// <summary>
    /// Tests that Clamp works correctly with long integer values at boundaries.
    /// </summary>
    [Fact]
    public void Clamp_LongBoundaryValues_HandlesCorrectly()
    {
        // Arrange & Act
        long result1 = GenericMath.Clamp(long.MinValue, long.MinValue, long.MaxValue);
        long result2 = GenericMath.Clamp(long.MaxValue, long.MinValue, long.MaxValue);
        long result3 = GenericMath.Clamp(0L, long.MinValue, long.MaxValue);

        // Assert
        Assert.Equal(long.MinValue, result1);
        Assert.Equal(long.MaxValue, result2);
        Assert.Equal(0L, result3);
    }

    /// <summary>
    /// Tests that Clamp works correctly with float values.
    /// </summary>
    [Theory]
    [InlineData(5.5f, 1.0f, 10.0f, 5.5f)]
    [InlineData(0.5f, 1.0f, 10.0f, 1.0f)]
    [InlineData(15.5f, 1.0f, 10.0f, 10.0f)]
    public void Clamp_FloatValues_ClampsCorrectly(float value, float min, float max, float expected)
    {
        // Act
        float result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that Clamp works correctly with decimal values.
    /// </summary>
    [Fact]
    public void Clamp_DecimalValues_ClampsCorrectly()
    {
        // Arrange
        decimal value1 = 5.5m;
        decimal value2 = 0.5m;
        decimal value3 = 15.5m;
        decimal min = 1.0m;
        decimal max = 10.0m;

        // Act
        decimal result1 = GenericMath.Clamp(value1, min, max);
        decimal result2 = GenericMath.Clamp(value2, min, max);
        decimal result3 = GenericMath.Clamp(value3, min, max);

        // Assert
        Assert.Equal(5.5m, result1);
        Assert.Equal(1.0m, result2);
        Assert.Equal(10.0m, result3);
    }

    /// <summary>
    /// Tests that Clamp handles the scenario where min is greater than max.
    /// In this case, tMin > value is checked first, so if value < min, min is returned.
    /// If value >= min (which is > max), then tMax < value will be true, so max is returned.
    /// </summary>
    [Fact]
    public void Clamp_MinGreaterThanMax_ReturnsBasedOnFirstCheck()
    {
        // Arrange
        int value1 = 5;
        int value2 = 20;
        int value3 = 25;
        int min = 20;
        int max = 10;

        // Act
        int result1 = GenericMath.Clamp(value1, min, max);
        int result2 = GenericMath.Clamp(value2, min, max);
        int result3 = GenericMath.Clamp(value3, min, max);

        // Assert
        Assert.Equal(20, result1); // value < min, returns min
        Assert.Equal(10, result2); // min check fails, value > max, returns max
        Assert.Equal(10, result3); // min check fails, value > max, returns max
    }

    /// <summary>
    /// Tests that Clamp works with short integer values.
    /// </summary>
    [Fact]
    public void Clamp_ShortValues_ClampsCorrectly()
    {
        // Arrange
        short value = 150;
        short min = 100;
        short max = 200;

        // Act
        short result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal((short)150, result);
    }

    /// <summary>
    /// Tests that Clamp works with short values at boundaries.
    /// </summary>
    [Fact]
    public void Clamp_ShortBoundaryValues_HandlesCorrectly()
    {
        // Arrange & Act
        short result1 = GenericMath.Clamp(short.MinValue, short.MinValue, short.MaxValue);
        short result2 = GenericMath.Clamp(short.MaxValue, short.MinValue, short.MaxValue);

        // Assert
        Assert.Equal(short.MinValue, result1);
        Assert.Equal(short.MaxValue, result2);
    }

    /// <summary>
    /// Tests that Clamp works correctly with zero values for all parameters.
    /// </summary>
    [Fact]
    public void Clamp_AllZeroValues_ReturnsZero()
    {
        // Arrange
        int value = 0;
        int min = 0;
        int max = 0;

        // Act
        int result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(0, result);
    }

    /// <summary>
    /// Tests that Clamp works with negative values for all parameters.
    /// </summary>
    [Theory]
    [InlineData(-15, -20, -10, -15)]
    [InlineData(-25, -20, -10, -20)]
    [InlineData(-5, -20, -10, -10)]
    public void Clamp_NegativeValues_ClampsCorrectly(int value, int min, int max, int expected)
    {
        // Act
        int result = GenericMath.Clamp(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that IsInRange returns true when value is within the inclusive range [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum bound.</param>
    /// <param name="max">The maximum bound.</param>
    [Theory]
    [InlineData(5, 1, 10)]
    [InlineData(0, -10, 10)]
    [InlineData(-5, -10, 0)]
    [InlineData(100, 100, 200)]
    [InlineData(200, 100, 200)]
    [InlineData(0, 0, 0)]
    public void IsInRange_ValueWithinRange_ReturnsTrue(int value, int min, int max)
    {
        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests that IsInRange returns false when value is outside the range [min, max].
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum bound.</param>
    /// <param name="max">The maximum bound.</param>
    [Theory]
    [InlineData(0, 1, 10)]
    [InlineData(11, 1, 10)]
    [InlineData(-11, -10, 0)]
    [InlineData(1, 2, 10)]
    [InlineData(99, 100, 200)]
    [InlineData(201, 100, 200)]
    public void IsInRange_ValueOutsideRange_ReturnsFalse(int value, int min, int max)
    {
        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Tests that IsInRange correctly handles boundary values including int.MinValue and int.MaxValue.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum bound.</param>
    /// <param name="max">The maximum bound.</param>
    /// <param name="expected">The expected result.</param>
    [Theory]
    [InlineData(int.MinValue, int.MinValue, int.MaxValue, true)]
    [InlineData(int.MaxValue, int.MinValue, int.MaxValue, true)]
    [InlineData(0, int.MinValue, int.MaxValue, true)]
    [InlineData(int.MinValue, int.MinValue, int.MinValue, true)]
    [InlineData(int.MaxValue, int.MaxValue, int.MaxValue, true)]
    [InlineData(int.MinValue, 0, int.MaxValue, false)]
    [InlineData(int.MaxValue, int.MinValue, 0, false)]
    public void IsInRange_BoundaryValues_ReturnsExpected(int value, int min, int max, bool expected)
    {
        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that IsInRange works correctly with double precision floating point values.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum bound.</param>
    /// <param name="max">The maximum bound.</param>
    /// <param name="expected">The expected result.</param>
    [Theory]
    [InlineData(5.5, 1.0, 10.0, true)]
    [InlineData(1.0, 1.0, 10.0, true)]
    [InlineData(10.0, 1.0, 10.0, true)]
    [InlineData(0.5, 1.0, 10.0, false)]
    [InlineData(10.5, 1.0, 10.0, false)]
    [InlineData(-0.001, -0.001, 0.001, true)]
    public void IsInRange_DoubleValues_ReturnsExpected(double value, double min, double max, bool expected)
    {
        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that IsInRange handles double.NaN correctly. NaN comparisons always return false.
    /// </summary>
    [Fact]
    public void IsInRange_ValueIsNaN_ReturnsFalse()
    {
        // Arrange
        double value = double.NaN;
        double min = 0.0;
        double max = 10.0;

        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Tests that IsInRange handles double.PositiveInfinity correctly.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum bound.</param>
    /// <param name="max">The maximum bound.</param>
    /// <param name="expected">The expected result.</param>
    [Theory]
    [InlineData(double.PositiveInfinity, 0.0, double.PositiveInfinity, true)]
    [InlineData(double.PositiveInfinity, 0.0, 100.0, false)]
    [InlineData(5.0, 0.0, double.PositiveInfinity, true)]
    public void IsInRange_PositiveInfinity_ReturnsExpected(double value, double min, double max, bool expected)
    {
        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that IsInRange handles double.NegativeInfinity correctly.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum bound.</param>
    /// <param name="max">The maximum bound.</param>
    /// <param name="expected">The expected result.</param>
    [Theory]
    [InlineData(double.NegativeInfinity, double.NegativeInfinity, 0.0, true)]
    [InlineData(double.NegativeInfinity, -100.0, 0.0, false)]
    [InlineData(5.0, double.NegativeInfinity, 10.0, true)]
    public void IsInRange_NegativeInfinity_ReturnsExpected(double value, double min, double max, bool expected)
    {
        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that IsInRange works correctly with mixed numeric types, relying on CreateSaturating conversions.
    /// </summary>
    [Fact]
    public void IsInRange_MixedNumericTypes_IntValueShortMinLongMax_ReturnsTrue()
    {
        // Arrange
        int value = 1000;
        short min = 100;
        long max = 10000L;

        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests that IsInRange with mixed types returns false when value is outside range.
    /// </summary>
    [Fact]
    public void IsInRange_MixedNumericTypes_IntValueOutsideRange_ReturnsFalse()
    {
        // Arrange
        int value = 50;
        short min = 100;
        long max = 10000L;

        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Tests that IsInRange handles saturation when min/max types have larger range than TValue.
    /// When converting a value larger than byte.MaxValue to byte, it saturates to 255.
    /// </summary>
    [Fact]
    public void IsInRange_TypeSaturation_MaxExceedsByteRange_Saturates()
    {
        // Arrange
        byte value = 200;
        int min = -100; // Saturates to 0 when converted to byte
        int max = 300;  // Saturates to 255 when converted to byte

        // Act
        // After saturation: IsInRange<byte>(200, 0, 255)
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests that IsInRange handles saturation when min is negative but TValue is unsigned.
    /// </summary>
    [Fact]
    public void IsInRange_TypeSaturation_NegativeMinWithUnsignedValue_Saturates()
    {
        // Arrange
        byte value = 5;
        int min = -100; // Saturates to 0 when converted to byte
        int max = 10;

        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests that IsInRange returns false for all values when min > max (invalid range).
    /// </summary>
    /// <param name="value">The value to test.</param>
    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(-5)]
    public void IsInRange_MinGreaterThanMax_ReturnsFalse(int value)
    {
        // Arrange
        int min = 10;
        int max = 5;

        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Tests that IsInRange works correctly with decimal type values.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum bound.</param>
    /// <param name="max">The maximum bound.</param>
    /// <param name="expected">The expected result.</param>
    [Theory]
    [InlineData(5.5, 1.0, 10.0, true)]
    [InlineData(0.5, 1.0, 10.0, false)]
    [InlineData(10.5, 1.0, 10.0, false)]
    public void IsInRange_DecimalValues_ReturnsExpected(double valueDouble, double minDouble, double maxDouble, bool expected)
    {
        // Arrange
        decimal value = (decimal)valueDouble;
        decimal min = (decimal)minDouble;
        decimal max = (decimal)maxDouble;

        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that IsInRange works correctly with float type values.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum bound.</param>
    /// <param name="max">The maximum bound.</param>
    /// <param name="expected">The expected result.</param>
    [Theory]
    [InlineData(5.5f, 1.0f, 10.0f, true)]
    [InlineData(1.0f, 1.0f, 10.0f, true)]
    [InlineData(10.0f, 1.0f, 10.0f, true)]
    [InlineData(0.5f, 1.0f, 10.0f, false)]
    [InlineData(10.5f, 1.0f, 10.0f, false)]
    public void IsInRange_FloatValues_ReturnsExpected(float value, float min, float max, bool expected)
    {
        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that IsInRange handles float.NaN correctly. NaN comparisons always return false.
    /// </summary>
    [Fact]
    public void IsInRange_FloatNaN_ReturnsFalse()
    {
        // Arrange
        float value = float.NaN;
        float min = 0.0f;
        float max = 10.0f;

        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Tests that IsInRange works with long type values and boundaries.
    /// </summary>
    [Fact]
    public void IsInRange_LongBoundaryValues_ReturnsTrue()
    {
        // Arrange
        long value = long.MaxValue;
        long min = long.MinValue;
        long max = long.MaxValue;

        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests that IsInRange works with negative values across the range.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum bound.</param>
    /// <param name="max">The maximum bound.</param>
    /// <param name="expected">The expected result.</param>
    [Theory]
    [InlineData(-5, -10, -1, true)]
    [InlineData(-10, -10, -1, true)]
    [InlineData(-1, -10, -1, true)]
    [InlineData(0, -10, -1, false)]
    [InlineData(-11, -10, -1, false)]
    public void IsInRange_NegativeValues_ReturnsExpected(int value, int min, int max, bool expected)
    {
        // Act
        bool result = GenericMath.IsInRange(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }
}