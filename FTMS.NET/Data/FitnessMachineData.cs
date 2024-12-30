namespace FTMS.NET.Data;
using System;
using System.Linq.Expressions;
using System.Reactive.Linq;

using DynamicData;

using FTMS.NET;
using FTMS.NET.Exceptions;

public abstract class FitnessMachineData<TData> : IFitnessMachineData
	where TData : FitnessMachineData<TData>
{
	private readonly SourceCache<IFitnessMachineValue, Guid> valueCache = new(value => value.Uuid);
	private readonly IDisposable populateSub;

	public abstract EFitnessMachineType Type { get; }

	protected FitnessMachineData(IObservable<byte[]> observeData, IFitnessMachineDataReader dataReader)
	{
		if (dataReader.Type != this.Type)
			throw new DataTypeMismatchException(this.Type, dataReader.Type);

		this.populateSub = this.valueCache.PopulateFrom(observeData.Select(dataReader.Read));
	}

	public IObservable<IChangeSet<IFitnessMachineValue, Guid>> Connect()
		=> this.valueCache.Connect();

	public IObservable<IFitnessMachineValue> Connect(
		Expression<Func<TData, IFitnessMachineValue?>> propertyExpression)
	{
		var propertyName = GetMemberName(propertyExpression.Body);
		var uuid = this.GetValueUuid(propertyName);

		return this.Connect().WatchValue(uuid);
	}

	public IFitnessMachineValue? GetValue(Guid uuid)
		=> this.valueCache.KeyValues.GetValueOrDefault(uuid);

	public void Dispose()
	{
		this.populateSub.Dispose();
		this.valueCache.Dispose();
		GC.SuppressFinalize(this);
	}

	protected abstract Guid GetValueUuid(string name);

	private static string GetMemberName(Expression expression) => expression.NodeType switch
	{
		ExpressionType.MemberAccess => ((MemberExpression)expression).Member.Name,
		ExpressionType.Convert => GetMemberName(((UnaryExpression)expression).Operand),
		_ => throw new NotSupportedException(expression.NodeType.ToString()),
	};
}
