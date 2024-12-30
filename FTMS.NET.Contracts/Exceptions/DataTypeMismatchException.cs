namespace FTMS.NET.Exceptions;
using System;

using FTMS.NET;

public class DataTypeMismatchException(EFitnessMachineType dataType, EFitnessMachineType readerType) : Exception(ExceptionMessage)
{
	private const string ExceptionMessage = "The type of reader is not compatible with the requested data type.";

	public EFitnessMachineType DataType { get; } = dataType;
	public EFitnessMachineType ReaderType { get; } = readerType;
}
