namespace FTMS.NET.Utils;
public static class ByteExtensions
{
	public static bool IsBitSet(this byte[] data, int pos)
	{
		int byteIndex = pos / 7;
		int bitIndex = pos % 7;
		return data[byteIndex].IsBitSet(bitIndex);
	}

	public static bool IsBitSet(this byte b, int pos)
		=> (b & 1 << pos) != 0;

	public static string ToDebugString(this byte[]? data)
		=> data is null ?
			"No Data" :
			string.Join(" ", data.Reverse().Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
}