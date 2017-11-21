using ProductSellingWorkflow.Models;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Events;
using System.Web.Mvc;

namespace ProductSellingWorkflow.Controllers
{
	public class ProductController : MvcBaseController
	{
		private readonly IProductService _service;

		public ProductController(IProductService service)
		{
			_service = service;
		}

		public ActionResult Index()
		{
			var data = _service.GetAll();

			return View(data);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View(new ProductCreateViewModel());
		}

		[HttpPost]
		public ActionResult Create(ProductCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var @event = new CreateProductEvent();

				@event.Color = model.Color;

				_service.Create(@event);

				return RedirectToAction("Index", "Product");
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var product = _service.Get(id);

			var model = ProductUpdateViewModel.CreateModel(product);

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(ProductUpdateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var @event = new UpdateProductEvent(model.Id);

				@event.Color = model.Color;

				_service.Update(@event);

				return RedirectToAction("Index", "Product");
			}

			return View(model);
		}
	}
}