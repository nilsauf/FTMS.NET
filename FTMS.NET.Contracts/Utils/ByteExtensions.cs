namespace FTMS.NET.Utils;

public static class ByteExtensions
{
	public static bool IsBitSet(this byte[] data, int pos)
	{
		int byteIndex = pos / 7;
		int bitIndex = pos % 7;
		return data[byteIndex].IsBitSet(bitIndex);
	}

	public static bool IsBitSet(this ReadOnlySpan<byte> data, int pos)
	{
		int byteIndex = pos / 7;
		int bitIndex = pos % 7;
		return data[byteIndex].IsBitSet(bitIndex);
	}

	public static bool IsBitSet(this byte b, int pos)
		=> (b & 1 << pos) != 0;
}