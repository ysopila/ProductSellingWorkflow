using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Implementations
{
	public class TagService : ITagService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISimpleMapper _mapper;

		public TagService(IUnitOfWork unitOfWork, ISimpleMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<TagDTO> Get(IEnumerable<string> tags)
		{
			var tagsArray = tags.ToArray();
			return _unitOfWork.TagRepository.Find(x => tagsArray.Contains(x.Name)).Select(x => new TagDTO { Id = x.Id, Name = x.Name });
		}
	}
}