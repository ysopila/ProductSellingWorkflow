using System;

namespace ProductSellingWorkflow.Common.Core
{
	public interface ISimpleMapper
	{
		V Map<T, V>(T aObject);
		Func<T, V> GetMapper<T, V>();
	}
}
