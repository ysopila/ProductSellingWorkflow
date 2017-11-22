using ProductSellingWorkflow.Models;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Events;
using System.Collections.Generic;
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
				var tags = model.TagsList?.Any() == true ? _serviceTags.Get(model.TagsList).Select(x => new ProductTag { Id = x.Id, Value = x.Name }) : new List<ProductTag>();
				// Adding new tags
				tags = tags.Union(model.TagsList.Where(x => !tags.Any(t => t.Value == x)).Select(x => new ProductTag { Value = x })).ToList();

				var @event = new CreateProductEvent
				{
					AddedTags = tags
				};

				if (!string.Equals(model.Name, null)) @event.Name = model.Name;
				if (!string.Equals(model.Description, null)) @event.Description = model.Description;
				if (!string.Equals(model.Color, null)) @event.Color = model.Color;
				if (!string.Equals(model.Size, null)) @event.Size = model.Size;

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

				var addedTags = model.TagsList?.Where(x => model.Original.Tags?.Contains(x) != true);
				var removedTags = model.Original.Tags?.Where(x => model.TagsList?.Contains(x) != true);

				var tags = addedTags?.Any() == true || removedTags?.Any() == true
					? _serviceTags.Get((addedTags ?? (new string[0])).Union(removedTags ?? new string[0])).Select(x => new ProductTag { Id = x.Id, Value = x.Name })
					: null;

				if (addedTags?.Any() == true)
					@event.AddedTags = addedTags.Select(x => tags.FirstOrDefault(t => t.Value == x) ?? new ProductTag { Value = x });
				if (removedTags?.Any() == true)
					@event.RemovedTags = removedTags.Select(x => tags.FirstOrDefault(t => t.Value == x)).Where(x => x != null);

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