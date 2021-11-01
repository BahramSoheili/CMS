using Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Events
{
    public interface ISendEvent<T> where T : IAggregate
    {
        Task Send(T aggregate, CancellationToken cancellationToken);
    }
}
