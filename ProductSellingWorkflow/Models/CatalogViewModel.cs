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
		public IEnumerable<WatchListView> WatchList { get; set; }

		public bool IsInWatchlist(int productId)
		{
			return WatchList.Any(x => x.ProductId == productId);
		}
	}
}