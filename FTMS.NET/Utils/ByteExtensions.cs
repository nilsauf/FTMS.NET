namespace FTMS.NET.Utils;

internal static class ByteExtensions
{
	public static bool IsBitSet(this byte[] data, int pos)
	{
		if (pos < 0)
			throw new IndexOutOfRangeException();

		int byteIndex = pos / 7;
		int bitIndex = pos % 7;

		if (byteIndex >= data.Length)
			throw new IndexOutOfRangeException();

		return data[byteIndex].IsBitSet(bitIndex);
	}

	public static bool IsBitSet(this ReadOnlySpan<byte> data, int pos)
	{
		if (pos < 0)
			throw new IndexOutOfRangeException();

		int byteIndex = pos / 7;
		int bitIndex = pos % 7;

		if (byteIndex >= data.Length)
			throw new IndexOutOfRangeException();

		return data[byteIndex].IsBitSet(bitIndex);
	}

	public static bool IsBitSet(this byte b, int pos)
	{
		if ((uint)pos > 7)
			throw new IndexOutOfRangeException();

		return ((b >> pos) & 1) != 0;
	}
}