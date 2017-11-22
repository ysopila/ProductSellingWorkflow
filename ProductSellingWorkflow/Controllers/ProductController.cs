using ProductSellingWorkflow.Models;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product;
using System.Linq;
using System.Web.Mvc;

namespace ProductSellingWorkflow.Controllers
{
	public class ProductController : MvcBaseController
	{
		private readonly IProductService _service;
		private readonly ITagService _serviceTags;

		public ProductController(IProductService service, ITagService serviceTags)
		{
			_service = service;
			_serviceTags = serviceTags;
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
				var @event = new CreateProductEvent
				{
					AddedTags = model.TagsList
				};

				if (!string.Equals(model.Name, null)) @event.Name = model.Name;
				if (!string.Equals(model.Description, null)) @event.Description = model.Description;
				if (!string.Equals(model.Color, null)) @event.Color = model.Color;
				if (!string.Equals(model.Size, null)) @event.Size = model.Size;
				if (!decimal.Equals(model.Price, 0)) @event.Price = model.Price;

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

				if (!string.Equals(model.Name, model.Original.Name)) @event.Name = model.Name;
				if (!string.Equals(model.Description, model.Original.Description)) @event.Description = model.Description;
				if (!string.Equals(model.Color, model.Original.Color)) @event.Color = model.Color;
				if (!string.Equals(model.Size, model.Original.Size)) @event.Size = model.Size;
				if (!decimal.Equals(model.Price, model.Original.Price)) @event.Price = model.Price;

				var addedTags = model.TagsList?.Where(x => model.Original.Tags?.Contains(x) != true);
				var removedTags = model.Original.Tags?.Where(x => model.TagsList?.Contains(x) != true);

				if (addedTags?.Any() == true)
					@event.AddedTags = addedTags;
				if (removedTags?.Any() == true)
					@event.RemovedTags = removedTags;

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