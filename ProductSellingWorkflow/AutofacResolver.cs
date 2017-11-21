using Autofac;
using Autofac.Integration.Mvc;
using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.Controllers;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Repository.Implementations;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Implementations;
using ProductSellingWorkflow.Service.Mappings;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace ProductSellingWorkflow
{
	public class AutofacResolver : IServiceProvider
	{
		private readonly IContainer _container;
		public IContainer Container { get { return _container; } }

		public AutofacResolver()
		{
			_container = Bootstrap();
		}

		public IContainer Bootstrap()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
			builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();

			builder.RegisterType<Mapper>().As<ISimpleMapper>().InstancePerLifetimeScope();

			var mvcControllersAssy = Assembly.GetAssembly(typeof(MvcBaseController));
			builder.RegisterControllers(mvcControllersAssy).InstancePerLifetimeScope();

			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

			return container;
		}

		public object GetService(Type serviceType)
		{
			return _container.Resolve(serviceType);
		}

		public T GetService<T>()
		{
			return DependencyResolver.Current.GetService<T>();
		}
	}
}