namespace FTMS.NET.Data;
using FTMS.NET.Data.Reader;
using FTMS.NET.Utils;
using SourceGeneration.Reflection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

internal sealed class FitnessMachineDataReader(
	[DynamicallyAccessedMembers(SourceReflectorAccessMemerTypes.All)]
	Type singleFrameReaderType)
{
	[DynamicallyAccessedMembers(SourceReflectorAccessMemerTypes.All)]
	private readonly Type singleFrameReaderTypeInfo = singleFrameReaderType.EnsureBaseTypeOf<SingleFrameReader>();

	public IEnumerable<IFitnessMachineValue> Read(byte[] dataFrame)
	{
		if (dataFrame.Length == 0)
			return [];

		using var singleFrameReader = (SingleFrameReader)SourceReflector.CreateInstance(this.singleFrameReaderTypeInfo, dataFrame);
		return singleFrameReader.ReadFrame();
	}
}