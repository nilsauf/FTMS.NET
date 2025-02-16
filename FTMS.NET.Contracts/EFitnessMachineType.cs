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

	public static Guid GetDataCharacteristicId(this EFitnessMachineType fitnessMachineType)
		=> fitnessMachineType switch
		{
			EFitnessMachineType.Threadmill => FtmsUuids.TreadmillData,
			EFitnessMachineType.CrossTrainer => FtmsUuids.CrossTrainerData,
			EFitnessMachineType.StepClimber => FtmsUuids.StepClimberData,
			EFitnessMachineType.StairClimber => FtmsUuids.StairClimberData,
			EFitnessMachineType.Rower => FtmsUuids.RowerData,
			EFitnessMachineType.IndoorBike => FtmsUuids.IndoorBikeData,
			_ => throw new InvalidOperationException()
		};
}
