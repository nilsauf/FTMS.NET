namespace FTMS.NET.State;
using System;

public interface IFitnessMachineStateProvider
{
	IObservable<IFitnessMachineState> ObserveMachineState();
	IObservable<ITrainingState> ObserveTrainingState();
}
