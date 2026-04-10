namespace FTMS.NET.Exceptions;

using System;

public sealed class FitnessMachineTypeNotDefinedException(EFitnessMachineType machineType)
	: Exception("The fitness machine type sent from the server is not defined.")
{
	public EFitnessMachineType MachineType { get; } = machineType;
}
