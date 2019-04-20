using System;
using System.Collections.Generic;
using System.Text;

namespace ChatLogParser.model
{
    public class ChatMessage
    {
        public DateTime TimeStamp { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
