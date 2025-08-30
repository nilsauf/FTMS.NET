namespace FTMS.NET.Utils;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Linq;

internal static class UtilExtensions
{
	public static IObservable<T> TakeUntil<T>(this IObservable<T> source, CancellationToken cancellationToken)
		=> Observable.Create<T>(observer =>
		{
			if (cancellationToken.IsCancellationRequested)
			{
				observer.OnCompleted();
				return Disposable.Empty;
			}

			var sub = source.SubscribeSafe(observer);
			return new CompositeDisposable(
				cancellationToken.Register(observer.OnCompleted),
				sub);
		});

	[return: DynamicallyAccessedMembers(SourceReflectorAccessMemerTypes.All)]
	internal static Type EnsureBaseTypeOf<TBaseType>(
		[DynamicallyAccessedMembers(SourceReflectorAccessMemerTypes.All)]
		this Type type)
	{
		Type targetType = typeof(TBaseType);
		if (type.IsAssignableTo(targetType))
			return type;

		throw new InvalidOperationException($"Type '{type.FullName}' does not inherit from '{targetType.FullName}'.");
	}
}
