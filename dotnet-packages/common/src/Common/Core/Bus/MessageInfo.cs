﻿using System;

namespace Common.Core.Bus
{
    public class MessageInfo : IBusInfo
    {
        public string MessageId { get; set; }

        public string RequestId { get; set; }

        public string ContentName { get; set; }

        public string Body { get; set; }

        public int Priority { get; set; }

        public Guid EventId { get; set; }
    }

    public enum ERetryType
    {
        Resend,
        Retry,
        Reject
    }
}
