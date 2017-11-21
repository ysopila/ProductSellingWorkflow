using System;

namespace ProductSellingWorkflow.Common.Exceptions
{
	public class MapperNotFoundException : Exception
	{
		public MapperNotFoundException() { }
		public MapperNotFoundException(string message) : base(message) { }
	}
}
