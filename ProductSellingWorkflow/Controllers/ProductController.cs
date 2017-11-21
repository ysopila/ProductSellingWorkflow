using ProductSellingWorkflow.Models;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Events;
using System.Web.Mvc;
using System.Linq;

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

				var result = _service.Create(@event);

				PopulateModelState(result);

				if (ModelState.IsValid)
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

				var result = _service.Update(@event);

				PopulateModelState(result);

				if (ModelState.IsValid)
					return RedirectToAction("Index", "Product");
			}

			return View(model);
		}

		private void PopulateModelState(EventResult result)
		{
			if (result.Errors?.Any() == true)
				foreach (var e in result.Errors)
					ModelState.AddModelError(string.Empty, e.Message);
		}
	}
}