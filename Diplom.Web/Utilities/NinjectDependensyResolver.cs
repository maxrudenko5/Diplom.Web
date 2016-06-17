using Diplom.DataAccess;
using Diplom.Interfaces;
using Diplom.Services;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diplom.Web.Utilities
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            //kernel.Bind<IRepository>().To<BookRepository>();
            //IApplicationContext applicationContext = ApplicationContext.Create();
            //kernel.Bind(typeof(IApplicationContext)).ToConstant(applicationContext);
            //kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InRequestScope();
            //kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            //kernel.Bind<IRoleService>().To<RoleService>().InRequestScope();
            //kernel.Bind<IProfileService>().To<ProfileService>().InRequestScope();
            //kernel.Bind<IPostService>().To<PostService>().InRequestScope();
            //kernel.Bind<IHomeService>().To<HomeService>().InRequestScope();
            IApplicationContext context = ApplicationContext.Create();
            kernel.Bind<IAuthenticationService>().To<AuthenticationService>().WithConstructorArgument("context", context);
            kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument("context", context);
            kernel.Bind<IRoleService>().To<RoleService>().WithConstructorArgument("context", context);
            kernel.Bind<IProfileService>().To<ProfileService>().WithConstructorArgument("context", context);
            kernel.Bind<IPostService>().To<PostService>().WithConstructorArgument("context", context);
            kernel.Bind<IHomeService>().To<HomeService>().WithConstructorArgument("context", context);
        }
    }
}