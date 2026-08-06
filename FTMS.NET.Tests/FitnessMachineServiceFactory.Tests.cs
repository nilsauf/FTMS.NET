namespace FTMS.NET.Tests;

using System.Reactive;
using System.Reactive.Linq;

public sealed class FitnessMachineServiceFactoryTests
{
	[Fact]
	public async Task ReadFitnessMachineFeaturesAsync_SpeedTargetSettingInByte4_ReportsSupported()
	{
		byte[] value = new byte[8];
		value[4] = 0x01; // Target Setting Features bit 0 -> Speed Target Setting Supported

		var features = await new FakeConnection(value).ReadFitnessMachineFeaturesAsync();

		Assert.True(features.SpeedTargetSettingSupported);
		Assert.False(features.AverageSpeedSupported);
	}

	[Fact]
	public async Task ReadFitnessMachineFeaturesAsync_DistanceTargetSettingInByte5_ReportsSupported()
	{
		byte[] value = new byte[8];
		value[5] = 0x01; // Target Setting Features bit 8 -> Targeted Distance Configuration Supported

		var features = await new FakeConnection(value).ReadFitnessMachineFeaturesAsync();

		Assert.True(features.TargetedDistanceConfigurationSupported);
		Assert.False(features.SpeedTargetSettingSupported);
	}

	private sealed class FakeCharacteristic(byte[] value) : IFitnessMachineCharacteristic
	{
		public Guid Id { get; } = Guid.NewGuid();
		public Task<byte[]> ReadValueAsync() => Task.FromResult(value);
		public Task WriteValueAsync(byte[] value) => Task.CompletedTask;
		public IObservable<byte[]> ObserveValue() => Observable.Empty<byte[]>();
	}

	private sealed class FakeConnection(byte[] featureValue) : IFitnessMachineServiceConnection
	{
		public byte[] ServiceData { get; } = [];
		public Task<IFitnessMachineCharacteristic?> GetCharacteristicAsync(Guid id)
			=> Task.FromResult<IFitnessMachineCharacteristic?>(
				id == FtmsUuids.Feature ? new FakeCharacteristic(featureValue) : null);
	}
}
