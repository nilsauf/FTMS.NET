namespace FTMS.NET.Exceptions;
using System;

public class FitnessMachineDataCreationException(Exception ex) : Exception(ExceptionMessage, ex)
{
	private const string ExceptionMessage = "The FitnessMachineData could not be created! " +
		"The data type needs to derive from FitnessMachineData<TData> and " +
		"needs to implement a constructur with a single IObservable<byte[]> parameter.";
}
