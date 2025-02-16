namespace FTMS.NET;

using FTMS.NET.Exceptions;

public enum EFitnessMachineType
{
	Threadmill,
	CrossTrainer,
	StepClimber,
	StairClimber,
	Rower,
	IndoorBike
}

public static class EFitnessMachineTypeExtensions
{
	public static EFitnessMachineType EnsureType(this EFitnessMachineType fitnessMachineType)
	{
		if (Enum.IsDefined(fitnessMachineType) == false)
			throw new FitnessMachineTypeNotDefinedException(fitnessMachineType);

		return fitnessMachineType;
	}
}
