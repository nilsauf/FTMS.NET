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
	/// Expected: Negative positions throw IndexOutOfRangeException.
	/// </summary>
	[Theory]
	[InlineData(-1)]
	[InlineData(-8)]
	public void IsBitSet_NegativeBytePosition_ThrowsIndexOutOfRange(int pos)
	{
		// Arrange
		byte b = 0xFF;

		// Act & Assert
		Assert.Throws<IndexOutOfRangeException>(() => b.IsBitSet(pos));
	}

	/// <summary>
	/// Tests behavior of IsBitSet when position is greater than 7 (beyond byte boundary).
	/// Expected: Positions beyond 7 throw IndexOutOfRangeException.
	/// </summary>
	[Theory]
	[InlineData(8)]
	[InlineData(9)]
	[InlineData(16)]
	[InlineData(31)]
	public void IsBitSet_PositionBeyondByteBoundary_ThrowsIndexOutOfRange(int pos)
	{
		// Arrange
		byte b = 0b1010_0101;

		// Act & Assert
		Assert.Throws<IndexOutOfRangeException>(() => b.IsBitSet(pos));
	}

	/// <summary>
	/// Tests behavior of IsBitSet with extreme position values.
	/// Expected: int.MaxValue throws IndexOutOfRangeException.
	/// </summary>
	[Fact]
	public void IsBitSet_PositionMaxValue_ThrowsIndexOutOfRange()
	{
		// Arrange
		byte b = 0xFF;
		int pos = int.MaxValue;

		// Act
		Assert.Throws<IndexOutOfRangeException>(() => b.IsBitSet(pos));
	}

	/// <summary>
	/// Tests behavior of IsBitSet with extreme negative position value.
	/// Expected: int.MinValue throws IndexOutOfRangeException.
	/// </summary>
	[Fact]
	public void IsBitSet_PositionMinValue_ThrowsIndexOutOfRange()
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
	/// Tests that IsBitSet throws IndexOutOfRangeException when the ReadOnlySpan is empty, regardless of position.
	/// Input: Empty span, position 0.
	/// </summary>
	[Fact]
	public void IsBitSet_EmptySpan_ThrowsIndexOutOfRange()
	{
		// Arrange
		ReadOnlySpan<byte> data = [];
		int pos = 0;

		// Act
		AssertThrowsIndexOutOfRange(data, pos);
	}

	/// <summary>
	/// Tests that IsBitSet throws IndexOutOfRangeException when position is far beyond the span length.
	/// Input: Single byte span, position 100.
	/// </summary>
	[Fact]
	public void IsBitSet_PositionBeyondLength_ThrowsIndexOutOfRange()
	{
		// Arrange
		byte[] backing = [0b1111_1111];
		ReadOnlySpan<byte> data = new(backing);
		int pos = 100;

		// Act
		AssertThrowsIndexOutOfRange(data, pos);
	}

	/// <summary>
	/// Tests that IsBitSet reads the MSB of a single byte at position 7 and throws beyond it.
	/// Input: Single byte span, positions 7 and 8.
	/// Expected: Position 7 returns true (byteIndex = 0, bitIndex = 7); position 8 throws (byteIndex = 1, equals length).
	/// </summary>
	[Fact]
	public void IsBitSet_PositionAtExactBoundary_ReturnsCorrectResult()
	{
		// Arrange
		byte[] backing = [0b1111_1111];
		ReadOnlySpan<byte> data = new(backing);

		// Act & Assert
		Assert.True(data.IsBitSet(7));          // byte[0] bit 7 (MSB), valid
		AssertThrowsIndexOutOfRange(data, 8);   // byteIndex = 1, equals data.Length
	}

	/// <summary>
	/// Tests that IsBitSet throws IndexOutOfRangeException when position is int.MaxValue.
	/// Input: Single byte span, position int.MaxValue.
	/// </summary>
	[Fact]
	public void IsBitSet_PositionIntMaxValue_ThrowsIndexOutOfRange()
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
	/// Note: -1 / 8 = 0, -1 % 8 = -1, which will call byte.IsBitSet(-1) and throw.
	/// </summary>
	[Fact]
	public void IsBitSet_SpanNegativePosition_ThrowsIndexOutOfRange()
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
	/// Expected: Throws IndexOutOfRangeException (extreme negative value).
	/// </summary>
	[Fact]
	public void IsBitSet_PositionIntMinValue_ThrowsIndexOutOfRange()
	{
		// Arrange
		byte[] backing = [0xFF];
		ReadOnlySpan<byte> data = new(backing);
		int pos = int.MinValue;

		// Act
		AssertThrowsIndexOutOfRange(data, pos);
	}

	/// <summary>
	/// Tests that IsBitSet correctly checks bits in the first byte using standard bit numbering.
	/// Input: Various positions 0-6 mapping to the first byte.
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
	/// Tests that IsBitSet correctly uses standard bit numbering to map positions across multiple bytes.
	/// Input: Positions 7 and 8 to verify transition from first to second byte.
	/// Expected: Position 7 maps to byte 0 bit 7 (MSB), position 8 maps to byte 1 bit 0 (LSB).
	/// </summary>
	[Fact]
	public void IsBitSet_PositionsCrossingByteBoundary_MapsCorrectly()
	{
		// Arrange
		// First byte: bit 7 set (0b1000_0000)
		// Second byte: bit 0 set (0b0000_0001)
		byte[] backing = [0b1000_0000, 0b0000_0001];
		ReadOnlySpan<byte> data = new(backing);

		// Act & Assert
		// pos = 7 -> byteIndex = 0, bitIndex = 7 -> first byte, bit 7
		Assert.True(data.IsBitSet(7));

		// pos = 8 -> byteIndex = 1, bitIndex = 0 -> second byte, bit 0
		Assert.True(data.IsBitSet(8));

		// pos = 6 -> byteIndex = 0, bitIndex = 6 -> first byte, bit 6 (not set)
		Assert.False(data.IsBitSet(6));

		// pos = 9 -> byteIndex = 1, bitIndex = 1 -> second byte, bit 1 (not set)
		Assert.False(data.IsBitSet(9));
	}

	/// <summary>
	/// Tests that IsBitSet correctly handles positions mapping to the second byte.
	/// Input: Positions 8-15 mapping to second byte using standard bit numbering.
	/// Expected: Returns correct bit values from second byte.
	/// </summary>
	[Theory]
	[InlineData(8, true)]   // pos 8 -> byte 1, bit 0 (set)
	[InlineData(9, false)]  // pos 9 -> byte 1, bit 1 (not set)
	[InlineData(10, true)]  // pos 10 -> byte 1, bit 2 (set)
	[InlineData(11, false)] // pos 11 -> byte 1, bit 3 (not set)
	[InlineData(12, true)]  // pos 12 -> byte 1, bit 4 (set)
	[InlineData(13, false)] // pos 13 -> byte 1, bit 5 (not set)
	[InlineData(14, true)]  // pos 14 -> byte 1, bit 6 (set)
	[InlineData(15, false)] // pos 15 -> byte 1, bit 7 (not set)
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
	[InlineData(0)]
	[InlineData(3)]
	[InlineData(6)]
	[InlineData(7)]
	[InlineData(8)]
	[InlineData(10)]
	[InlineData(13)]
	[InlineData(15)]
	public void IsBitSet_AllBitsSet_ReturnsTrue(int pos)
	{
		// Arrange
		byte[] backing = [0xFF, 0xFF];
		ReadOnlySpan<byte> data = new(backing);
		Assert.True(data.IsBitSet(pos));
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
	[InlineData(8)]
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
	/// Tests that IsBitSet throws IndexOutOfRangeException when position maps to a non-existent byte.
	/// Input: Two byte span, position 16 (maps to third byte).
	/// </summary>
	[Fact]
	public void IsBitSet_PositionMapsToNonExistentThirdByte_ThrowsIndexOutOfRange()
	{
		// Arrange
		byte[] backing = [0xFF, 0xFF];
		ReadOnlySpan<byte> data = new(backing);
		int pos = 16; // byteIndex = 16/8 = 2, but only indices 0-1 exist

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
	/// Tests that IsBitSet throws IndexOutOfRangeException when the position is out of bounds of the array.
	/// Uses standard bit numbering, so position is mapped to byteIndex = pos / 8.
	/// </summary>
	[Theory]
	[InlineData(8, 1)]      // pos=8 maps to byteIndex=1, array has only 1 byte
	[InlineData(9, 1)]      // pos=9 maps to byteIndex=1, array has only 1 byte
	[InlineData(16, 2)]     // pos=16 maps to byteIndex=2, array has only 2 bytes
	[InlineData(100, 3)]    // pos=100 maps to byteIndex=12, array has only 3 bytes
	[InlineData(int.MaxValue, 10)] // Very large position, array has 10 bytes
	public void IsBitSet_PositionOutOfBounds_ThrowsIndexOutOfRange(int pos, int arrayLength)
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
	/// Tests that IsBitSet throws IndexOutOfRangeException for any valid position when the array is empty.
	/// </summary>
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(int.MaxValue)]
	public void IsBitSet_EmptyArray_ThrowsIndexOutOfRange(int pos)
	{
		// Arrange
		byte[] data = [];

		// Act
		Assert.Throws<IndexOutOfRangeException>(() => data.IsBitSet(pos));
	}

	/// <summary>
	/// Tests IsBitSet with various positions and bit patterns using standard bit numbering.
	/// Verifies correct mapping: pos/8 gives byte index, pos%8 gives bit index within that byte.
	/// </summary>
	[Theory]
	[InlineData(0, new byte[] { 0b0000_0001 }, true)]   // pos=0: byte[0] bit 0 is set
	[InlineData(0, new byte[] { 0b0000_0000 }, false)]  // pos=0: byte[0] bit 0 is not set
	[InlineData(1, new byte[] { 0b0000_0010 }, true)]   // pos=1: byte[0] bit 1 is set
	[InlineData(6, new byte[] { 0b0100_0000 }, true)]   // pos=6: byte[0] bit 6 is set
	[InlineData(6, new byte[] { 0b0000_0000 }, false)]  // pos=6: byte[0] bit 6 is not set
	[InlineData(7, new byte[] { 0b1000_0000 }, true)]   // pos=7: byte[0] bit 7 is set
	[InlineData(7, new byte[] { 0b1111_1111, 0b0000_0000 }, true)]   // pos=7: byte[0] bit 7 is set, byte[1] bit 0 is not relevant
	[InlineData(8, new byte[] { 0b0000_0000, 0b0000_0010 }, false)]  // pos=8: byte[1] bit 1 is set, not bit 0
	[InlineData(8, new byte[] { 0b0000_0000, 0b0000_0001 }, true)]   // pos=8: byte[1] bit 0 is set
	[InlineData(13, new byte[] { 0b0000_0000, 0b0010_0000 }, true)]  // pos=13: byte[1] bit 5 is set
	[InlineData(16, new byte[] { 0b0000_0000, 0b0000_0000, 0b0000_0001 }, true)] // pos=16: byte[2] bit 0 is set
	public void IsBitSet_ValidPositionWithBitPattern_ReturnsExpectedResult(int pos, byte[] data, bool expectedResult)
	{
		// Act
		Assert.Equal(expectedResult, data.IsBitSet(pos));
	}

	/// <summary>
	/// Tests IsBitSet at byte boundary positions (where position transitions from one byte to the next).
	/// With standard bit numbering: positions 7→8, 15→16, etc. are boundaries.
	/// </summary>
	[Fact]
	public void IsBitSet_BoundaryPositions_MapsCorrectlyAcrossBytes()
	{
		// Arrange
		// byte[0] has bit 6 set, byte[1] has bit 0 set, byte[2] has bit 6 set
		byte[] data = [0b0100_0000, 0b0000_0001, 0b0100_0000];

		// Act & Assert
		Assert.True(data.IsBitSet(6));   // byte[0] bit 6
		Assert.False(data.IsBitSet(7));  // byte[0] bit 7 not set
		Assert.True(data.IsBitSet(8));   // byte[1] bit 0
		Assert.False(data.IsBitSet(13)); // byte[1] bit 5 not set
		Assert.False(data.IsBitSet(14)); // byte[1] bit 6 not set
		Assert.False(data.IsBitSet(16)); // byte[2] bit 0 not set
		Assert.True(data.IsBitSet(22));  // byte[2] bit 6
	}

	/// <summary>
	/// Tests IsBitSet with negative positions where byteIndex becomes negative,
	/// which should throw IndexOutOfRangeException when accessing the array.
	/// </summary>
	[Theory]
	[InlineData(-8)]
	[InlineData(-9)]
	[InlineData(-16)]
	[InlineData(int.MinValue)]
	public void IsBitSet_NegativePositionWithNegativeByteIndex_ThrowsIndexOutOfRangeException(int pos)
	{
		// Arrange
		byte[] data = [0xFF];

		// Act & Assert
		Assert.Throws<IndexOutOfRangeException>(() => data.IsBitSet(pos));
	}

	/// <summary>
	/// Tests IsBitSet with negative positions in range [-7, -1] where byteIndex = 0 but bitIndex is negative.
	/// The byte.IsBitSet implementation throws for negative bit indices.
	/// </summary>
	[Theory]
	[InlineData(-1)]
	[InlineData(-2)]
	[InlineData(-7)]
	public void IsBitSet_NegativePositionWithZeroByteIndex_ThrowsIndexOutOfRangeException(int pos)
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
	/// to verify correct standard bit numbering throughout the array.
	/// </summary>
	[Fact]
	public void IsBitSet_MultipleBytes_UsesStandardBitMappingCorrectly()
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
		// Positions 0-7 map to byte[0]
		Assert.True(data.IsBitSet(0));   // bit 0 set
		Assert.False(data.IsBitSet(1));  // bit 1 not set
		Assert.True(data.IsBitSet(2));   // bit 2 set
		Assert.False(data.IsBitSet(3));  // bit 3 not set
		Assert.True(data.IsBitSet(4));   // bit 4 set
		Assert.False(data.IsBitSet(5));  // bit 5 not set
		Assert.True(data.IsBitSet(6));   // bit 6 set
		Assert.False(data.IsBitSet(7));  // bit 7 not set

		// Positions 8-15 map to byte[1]
		Assert.False(data.IsBitSet(8));  // bit 0 not set
		Assert.True(data.IsBitSet(9));   // bit 1 set
		Assert.False(data.IsBitSet(10)); // bit 2 not set
		Assert.True(data.IsBitSet(11));  // bit 3 set
		Assert.False(data.IsBitSet(12)); // bit 4 not set
		Assert.True(data.IsBitSet(13));  // bit 5 set
		Assert.False(data.IsBitSet(14)); // bit 6 not set
		Assert.True(data.IsBitSet(15));  // bit 7 set

		// Positions 16-23 map to byte[2]
		Assert.True(data.IsBitSet(16));  // bit 0 set
		Assert.True(data.IsBitSet(17));  // bit 1 set
		Assert.True(data.IsBitSet(18));  // bit 2 set
		Assert.True(data.IsBitSet(19));  // bit 3 set
		Assert.False(data.IsBitSet(20)); // bit 4 not set
		Assert.False(data.IsBitSet(21)); // bit 5 not set
		Assert.False(data.IsBitSet(22)); // bit 6 not set
		Assert.False(data.IsBitSet(23)); // bit 7 not set
	}

	/// <summary>
	/// Tests IsBitSet with position exactly at the upper boundary that maps to the last valid byte.
	/// Verifies behavior when position maps exactly to the last byte's last usable bit (bit 7).
	/// </summary>
	[Fact]
	public void IsBitSet_PositionAtExactArrayBoundary_ReturnsCorrectResult()
	{
		// Arrange
		// Array with 3 bytes, so valid positions are 0-23
		// Position 23 = byteIndex 2, bitIndex 7 (last valid position)
		byte[] data = [0x00, 0x00, 0b1000_0000];

		// Act & Assert
		Assert.True(data.IsBitSet(23));         // Exactly at boundary, bit is set
		AssertThrowsIndexOutOfRange(data, 24);  // Beyond boundary, throws
	}

	/// <summary>
	/// Tests that the MSB of the first byte is bit 7 using standard bit numbering.
	/// Expected: pos 7 reads byte[0] bit 7, pos 15 reads byte[1] bit 7.
	/// </summary>
	[Fact]
	public void IsBitSet_StandardBitNumbering_MsbOfFirstByteIsBit7()
	{
		byte[] data = [0b1000_0000, 0b0000_0000];

		Assert.True(data.IsBitSet(7));
		Assert.False(data.IsBitSet(15));   // byte[1] bit 7, not set
	}

	/// <summary>
	/// Tests that the LSB of the second byte is bit 8 using standard bit numbering.
	/// Expected: pos 8 reads byte[1] bit 0, pos 7 reads byte[0] bit 7.
	/// </summary>
	[Fact]
	public void IsBitSet_StandardBitNumbering_LsbOfSecondByteIsBit8()
	{
		byte[] data = [0b0000_0000, 0b0000_0001];

		Assert.True(data.IsBitSet(8));
		Assert.False(data.IsBitSet(7));    // byte[0] bit 7, not set
	}

	/// <summary>
	/// Tests that a single byte with its MSB set does not overflow at position 7.
	/// Expected: pos 7 returns true instead of throwing IndexOutOfRangeException.
	/// </summary>
	[Fact]
	public void IsBitSet_SingleByte_MsbDoesNotOverflow()
	{
		byte[] data = [0b1000_0000];

		Assert.True(data.IsBitSet(7));     // byte[0] bit 7 (MSB)
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