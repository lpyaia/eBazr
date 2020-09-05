using System;

namespace Common.Core.Bus
{
    public interface IBusInfo
    {
        Guid EventId { get; }
        string MessageId { get; }
        string ContentName { get; }
    }
}