namespace FTMS.NET.Data;
using System.Collections.Generic;

internal sealed class FitnessMachineDataReader(SingleFrameStrategy singleFrameStrategy)
{
	public IEnumerable<IFitnessMachineValue> Read(byte[] dataFrame)
	{
		if (dataFrame.Length == 0)
			return [];

		using var singleFrameReader = new SingleFrameReader(dataFrame, singleFrameStrategy);
		return singleFrameReader.ReadFrame();
	}
}