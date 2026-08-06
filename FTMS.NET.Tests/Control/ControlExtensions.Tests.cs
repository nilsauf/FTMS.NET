namespace FTMS.NET.Tests.Control;

using FTMS.NET.Control;
using FTMS.NET.Utils;

public sealed class ControlExtensionsTests
{
	[Fact]
	public async Task SetTargetedDistance_SendsOpCodeWithLittleEndianThreeByteParameter()
	{
		var control = new FakeControl();

		await control.SetTargetedDistance(new UInt24(1000)); // 0x0003E8

		Assert.NotNull(control.Request);
		Assert.Equal(EControlOpCode.SetTargetedDistance, control.Request!.OpCode);
		Assert.Equal(new byte[] { 0xE8, 0x03, 0x00 }, control.Request!.Parameter);
	}

	[Fact]
	public async Task SetTargetedDistance_MaxValue_SendsThreeByteLittleEndian()
	{
		var control = new FakeControl();

		await control.SetTargetedDistance(UInt24.MaxValue); // 0xFFFFFF

		Assert.NotNull(control.Request);
		Assert.Equal(new byte[] { 0xFF, 0xFF, 0xFF }, control.Request!.Parameter);
	}

	private sealed class FakeControl : IFitnessMachineControl
	{
		public ControlRequest? Request { get; private set; }

		public Task<ControlResponse> Execute(ControlRequest request)
		{
			this.Request = request;
			return Task.FromResult(new ControlResponse(request.OpCode, EControlResultCode.Success, []));
		}

		public void Dispose() { }
	}
}