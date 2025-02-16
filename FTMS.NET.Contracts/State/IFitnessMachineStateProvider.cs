namespace FTMS.NET.State;
using System;

public interface IFitnessMachineStateProvider : IDisposable
{
	IObservable<IFitnessMachineState> ObserveMachineState();
	IObservable<ITrainingState> ObserveTrainingState();
}
