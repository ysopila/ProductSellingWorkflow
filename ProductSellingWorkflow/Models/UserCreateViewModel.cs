using ProductSellingWorkflow.Common.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductSellingWorkflow.Models
{
	public class UserCreateViewModel : UserLoginViewModel
	{
		public UserCreateViewModel()
		{
			AllRoles = new List<SelectedRole>
			{
				new SelectedRole { Name = Roles.Buyer },
				new SelectedRole { Name = Roles.Seller }
			};
		}

		[Required]
		public string Name { get; set; }

		[Display(Name = "Roles")]
		public List<SelectedRole> AllRoles { get; set; }

		public class SelectedRole
		{
			public bool Selected { get; set; }
			public string Name { get; set; }
		}
	}
}