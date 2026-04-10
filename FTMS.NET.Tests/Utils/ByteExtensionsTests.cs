namespace FTMS.NET.Tests.Utils;

using FTMS.NET.Utils;

public sealed class ByteExtensionsTests
{
    /// <summary>
    /// Tests that IsBitSet correctly identifies set and unset bits for all positions 0-7
    /// in a byte with a known bit pattern (0b10100101).
    /// Expected: Returns true for positions where bit is 1, false where bit is 0.
    /// </summary>
    [Fact]
    public void IsBitSet_ByteWithMixedBitPattern_ReturnsCorrectStateForEachPosition()
    {
        // Arrange
        // 0b10100101 == 0xA5 == 165
        byte b = 0b1010_0101;

        // Act & Assert
        Assert.True(b.IsBitSet(0));   // LSB = 1
        Assert.False(b.IsBitSet(1));  // bit 1 = 0
        Assert.True(b.IsBitSet(2));   // bit 2 = 1
        Assert.False(b.IsBitSet(3));  // bit 3 = 0
        Assert.False(b.IsBitSet(4));  // bit 4 = 0
        Assert.True(b.IsBitSet(5));   // bit 5 = 1
        Assert.False(b.IsBitSet(6));  // bit 6 = 0
        Assert.True(b.IsBitSet(7));   // MSB = 1
    }

    /// <summary>
    /// Tests that IsBitSet returns false for all bit positions when the byte value is 0x00 (all bits unset).
    /// Expected: Returns false for all positions 0-7.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void IsBitSet_ByteWithAllBitsUnset_ReturnsFalseForAllPositions(int pos)
    {
        // Arrange
        byte b = 0x00;

        // Act
        bool result = b.IsBitSet(pos);

		// Assert
		Assert.False(result);
    }

    /// <summary>
    /// Tests that IsBitSet returns true for all bit positions when the byte value is 0xFF (all bits set).
    /// Expected: Returns true for all positions 0-7.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void IsBitSet_ByteWithAllBitsSet_ReturnsTrueForAllPositions(int pos)
    {
        // Arrange
        byte b = 0xFF;

        // Act
        bool result = b.IsBitSet(pos);

		// Assert
		Assert.True(result);
    }

    /// <summary>
    /// Tests that IsBitSet correctly identifies a single set bit at various positions.
    /// Expected: Returns true only for the position where the bit is set, false for all others.
    /// </summary>
    [Theory]
    [InlineData(0, 0b0000_0001)]
    [InlineData(1, 0b0000_0010)]
    [InlineData(2, 0b0000_0100)]
    [InlineData(3, 0b0000_1000)]
    [InlineData(4, 0b0001_0000)]
    [InlineData(5, 0b0010_0000)]
    [InlineData(6, 0b0100_0000)]
    [InlineData(7, 0b1000_0000)]
    public void IsBitSet_ByteWithSingleBitSet_ReturnsTrueOnlyForThatPosition(int pos, byte byteValue)
    {
        // Arrange
        byte b = byteValue;

        // Act
        bool result = b.IsBitSet(pos);

		// Assert
		Assert.True(result);
    }

    /// <summary>
    /// Tests that IsBitSet returns false when checking a position that is not set in a byte with a single bit set.
    /// Expected: Returns false for all positions except the one that is set.
    /// </summary>
    [Theory]
    [InlineData(0, 0b0000_0010)] // bit 0 unset, bit 1 set
    [InlineData(1, 0b0000_0001)] // bit 1 unset, bit 0 set
    [InlineData(3, 0b1000_0000)] // bit 3 unset, bit 7 set
    [InlineData(7, 0b0000_0001)] // bit 7 unset, bit 0 set
    public void IsBitSet_ByteWithSingleBitSet_ReturnsFalseForOtherPositions(int pos, byte byteValue)
    {
        // Arrange
        byte b = byteValue;

        // Act
        bool result = b.IsBitSet(pos);

		// Assert
		Assert.False(result);
    }

    /// <summary>
    /// Tests behavior of IsBitSet when position is negative.
    /// Expected: Due to bit shift behavior, negative positions may produce unexpected results or exceptions.
    /// This test documents the actual behavior.
    /// </summary>
    [Theory]
    [InlineData(-1)]
    [InlineData(-8)]
    public void IsBitSet_NegativePosition_ProducesResult(int pos)
    {
        // Arrange
        byte b = 0xFF;

        // Act & Assert
        Assert.Throws<IndexOutOfRangeException>(() => b.IsBitSet(pos));
    }

