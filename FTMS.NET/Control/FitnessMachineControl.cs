namespace FTMS.NET.Control;
using FTMS.NET.Exceptions;
using FTMS.NET.Utils;
using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

internal sealed class FitnessMachineControl : IFitnessMachineControl
{
	private readonly Func<byte[], Task> writeControlPoint;
	private readonly CancellationDisposable cancellationDisposable = new();
	private readonly IObservable<ControlResponse> responseObservable;

	internal FitnessMachineControl(IObservable<byte[]> observeControlPoint, Func<byte[], Task> writeControlPoint, IScheduler? scheduler = null)
	{
		this.writeControlPoint = writeControlPoint;
		this.responseObservable = observeControlPoint
			.TakeUntil(this.cancellationDisposable.Token)
			.Select(this.ReadResponseData)
			.Publish()
			.RefCount(TimeSpan.FromSeconds(5), scheduler ?? DefaultScheduler.Instance);
	}

	public async Task<ControlResponse> Execute(ControlRequest request)
	{
		ArgumentNullException.ThrowIfNull(request);

		var responseTask = this.responseObservable
			.FirstAsync(response => response.RequestedOpCode == request.OpCode)
			.ToTask();

		byte[] writeValue = [(byte)request.OpCode, .. request.Parameter];

		try
		{
			await this.writeControlPoint(writeValue);

			var response = await responseTask;

			if (response.ResultCode == EControlResultCode.Success)
				return response;

			throw new ControlRequestException(request.OpCode, response.ResultCode);
		}
		catch (Exception ex)
		{
			throw new ControlRequestException(request.OpCode, ex);
		}
	}

	private ControlResponse ReadResponseData(byte[] data)
	{
		var responseCode = data[0];

		if (responseCode != 0x80)
			throw new InvalidOperationException();

		var requestedOpCode = (EControlOpCode)data[1];
		var resultCode = (EControlResultCode)data[2];
		byte[] parameter = [.. data.Skip(3)];

		return new ControlResponse(requestedOpCode, resultCode, parameter);
	}

	public void Dispose()
	{
		this.cancellationDisposable.Dispose();
	}
}
