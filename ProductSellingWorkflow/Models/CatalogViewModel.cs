using ProductSellingWorkflow.Data.Views;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Models
{
	public class CatalogViewModel
	{
		public CatalogViewModel()
		{
			Products = new List<ProductBaseView>();
		}

		public IEnumerable<ProductBaseView> Products { get; set; }
		public UserView CurrentUser { get; set; }

		public bool IsInWatchlist(int productId)
		{
			return CurrentUser.WatchList.Any(x => x.ProductId == productId);
		}
	}
}