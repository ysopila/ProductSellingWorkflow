using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.Repository.Models;
using ProductSellingWorkflow.Service.Models;

namespace ProductSellingWorkflow.Service.Mappings
{
	public class Mapper : SimpleMapper, ISimpleMapper
	{
		public Mapper()
		{
			AddMapper<ProductModel, ProductDTO>(Convert);
		}

		private ProductDTO Convert(ProductModel x)
		{
			return x == null ? null : new ProductDTO
			{
				Id = x.Id,
				Color = x.Color,
				CreatedAt = x.CreatedAt,
				Description = x.Description,
				ModifiedAt = x.ModifiedAt,
				Tags = x.Tags,
				Name = x.Name,
				Size = x.Size,
				State = x.State
			};
		}
	}
}
