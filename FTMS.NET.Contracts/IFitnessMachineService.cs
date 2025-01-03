﻿namespace FTMS.NET;

using FTMS.NET.Control;
using FTMS.NET.Data;
using FTMS.NET.State;

public interface IFitnessMachineService<TFitnessMachineData> : IFitnessMachineService
	where TFitnessMachineData : IFitnessMachineData
{
	new TFitnessMachineData Data { get; }
}

public interface IFitnessMachineService : IFitnessMachineControl, IFitnessMachineStateProvider, IDisposable
{
	IFitnessMachineData Data { get; }

	IFitnessMachineStateProvider State { get; }

	IFitnessMachineControl Control { get; }
}
