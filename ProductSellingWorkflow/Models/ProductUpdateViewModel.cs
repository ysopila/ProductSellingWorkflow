using ProductSellingWorkflow.Service.Models;

namespace ProductSellingWorkflow.Models
{
	public class ProductUpdateViewModel: ProductCreateViewModel
	{
		public int Id { get; set; }

		public static ProductUpdateViewModel CreateModel(ProductDTO product)
		{
			return new ProductUpdateViewModel
			{
				Id = product.Id,
				Name = product.Name,
				Color = product.Color,
				Description = product.Description,
				Size = product.Size
			};
		}
	}
}