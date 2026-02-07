using System;
using Xunit;
using FTMS.NET.Utils;

namespace FTMS.NET.Tests;

public class ByteExtensionsTests
{
	[Fact]
	public void Byte_IsBitSet_ChecksAllBits0To7()
	{
		// 0b10100101 == 0xA5 == 165
		byte b = 0b1010_0101;

		Assert.True(b.IsBitSet(0));  // LSB = 1
		Assert.False(b.IsBitSet(1));
		Assert.True(b.IsBitSet(2));
		Assert.False(b.IsBitSet(3));
		Assert.False(b.IsBitSet(4));
		Assert.True(b.IsBitSet(5));
		Assert.False(b.IsBitSet(6));
		Assert.True(b.IsBitSet(7));  // MSB = 1
	}

	[Fact]
	public void Array_IsBitSet_Uses7BitPacking_MappingAcrossBytes()
	{
		// Erstes Byte hat Bit 6 gesetzt (0b0100_0000), zweites Byte hat Bit 0 gesetzt (0b0000_0001)
		byte[] data = new byte[] { 0b0100_0000, 0b0000_0001 };

		// pos = 6 -> byteIndex = 6/7 = 0, bitIndex = 6 -> aus erstem Byte
		Assert.True(data.IsBitSet(6));

		// pos = 7 -> byteIndex = 7/7 = 1, bitIndex = 0 -> aus zweitem Byte
		Assert.True(data.IsBitSet(7));

		// pos = 0 -> byteIndex = 0, bitIndex = 0 -> erstes Byte hat dieses Bit nicht gesetzt
		Assert.False(data.IsBitSet(0));
	}

	[Fact]
	public void ReadOnlySpan_IsBitSet_UsesSameLogicAsArray()
	{
		byte[] backing = new byte[] { 0b0100_0000, 0b0000_0001 };
		ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(backing);

		Assert.True(span.IsBitSet(6)); // maps to backing[0] bit 6
		Assert.True(span.IsBitSet(7)); // maps to backing[1] bit 0
		Assert.False(span.IsBitSet(0));
	}

	[Fact]
	public void IsBitSet_OutOfRangePosition_ReturnsFalse()
	{
		byte[] data = new byte[] { 0b0000_0000, 0b0000_0000 }; // length = 2
		int pos = data.Length * 7; // byteIndex == data.Length -> out of range

		Assert.False(data.IsBitSet(pos));
		Assert.False(new ReadOnlySpan<byte>(data).IsBitSet(pos));
	}
}