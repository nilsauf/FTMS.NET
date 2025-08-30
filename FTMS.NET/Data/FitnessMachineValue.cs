namespace FTMS.NET.Data;
using System;

public record FitnessMachineValue(Guid Uuid, double Value, string Name) : IFitnessMachineValue;
