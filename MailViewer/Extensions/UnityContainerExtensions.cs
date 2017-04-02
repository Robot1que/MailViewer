using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

namespace Robot1que.MailViewer.Extensions
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer RegisterTypeAsSingleton<TFrom, TTo>(
            this IUnityContainer container) where TTo: TFrom
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }
    }
}
