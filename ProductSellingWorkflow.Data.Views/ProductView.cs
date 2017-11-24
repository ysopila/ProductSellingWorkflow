using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.Data.Views
{
	public class ProductView : ProductBaseView
	{
		public ProductState State { get; set; }
		public UserView CreatedBy { get; set; }
	}
}