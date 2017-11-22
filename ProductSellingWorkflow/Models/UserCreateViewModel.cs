using System.ComponentModel.DataAnnotations;

namespace ProductSellingWorkflow.Models
{
	public class UserCreateViewModel : UserLoginViewModel
	{
		[Required]
		public string Name { get; set; }
	}
}