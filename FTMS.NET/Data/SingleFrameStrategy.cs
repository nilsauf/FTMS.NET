namespace FTMS.NET.Data;

using FTMS.NET.Utils;
using System;
using System.Collections.Immutable;

internal class SingleFrameStrategy
{
	public required int FlagFieldLength { get; init; }

	public required IImmutableList<SingleValueRule> SingleValueRules { get; init; }
}

internal record SingleValueRule(
	Guid ValueUuid,
	bool CheckIfBitIsSet,
	int BitPostion,
	Type RawValueType,
	ValueCalculation Calculation);
