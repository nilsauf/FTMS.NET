namespace FTMS.NET.State;

internal sealed record TrainingState(ETrainingState State, string? Details) : ITrainingState;