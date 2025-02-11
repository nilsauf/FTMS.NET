namespace FTMS.NET.Features;
public interface ISupportedRange
{
	double MinimumValue { get; }
	double MaximumValue { get; }
	double MinimumIncrement { get; }
}
