namespace FTMS.NET.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


/// Based on https://stackoverflow.com/a/12549260
[StructLayout(LayoutKind.Sequential)]
public readonly struct UInt24 : IEquatable<UInt24>, IComparable<UInt24>/*, INumber<UInt24>*/
{
	public static readonly UInt24 MaxValue = new(0xFF, 0xFF, 0xFF);
	public static readonly UInt24 MinValue = new(0x00, 0x00, 0x00);

	private const uint _maxValue = 0x00FFFFFF; // 16,777,215 // This is the *inclusive* upper-bound (i.e. max-value).

	//

	public static implicit operator uint(UInt24 self) => self.Value;
	public static implicit operator UInt24(ushort u16) => new(value: u16);

	//

	private readonly byte b0;
	private readonly byte b1;
	private readonly byte b2;

	public UInt24(byte b0, byte b1, byte b2)
	{
		this.b0 = b0;
		this.b1 = b1;
		this.b2 = b2;
	}

	public UInt24(uint value)
	{
		if (value > _maxValue)
			throw new ArgumentOutOfRangeException(
				paramName: nameof(value),
				actualValue: value,
				message: $"Value {value:N0} must be between 0 and {_maxValue:N0} (inclusive).");

		//

		this.b0 = (byte)(value & 0xFF);
		this.b1 = (byte)(value >> 8 & 0xFF);
		this.b2 = (byte)(value >> 16 & 0xFF);
	}

#if UNSAFE
    public unsafe Byte* Byte0 => &_b0;
#endif

	private int SignedValue => this.b0 | this.b1 << 8 | this.b2 << 16;
	public uint Value => (uint)this.SignedValue;

	//

	#region Struct Tedium + IEquatable<UInt24> + IComparable<UInt24>

	public override string ToString() => this.Value.ToString();
	public override int GetHashCode() => this.SignedValue;
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is UInt24 other && this.Equals(other: other);

	public bool Equals(UInt24 other) => this.Value.Equals(other.Value);
	public int CompareTo(UInt24 other) => this.Value.CompareTo(other.Value);

	public static int Compare(UInt24 left, UInt24 right) => left.Value.CompareTo(right.Value);

	//

	public static bool operator ==(UInt24 left, UInt24 right) => left.Equals(right);
	public static bool operator !=(UInt24 left, UInt24 right) => !left.Equals(right);

	public static bool operator >(UInt24 left, UInt24 right) => Compare(left, right) > 0;
	public static bool operator >=(UInt24 left, UInt24 right) => Compare(left, right) >= 0;

	public static bool operator <(UInt24 left, UInt24 right) => Compare(left, right) < 0;
	public static bool operator <=(UInt24 left, UInt24 right) => Compare(left, right) <= 0;

	#endregion
}