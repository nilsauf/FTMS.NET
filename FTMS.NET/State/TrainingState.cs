namespace FTMS.NET.State;
internal record TrainingState(ETrainingState State, string? Details) : ITrainingState
{
}
