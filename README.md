# FTMS.NET

This Repository holds a Fitness Machine Service (FTMS) Client Library for .NET.
FTMS is a service protocol for Bluetooth Low Energy Devices.

**Supported Fitness Maschine Types**

- Threadmill   :no_entry:
- CrossTrainer :no_entry:
- StepClimber  :no_entry:
- StairClimber :no_entry:
- Rower        :no_entry:
- IndoorBike   :heavy_check_mark:

**Contributions to support other maschines types are welcome!**

You can find further documentation on this BLE Service in the supplied PDF files (see docs folder).

## Creation

The app already needs to be connected to the BLE device. You can use a BLE library of you choice.
It needs to be able to observe and write characteristic values. Please create a class implementing `IFitnessMachineServiceConnection` for the used library.

To create a `IFitnessMachineService<TFitnessMachineData>` object to interact with the service, you need to know which type of device you are using.
Use the static method `FitnessMachineService.Create<TFitnessMachineData>(connection)` with the conection and the data type you want to use:
- FTMS.NET.IndoorBike.IndoorBikeData


## Tested Maschines

Every device, which supports FTMS, should work.

### Indoor Bikes

- VanRysel D100