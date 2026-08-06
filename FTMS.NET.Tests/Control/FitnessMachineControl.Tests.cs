namespace FTMS.NET.Tests.Control;

using FTMS.NET.Control;
using FTMS.NET.Exceptions;
using Microsoft.Reactive.Testing;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

public sealed class FitnessMachineControlTests
{
	[Fact(Timeout = 10_000)]
	public async Task Execute_NoResponseWithinTimeout_ThrowsControlRequestException()
	{
		var scheduler = new TestScheduler();
		var controlPoint = new Subject<byte[]>();
		var control = new FitnessMachineControl(
			controlPoint,
			_ => Task.CompletedTask,
			responseTimeout: TimeSpan.FromSeconds(5),
			scheduler);

		var executeTask = control.Execute(new ControlRequest(EControlOpCode.RequestControl, []));

		scheduler.AdvanceBy(TimeSpan.FromSeconds(5).Ticks);

		await Assert.ThrowsAsync<ControlRequestException>(() => executeTask);
	}

	[Fact]
	public async Task Execute_ValidResponseIndicated_ReturnsResponse()
	{
		var controlPoint = new Subject<byte[]>();
		var control = new FitnessMachineControl(
			controlPoint,
			_ => Task.CompletedTask,
			responseTimeout: TimeSpan.FromSeconds(5));

		var executeTask = control.Execute(new ControlRequest(EControlOpCode.RequestControl, []));

		controlPoint.OnNext([0x80, 0x00, 0x01]);

		var response = await executeTask;
		Assert.Equal(EControlOpCode.RequestControl, response.RequestedOpCode);
		Assert.Equal(EControlResultCode.Success, response.ResultCode);
	}

	[Fact]
	public async Task Execute_NonSuccessResultCode_ThrowsControlRequestException()
	{
		var controlPoint = new Subject<byte[]>();
		var control = new FitnessMachineControl(
			controlPoint,
			_ => Task.CompletedTask,
			responseTimeout: TimeSpan.FromSeconds(5));

		var executeTask = control.Execute(new ControlRequest(EControlOpCode.RequestControl, []));

		controlPoint.OnNext([0x80, 0x00, 0x03]);

		await Assert.ThrowsAsync<ControlRequestException>(() => executeTask);
	}

	[Fact]
	public async Task Execute_WriteThrows_WrapsInControlRequestException()
	{
		var control = new FitnessMachineControl(
			Observable.Never<byte[]>(),
			_ => throw new Exception("simulated ATT error"),
			responseTimeout: TimeSpan.FromSeconds(5));

		await Assert.ThrowsAsync<ControlRequestException>(() =>
			control.Execute(new ControlRequest(EControlOpCode.RequestControl, [])));
	}
}
