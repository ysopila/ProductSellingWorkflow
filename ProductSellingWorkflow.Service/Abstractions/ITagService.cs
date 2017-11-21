using ProductSellingWorkflow.Service.Models;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface ITagService
	{
		IEnumerable<TagDTO> Get(IEnumerable<string> tags);
	}
}