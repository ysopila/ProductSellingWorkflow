using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProductSellingWorkflow.Models
{
	public class ProductCreateViewModel
	{
		private static List<string> ExcludedProperties = new List<string> { "Id" };

		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }

		public virtual Dictionary<string, object> ToPropertiesDictionary()
		{
			var result = new Dictionary<string, object>();
			var properties = GetType().GetProperties();

			foreach (var prop in properties.Where(x => !ExcludedProperties.Contains(x.Name)))
			{
				var value = prop.GetValue(this, null);

				if (value != null)
				{
					result.Add(prop.Name, value);
				}
			}

			return result;
		}

	}
}