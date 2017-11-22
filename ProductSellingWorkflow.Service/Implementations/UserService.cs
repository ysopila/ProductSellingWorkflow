using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;

namespace ProductSellingWorkflow.Service.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISimpleMapper _mapper;

		public UserService(IUnitOfWork unitOfWork, ISimpleMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public bool Create(string name, string email, string password, out UserView user)
		{
			user = _unitOfWork.UserRepository.GetOne(email);

			if (user == null)
			{
				var entity = new User
				{
					Name = name,
					Email = email,
					Password = password
				};

				_unitOfWork.UserRepository.Insert(entity);
				_unitOfWork.Save();

				user = _unitOfWork.UserRepository.GetOne(email);
				return true;
			}

			return false;
		}

		public UserView GetByEmail(string email)
		{
			return _unitOfWork.UserRepository.GetOne(email);
		}

		public bool Validate(string email, string password, out UserView user)
		{
			user = _unitOfWork.UserRepository.GetOne(email);

			if (user == null || !string.Equals(user.Password, password))
				return false;

			return true;
		}
	}
}
