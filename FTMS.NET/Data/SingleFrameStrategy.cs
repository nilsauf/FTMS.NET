namespace FTMS.NET.Data;

using FTMS.NET.Utils;
using System;
using System.Collections.Immutable;

internal class SingleFrameStrategy
{
	public required int FlagFieldLength { get; init; }

	public required IImmutableList<SingleValueRule> SingleValueRules { get; init; }
}

internal enum RawValueType
{
    Byte,
    SByte,
    Short,
    UShort,
    Int,
    UInt,
    UInt24,
    Long,
    ULong,
    Float,
    Double
}

internal record SingleValueRule(
    Guid ValueUuid,
    bool CheckIfBitIsSet,
    int BitPosition,
    RawValueType RawValueType,
    ValueCalculation Calculation);
