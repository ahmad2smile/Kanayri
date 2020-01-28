using System;
using System.Collections;
using System.Linq;
using MediatR;

namespace Kanayri.Tests
{
    public class MediatorFactory
    {
        public Mediator GetMediator()
        {
            var serviceFactory = new ServiceFactory(type =>
                typeof(IEnumerable).IsAssignableFrom(type)
                    ? Array.CreateInstance(type.GetGenericArguments().First(), 0)
                    : null);

            return new Mediator(serviceFactory);
        }
    }
}
