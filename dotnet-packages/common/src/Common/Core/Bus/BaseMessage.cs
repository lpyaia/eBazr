using System;

namespace Common.Core.Bus
{
    public abstract class BaseMessage : IBusMessage
    {
        protected BaseMessage()
        {
            EventId = Guid.NewGuid();
            MessageId = EventId.ToString();
            ContentName = GetType().Name;
        }

        public Guid EventId { get; }
        public virtual string ContentName { get; }
        public string MessageId { get; }
    }
}