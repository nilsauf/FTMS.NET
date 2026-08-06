namespace FTMS.NET.Tests.Utils;

using FTMS.NET.Utils;

/// <summary>
/// Unit tests for the <see cref="UInt24"/> struct.
/// </summary>
public sealed class UInt24Tests
{
    /// <summary>
    /// Tests that GetBytes returns the value encoded as three little-endian bytes (LSO...MSO).
    /// </summary>
    [Theory]
    [InlineData(0u, new byte[] { 0x00, 0x00, 0x00 })]
    [InlineData(1000u, new byte[] { 0xE8, 0x03, 0x00 })] // 0x0003E8
    [InlineData(0x010203u, new byte[] { 0x03, 0x02, 0x01 })]
    [InlineData(0xFFFFFFu, new byte[] { 0xFF, 0xFF, 0xFF })]
    public void GetBytes_ReturnsLittleEndianBytes(uint value, byte[] expected)
    {
        var u24 = new UInt24(value);

        byte[] actual = u24.GetBytes();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests that GetBytes on MaxValue returns three bytes of 0xFF.
    /// </summary>
    [Fact]
    public void GetBytes_MaxValue_ReturnsThreeBytesOfFF()
    {
        byte[] actual = UInt24.MaxValue.GetBytes();

        Assert.Equal(new byte[] { 0xFF, 0xFF, 0xFF }, actual);
    }
}
