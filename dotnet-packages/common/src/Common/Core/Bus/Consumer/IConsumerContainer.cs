using System.Collections.Generic;

namespace Common.Core.Bus.Consumer
{
    public interface IConsumerContainer
    {
        IEnumerable<ConsumerResolver> GetAll();
    }
}