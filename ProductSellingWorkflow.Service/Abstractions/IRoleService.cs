using ProductSellingWorkflow.Data.Views;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface IRoleService
	{
		IEnumerable<RoleView> GetRoles();
	}
}
