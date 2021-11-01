using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Events
{
    public interface IApplyEvent<T> where T : IExternalEvent
    {
        public void ApplyEvent(T @event);
    }
}
