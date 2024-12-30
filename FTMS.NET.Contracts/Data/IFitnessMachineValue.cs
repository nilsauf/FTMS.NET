namespace FTMS.NET.Data;
using System;

public interface IFitnessMachineValue
{
	Guid Uuid { get; }
	double Value { get; }
	string Name { get; }
}
