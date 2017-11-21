using ProductSellingWorkflow.DataModel;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Events
{
	public abstract class ProductEvent
	{
		internal virtual EventResult Apply(Product product, bool createLog = true)
		{
			var result = new EventResult();
			result.Errors = Validate(product);
			return result;
		}

		internal virtual IList<EventValidationError> Validate(Product product)
		{
			return new List<EventValidationError>();
		}
	}

}
