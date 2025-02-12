namespace FTMS.NET.Exceptions;
using System;

public class FitnessMachineNotAvailableException()
	: Exception("The service data indicate, that the fitness machine service is not available.")
{
}
