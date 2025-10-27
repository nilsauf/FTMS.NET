# FTMS.NET

This repository holds a Fitness Machine Service (FTMS) Client Library for .NET.
FTMS is a service protocol for Bluetooth Low Energy Devices.

You can find further documentation on this BLE Service in the supplied PDF files (see docs folder).

:warning: **All Fitness Machine Types are supported, but not all are tested! (see below)** :warning:

## Creation

The app already needs to be connected to the BLE device. You can use a BLE library of you choice. Please create a class implementing `IFitnessMachineServiceConnection` for the used library.

Use the static extension methods for `IFitnessMachineServiceConnection` to create a complete `IFitnessMachineService` or just a part of it.

## Remarks

### Fitness Machine Features

Although the library is able to read the supported features flags and ranges from the machine into `IFitnessMachineService.Features`, the library will not use this information as not all manufacturers will set all the flags correctly.

Eg the control request to change the target power will not check if the new target power is in the supported power range of the machine.

## Tested Machines

In theory every device, which supports FTMS, should work.

### Indoor Bikes

- VanRysel D100