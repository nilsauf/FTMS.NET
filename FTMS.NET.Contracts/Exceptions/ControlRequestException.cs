namespace FTMS.NET.Exceptions;

using FTMS.NET.Control;
using System;

public class ControlRequestException(EControlOpCode opCode, Exception? innerException = null)
	: Exception(ExceptionMessage, innerException)
{
	private const string ExceptionMessage = "Control Request could not be executed.";

	public EControlOpCode OpCode { get; } = opCode;
	public EControlResultCode? ResultCode { get; }

	public ControlRequestException(EControlOpCode opCode, EControlResultCode resultCode)
		: this(opCode)
	{
		this.ResultCode = resultCode;
	}
}