    /// <summary>
    /// Tests behavior of IsBitSet when position is greater than 7 (beyond byte boundary).
    /// Expected: Positions beyond 7 should produce results based on bit shift wrapping behavior.
    /// This test documents the actual behavior for out-of-range positions.
    /// </summary>
    [Theory]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(16)]
    [InlineData(31)]
    public void IsBitSet_PositionBeyondByteBoundary_ProducesResult(int pos)
    {
        // Arrange
        byte b = 0b1010_0101;

        // Act & Assert
        Assert.Throws<IndexOutOfRangeException>(() => b.IsBitSet(pos));
    }

    /// <summary>
    /// Tests behavior of IsBitSet with extreme position values.
    /// Expected: Documents behavior with int.MaxValue as position.
    /// </summary>
    [Fact]
    public void IsBitSet_PositionMaxValue_ProducesResult()
    {
        // Arrange
        byte b = 0xFF;
        int pos = int.MaxValue;

        // Act
        Assert.Throws<IndexOutOfRangeException>(() => b.IsBitSet(pos));
    }

    /// <summary>
    /// Tests behavior of IsBitSet with extreme negative position value.
    /// Expected: Documents behavior with int.MinValue as position.
    /// </summary>
    [Fact]
    public void IsBitSet_PositionMinValue_ProducesResult()
    {
        // Arrange
        byte b = 0xFF;
        int pos = int.MinValue;

        // Act
        Assert.Throws<IndexOutOfRangeException>(() => b.IsBitSet(pos));
    }

    /// <summary>
    /// Tests that IsBitSet correctly handles boundary byte values (byte.MinValue and byte.MaxValue).
    /// Expected: byte.MinValue (0) should return false for all positions, byte.MaxValue (255) should return true.
    /// </summary>
    [Theory]
    [InlineData(byte.MinValue, 0, false)]
    [InlineData(byte.MinValue, 7, false)]
    [InlineData(byte.MaxValue, 0, true)]
    [InlineData(byte.MaxValue, 7, true)]
    public void IsBitSet_BoundaryByteValues_ReturnsExpectedResult(byte b, int pos, bool expected)
    {
        // Act
        Assert.Equal(expected, b.IsBitSet(pos));
    }

    /// <summary>
    /// Tests that IsBitSet returns false when the ReadOnlySpan is empty, regardless of position.
    /// Input: Empty span, position 0.
    /// Expected: Returns false.
    /// </summary>
    [Fact]
    public void IsBitSet_EmptySpan_ReturnsFalse()
    {
        // Arrange
        ReadOnlySpan<byte> data = [];
        int pos = 0;

        // Act
        AssertThrowsIndexOutOfRange(data, pos);
    }

    /// <summary>
    /// Tests that IsBitSet returns false when position is far beyond the span length.
    /// Input: Single byte span, position 100.
    /// Expected: Returns false.
    /// </summary>
    [Fact]
    public void IsBitSet_PositionBeyondLength_ReturnsFalse()
    {
        // Arrange
        byte[] backing = [0b1111_1111];
        ReadOnlySpan<byte> data = new(backing);
        int pos = 100;

        // Act
        AssertThrowsIndexOutOfRange(data, pos);
    }

    /// <summary>
    /// Tests that IsBitSet returns false when position is at exact boundary where byteIndex equals data.Length.
    /// Input: Single byte span, position 7 (byteIndex = 7/7 = 1, which equals length).
    /// Expected: Returns false.
    /// </summary>
    [Fact]
    public void IsBitSet_PositionAtExactBoundary_ReturnsFalse()
    {
        // Arrange
        byte[] backing = [0b1111_1111];
        ReadOnlySpan<byte> data = new(backing);
        int pos = 7; // byteIndex = 1, equals data.Length

        // Act
        AssertThrowsIndexOutOfRange(data, pos);
    }

    /// <summary>
    /// Tests that IsBitSet returns false when position is int.MaxValue.
    /// Input: Single byte span, position int.MaxValue.
    /// Expected: Returns false.
    /// </summary>
    [Fact]
    public void IsBitSet_PositionIntMaxValue_ReturnsFalse()
    {
        // Arrange
        byte[] backing = [0xFF];
        ReadOnlySpan<byte> data = new(backing);
        int pos = int.MaxValue;

        // Act
        AssertThrowsIndexOutOfRange(data, pos);
    }

    /// <summary>
    /// Tests that IsBitSet handles negative positions correctly.
    /// Input: Single byte span with all bits set, position -1.
    /// Expected: Behavior depends on implementation (negative byteIndex/bitIndex).
    /// Note: -1 / 7 = 0, -1 % 7 = -1, which will call byte.IsBitSet(-1).
    /// </summary>
    [Fact]
    public void IsBitSet_NegativePosition_ReturnsExpectedResult()
    {
        // Arrange
        byte[] backing = [0b1111_1111];
        ReadOnlySpan<byte> data = new(backing);
        int pos = -1;

        // Act
        AssertThrowsIndexOutOfRange(data, pos);
    }

    /// <summary>
    /// Tests that IsBitSet handles int.MinValue position correctly.
    /// Input: Single byte span, position int.MinValue.
    /// Expected: Returns false (extreme negative value).
    /// </summary>
    [Fact]
    public void IsBitSet_PositionIntMinValue_ReturnsFalse()
    {
        // Arrange
        byte[] backing = [0xFF];
        ReadOnlySpan<byte> data = new(backing);
        int pos = int.MinValue;

        // Act
        AssertThrowsIndexOutOfRange(data, pos);
    }

    /// <summary>
    /// Tests that IsBitSet correctly checks bits in the first byte using 7-bit packing.
    /// Input: Various positions 0-6 mapping to first byte.
    /// Expected: Returns true/false based on bit pattern.
    /// </summary>
    [Theory]
    [InlineData(0, false)] // Bit 0 of 0b0100_0000 is not set
    [InlineData(1, false)] // Bit 1 is not set
    [InlineData(2, false)] // Bit 2 is not set
    [InlineData(3, false)] // Bit 3 is not set
    [InlineData(4, false)] // Bit 4 is not set
    [InlineData(5, false)] // Bit 5 is not set
    [InlineData(6, true)]  // Bit 6 is set
    public void IsBitSet_PositionsInFirstByte_ReturnsCorrectResult(int pos, bool expected)
    {
        // Arrange
        byte[] backing = [0b0100_0000]; // Only bit 6 is set
        ReadOnlySpan<byte> data = new(backing);
		Assert.Equal(expected, data.IsBitSet(pos));
	}

    /// <summary>
    /// Tests that IsBitSet correctly uses 7-bit packing to map positions across multiple bytes.
    /// Input: Positions 6 and 7 to verify transition from first to second byte.
    /// Expected: Position 6 maps to byte 0 bit 6, position 7 maps to byte 1 bit 0.
    /// </summary>
    [Fact]
    public void IsBitSet_PositionsCrossingByteBoundary_MapsCorrectly()
    {
        // Arrange
        // First byte: bit 6 set (0b0100_0000)
        // Second byte: bit 0 set (0b0000_0001)
        byte[] backing = [0b0100_0000, 0b0000_0001];
        ReadOnlySpan<byte> data = new(backing);

        // Act & Assert
        // pos = 6 -> byteIndex = 0, bitIndex = 6 -> first byte, bit 6
        Assert.True(data.IsBitSet(6));

        // pos = 7 -> byteIndex = 1, bitIndex = 0 -> second byte, bit 0
        Assert.True(data.IsBitSet(7));

        // pos = 0 -> byteIndex = 0, bitIndex = 0 -> first byte, bit 0 (not set)
        Assert.False(data.IsBitSet(0));

        // pos = 8 -> byteIndex = 1, bitIndex = 1 -> second byte, bit 1 (not set)
        Assert.False(data.IsBitSet(8));
    }

    /// <summary>
    /// Tests that IsBitSet correctly handles positions mapping to the second byte.
    /// Input: Positions 7-13 mapping to second byte using 7-bit packing.
    /// Expected: Returns correct bit values from second byte.
    /// </summary>
    [Theory]
    [InlineData(7, true)]   // pos 7 -> byte 1, bit 0 (set)
    [InlineData(8, false)]  // pos 8 -> byte 1, bit 1 (not set)
    [InlineData(9, true)]   // pos 9 -> byte 1, bit 2 (set)
    [InlineData(10, false)] // pos 10 -> byte 1, bit 3 (not set)
    [InlineData(11, true)]  // pos 11 -> byte 1, bit 4 (set)
    [InlineData(12, false)] // pos 12 -> byte 1, bit 5 (not set)
    [InlineData(13, true)]  // pos 13 -> byte 1, bit 6 (set)
    public void IsBitSet_PositionsInSecondByte_ReturnsCorrectResult(int pos, bool expected)
    {
        // Arrange
        // First byte: all zeros
        // Second byte: 0b0101_0101 (bits 0, 2, 4, 6 set)
        byte[] backing = [0b0000_0000, 0b0101_0101];
        ReadOnlySpan<byte> data = new(backing);
		Assert.Equal(expected, data.IsBitSet(pos));
	}

    /// <summary>
    /// Tests that IsBitSet works correctly with all bits set in multiple bytes.
    /// Input: Two bytes with all bits set, various positions.
    /// Expected: All positions within valid range return true.
    /// </summary>
    [Theory]
    [InlineData(0, true)]
    [InlineData(3, true)]
    [InlineData(6, true)]
    [InlineData(7, true)]
    [InlineData(10, true)]
    [InlineData(13, true)]
    public void IsBitSet_AllBitsSet_ReturnsTrue(int pos, bool expected)
    {
        // Arrange
        byte[] backing = [0xFF, 0xFF];
        ReadOnlySpan<byte> data = new(backing);
		Assert.Equal(expected, data.IsBitSet(pos));
	}

    /// <summary>
    /// Tests that IsBitSet works correctly with no bits set.
    /// Input: Multiple bytes with all bits cleared, various positions.
    /// Expected: All positions return false.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(10)]
    [InlineData(13)]
    public void IsBitSet_NoBitsSet_ReturnsFalse(int pos)
    {
        // Arrange
        byte[] backing = [0x00, 0x00, 0x00];
        ReadOnlySpan<byte> data = new(backing);
		Assert.False(data.IsBitSet(pos));
	}

    /// <summary>
    /// Tests that IsBitSet returns false when position maps to third byte but only two bytes exist.
    /// Input: Two byte span, position 14 (maps to third byte).
    /// Expected: Returns false.
    /// </summary>
    [Fact]
    public void IsBitSet_PositionMapsToNonExistentThirdByte_ReturnsFalse()
    {
        // Arrange
        byte[] backing = [0xFF, 0xFF];
        ReadOnlySpan<byte> data = new(backing);
        int pos = 14; // byteIndex = 14/7 = 2, but only indices 0-1 exist

        // Act
        AssertThrowsIndexOutOfRange(data, pos);
    }

    /// <summary>
    /// Tests that IsBitSet handles position zero correctly with single byte.
    /// Input: Single byte with specific pattern, position 0.
    /// Expected: Returns correct bit value at position 0.
    /// </summary>
    [Theory]
    [InlineData(0b0000_0001, true)]  // Bit 0 set
    [InlineData(0b0000_0000, false)] // Bit 0 not set
    [InlineData(0b1111_1110, false)] // Bit 0 not set
    public void IsBitSet_PositionZero_ReturnsCorrectResult(byte byteValue, bool expected)
    {
        // Arrange
        byte[] backing = [byteValue];
        ReadOnlySpan<byte> data = new(backing);
        int pos = 0;

        // Act
        bool result = data.IsBitSet(pos);
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that IsBitSet returns false when the position is out of bounds of the array.
    /// Uses 7-bit packing, so position is mapped to byteIndex = pos / 7.
    /// </summary>
    [Theory]
    [InlineData(7, 1)]  // pos=7 maps to byteIndex=1, array has only 1 byte
    [InlineData(8, 1)]  // pos=8 maps to byteIndex=1, array has only 1 byte
    [InlineData(14, 2)] // pos=14 maps to byteIndex=2, array has only 2 bytes
    [InlineData(100, 3)] // pos=100 maps to byteIndex=14, array has only 3 bytes
    [InlineData(int.MaxValue, 10)] // Very large position, array has 10 bytes
    public void IsBitSet_PositionOutOfBounds_ReturnsFalse(int pos, int arrayLength)
    {
        // Arrange
        byte[] data = new byte[arrayLength];

		// Act
		Assert.Throws<IndexOutOfRangeException>(() => data.IsBitSet(pos));
	}

    /// <summary>
    /// Tests that IsBitSet throws NullReferenceException when the array is null.
    /// </summary>
    [Fact]
    public void IsBitSet_NullArray_ThrowsNullReferenceException()
    {
        // Arrange
        byte[]? data = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => data!.IsBitSet(0));
    }

    /// <summary>
    /// Tests that IsBitSet returns false for any valid position when the array is empty.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(int.MaxValue)]
    public void IsBitSet_EmptyArray_ReturnsFalse(int pos)
    {
        // Arrange
        byte[] data = [];

		// Act
		Assert.Throws<IndexOutOfRangeException>(() => data.IsBitSet(pos));
	}

    /// <summary>
    /// Tests IsBitSet with various positions and bit patterns using 7-bit packing.
    /// Verifies correct mapping: pos/7 gives byte index, pos%7 gives bit index within that byte.
    /// </summary>
    [Theory]
    [InlineData(0, new byte[] { 0b0000_0001 }, true)]   // pos=0: byte[0] bit 0 is set
    [InlineData(0, new byte[] { 0b0000_0000 }, false)]  // pos=0: byte[0] bit 0 is not set
    [InlineData(1, new byte[] { 0b0000_0010 }, true)]   // pos=1: byte[0] bit 1 is set
    [InlineData(6, new byte[] { 0b0100_0000 }, true)]   // pos=6: byte[0] bit 6 is set
    [InlineData(6, new byte[] { 0b0000_0000 }, false)]  // pos=6: byte[0] bit 6 is not set
    [InlineData(7, new byte[] { 0b0000_0000, 0b0000_0001 }, true)]  // pos=7: byte[1] bit 0 is set
    [InlineData(7, new byte[] { 0b1111_1111, 0b0000_0000 }, false)] // pos=7: byte[1] bit 0 is not set
    [InlineData(8, new byte[] { 0b0000_0000, 0b0000_0010 }, true)]  // pos=8: byte[1] bit 1 is set
    [InlineData(13, new byte[] { 0b0000_0000, 0b0100_0000 }, true)] // pos=13: byte[1] bit 6 is set
    [InlineData(14, new byte[] { 0b0000_0000, 0b0000_0000, 0b0000_0001 }, true)] // pos=14: byte[2] bit 0 is set
    public void IsBitSet_ValidPositionWithBitPattern_ReturnsExpectedResult(int pos, byte[] data, bool expectedResult)
    {
		// Act
		Assert.Equal(expectedResult, data.IsBitSet(pos));
	}

    /// <summary>
    /// Tests IsBitSet at byte boundary positions (where position transitions from one byte to the next).
    /// With 7-bit packing: positions 6→7, 13→14, etc. are boundaries.
    /// </summary>
    [Fact]
    public void IsBitSet_BoundaryPositions_MapsCorrectlyAcrossBytes()
    {
        // Arrange
        // byte[0] has bit 6 set, byte[1] has bit 0 set, byte[2] has bit 6 set
        byte[] data = [0b0100_0000, 0b0000_0001, 0b0100_0000];

        // Act & Assert
        Assert.True(data.IsBitSet(6));
        Assert.True(data.IsBitSet(7));
        Assert.False(data.IsBitSet(13));
        Assert.False(data.IsBitSet(14));
        Assert.True(data.IsBitSet(20));
    }

    /// <summary>
    /// Tests IsBitSet with negative positions where byteIndex becomes negative,
    /// which should throw IndexOutOfRangeException when accessing the array.
    /// </summary>
    [Theory]
    [InlineData(-7)]
    [InlineData(-8)]
    [InlineData(-14)]
    [InlineData(int.MinValue)]
    public void IsBitSet_NegativePositionWithNegativeByteIndex_ThrowsIndexOutOfRangeException(int pos)
    {
        // Arrange
        byte[] data = [0xFF];

        // Act & Assert
        Assert.Throws<IndexOutOfRangeException>(() => data.IsBitSet(pos));
    }

    /// <summary>
    /// Tests IsBitSet with negative positions in range [-6, -1] where byteIndex = 0 but bitIndex is negative.
    /// The behavior depends on the byte.IsBitSet implementation with negative bit indices.
    /// This tests the actual behavior of the bit shift operation with negative indices.
    /// </summary>
    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-6)]
    public void IsBitSet_NegativePositionWithZeroByteIndex_CallsByteIsBitSetWithNegativeBitIndex(int pos)
    {
        // Arrange
        byte[] data = [0xFF];

        // Act
        Assert.Throws<IndexOutOfRangeException>(() => data.IsBitSet(pos));
    }

    /// <summary>
    /// Tests IsBitSet with position 0 on various byte patterns.
    /// Position 0 maps to byte[0] bit 0 (LSB).
    /// </summary>
    [Theory]
    [InlineData(0b0000_0001, true)]  // LSB set
    [InlineData(0b0000_0000, false)] // LSB not set
    [InlineData(0b1111_1110, false)] // All bits except LSB set
    [InlineData(0b1111_1111, true)]  // All bits set
    public void IsBitSet_Position0_ChecksFirstBitOfFirstByte(byte firstByte, bool expectedResult)
    {
        // Arrange
        byte[] data = [firstByte];

        // Act
        bool result = data.IsBitSet(0);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    /// <summary>
    /// Tests IsBitSet with a comprehensive bit pattern across multiple bytes
    /// to verify correct 7-bit packing logic throughout the array.
    /// </summary>
    [Fact]
    public void IsBitSet_MultipleBytes_Uses7BitPackingCorrectly()
    {
        // Arrange
        // Create a pattern where specific positions are set
        byte[] data =
		[
			0b0101_0101,  // byte[0]: bits 0,2,4,6 set
            0b1010_1010,  // byte[1]: bits 1,3,5,7 set
            0b0000_1111   // byte[2]: bits 0,1,2,3 set
        ];

        // Act & Assert
        // Positions 0-6 map to byte[0]
        Assert.True(data.IsBitSet(0));   // bit 0 set
        Assert.False(data.IsBitSet(1));  // bit 1 not set
        Assert.True(data.IsBitSet(2));   // bit 2 set
        Assert.False(data.IsBitSet(3));  // bit 3 not set
        Assert.True(data.IsBitSet(4));   // bit 4 set
        Assert.False(data.IsBitSet(5));  // bit 5 not set
        Assert.True(data.IsBitSet(6));   // bit 6 set

        // Positions 7-13 map to byte[1]
        Assert.False(data.IsBitSet(7));  // bit 0 not set
        Assert.True(data.IsBitSet(8));   // bit 1 set
        Assert.False(data.IsBitSet(9));  // bit 2 not set
        Assert.True(data.IsBitSet(10));  // bit 3 set
        Assert.False(data.IsBitSet(11)); // bit 4 not set
        Assert.True(data.IsBitSet(12));  // bit 5 set
        Assert.False(data.IsBitSet(13)); // bit 6 not set

        // Positions 14-20 map to byte[2]
        Assert.True(data.IsBitSet(14));  // bit 0 set
        Assert.True(data.IsBitSet(15));  // bit 1 set
        Assert.True(data.IsBitSet(16));  // bit 2 set
        Assert.True(data.IsBitSet(17));  // bit 3 set
        Assert.False(data.IsBitSet(18)); // bit 4 not set
        Assert.False(data.IsBitSet(19)); // bit 5 not set
        Assert.False(data.IsBitSet(20)); // bit 6 not set
    }

    /// <summary>
    /// Tests IsBitSet with position exactly at the upper boundary that maps to the last valid byte.
    /// Verifies behavior when position maps exactly to the last byte's last usable bit (bit 6).
    /// </summary>
    [Fact]
    public void IsBitSet_PositionAtExactArrayBoundary_ReturnsCorrectResult()
    {
        // Arrange
        // Array with 3 bytes, so valid positions are 0-20
        // Position 20 = byteIndex 2, bitIndex 6 (last valid position)
        byte[] data = [0x00, 0x00, 0b0100_0000];

        // Act & Assert
        Assert.True(data.IsBitSet(20));			// Exactly at boundary, bit is set
		AssertThrowsIndexOutOfRange(data, 21);  // Beyond boundary, throws
    }

	// Helper for ReadOnlySpan<byte> exception assertion
	private static void AssertThrowsIndexOutOfRange(ReadOnlySpan<byte> span, int pos)
	{
		try
		{
			span.IsBitSet(pos);
			Assert.Fail("Expected IndexOutOfRangeException was not thrown.");
		}
		catch (IndexOutOfRangeException)
		{
			// Expected
		}
	}
}