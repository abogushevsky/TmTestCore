using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.AspNet.SignalR;
using SimpleInjector;

namespace TaskManager.Web.Hubs
{
    public class SignalRSimpleInjectorDependencyResolver : DefaultDependencyResolver
    {
        private readonly Container container;

        public SignalRSimpleInjectorDependencyResolver(Container container)
        {
            Contract.Requires(container != null);

            this.container = container;
        }

        public override object GetService(Type serviceType)
        {
            return ((IServiceProvider)container).GetService(serviceType)
                   ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.GetAllInstances(serviceType)
                .Concat(base.GetServices(serviceType));
        }

    }
}