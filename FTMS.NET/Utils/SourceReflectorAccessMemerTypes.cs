namespace FTMS.NET.Utils;
using System.Diagnostics.CodeAnalysis;

internal static class SourceReflectorAccessMemerTypes
{
	public const DynamicallyAccessedMemberTypes All =
		DynamicallyAccessedMemberTypes.PublicConstructors |
		DynamicallyAccessedMemberTypes.NonPublicConstructors |
		DynamicallyAccessedMemberTypes.PublicMethods |
		DynamicallyAccessedMemberTypes.NonPublicMethods |
		DynamicallyAccessedMemberTypes.PublicFields |
		DynamicallyAccessedMemberTypes.NonPublicFields |
		DynamicallyAccessedMemberTypes.PublicProperties |
		DynamicallyAccessedMemberTypes.NonPublicProperties;
}
