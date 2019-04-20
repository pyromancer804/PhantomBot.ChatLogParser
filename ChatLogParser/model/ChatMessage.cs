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

        public ChatMessage(string _logLine)
        {
            TimeStamp = ParseDate(_logLine);
        }

        /// <summary>
        /// Parse the datetime from the log to a datetime format
        /// </summary>
        /// <param name="_logLine"></param>
        /// <returns></returns>
        private DateTime ParseDate(string _logLine)
        {
            string dateString = _logLine.Split('[')[1].Split(']')[0].Trim();
            return DateTime.ParseExact(dateString,"MM-dd-yyyy @ HH:mm:ss.fff GMT",null);
        }
    }
}
