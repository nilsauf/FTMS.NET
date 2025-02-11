namespace FTMS.NET.State;
public interface ITrainingState
{
	ETrainingState State { get; }
	string? Details { get; }
}
