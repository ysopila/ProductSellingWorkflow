using ProductSellingWorkflow.Data.Views;

namespace ProductSellingWorkflow.Models
{
	public class ProductUpdateViewModel : ProductCreateViewModel
	{
		public ProductView Original { get; set; }
		public int Id { get; set; }

		public static ProductUpdateViewModel CreateModel(ProductView product)
		{
			return new ProductUpdateViewModel
			{
				Id = product.Id,

				Name = product.Name,
				Color = product.Color,
				Description = product.Description,
				Size = product.Size,
				Tags = string.Join(", ", product.Tags),
				Original = product
			};
		}
	}
}