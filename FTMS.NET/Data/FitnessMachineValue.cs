namespace FTMS.NET.Data;
using System;

public sealed record FitnessMachineValue(Guid Uuid, double Value, string Name) : IFitnessMachineValue;
