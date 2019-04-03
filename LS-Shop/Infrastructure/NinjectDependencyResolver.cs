using LS_Shop.Data_Access_Layer;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LS_Shop.Infrastructure
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

        //TUTAJ DODAJEMY ZALEŻNOŚCI
        private void AddBindings()
        {
            kernel.Bind<IDbContext>().To<DbContext>();
            kernel.Bind<ISessionManager>().To<SessionManager>();
        }
    }
}