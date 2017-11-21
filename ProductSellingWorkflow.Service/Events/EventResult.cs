using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Events
{
	public class EventValidationError
	{
		public string Property { get; set; }
		public string Message { get; set; }
	}

	public class EventResult
	{
		public IList<EventValidationError> Errors { get; internal set; } = new List<EventValidationError>();

		public bool Success => !Errors.Any();

		public static EventResult operator +(EventResult e1, EventResult e2)
		{
			return new EventResult
			{
				Errors = e1?.Errors == null
					? e2?.Errors
					: e2?.Errors == null
						? e1?.Errors
						: e1.Errors.Union(e2.Errors).ToList()
			};
		}
	}
}
