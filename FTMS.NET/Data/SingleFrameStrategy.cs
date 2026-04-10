namespace FTMS.NET.Data;

using FTMS.NET.Utils;
using System;
using System.Collections.Immutable;

internal sealed class SingleFrameStrategy
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

internal sealed record SingleValueRule(
    Guid ValueUuid,
    bool CheckIfBitIsSet,
    int BitPosition,
    RawValueType RawValueType,
    ValueCalculation Calculation);
