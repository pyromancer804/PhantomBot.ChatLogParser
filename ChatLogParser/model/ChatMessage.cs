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
        public bool IsFollow { get; set; }
        public bool IsSub { get; set; }
        public bool IsCheer { get; set; }
        public int? CheerAmount { get; set; }
        public string AffectedUserName { get; set; }

        public ChatMessage(string _logLine)
        {
            TimeStamp = ParseDate(_logLine);
            UserName = ParseUserName(_logLine);
            Message = ParseMessage(_logLine);

            // Default the affected user to the current user
            AffectedUserName = UserName;

            // Check if the message is from the bot and parse the extra info if it is
            if (UserName == ConfigValues.BOT_NAME)
            {
                // TODO : find a way to make these dynamic as not everyone's bot messages are the same
                SetFollow();
                SetSub();
                SetCheer();
            }
        }

        public string ToCSV()
        {
            return $@"{TimeStamp:G},{UserName},""{Message.Replace(@"""",@"""""")}"",{IsFollow},{IsSub},{IsCheer},{CheerAmount},{AffectedUserName}";
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

        /// <summary>
        /// Parse the username
        /// </summary>
        /// <param name="_logLine"></param>
        /// <returns></returns>
        private string ParseUserName(string _logLine)
        {
            return _logLine.Split(']')[1].Split(':')[0].Trim();
        }

        /// <summary>
        /// Parse the message
        /// </summary>
        /// <param name="_logLine"></param>
        /// <returns></returns>
        private string ParseMessage(string _logLine)
        {
            string userAndMessage =_logLine.Split(']')[1];
            return userAndMessage.Substring(userAndMessage.IndexOf(':')+1); // Only go from the first :
        }

        private void SetFollow()
        {
            // Find the word followed in the message
            if (Message.Contains("followed"))
            {
                IsFollow = true;
                AffectedUserName = Message.Split(' ')[1].Trim(); // First word of the message is the user affected
            }  
        }

        private void SetSub()
        {
            if (Message.Contains("subscribed"))
            {
                IsSub = true;
                AffectedUserName = Message.Split(' ')[1].Trim(); // First word of the message is the user affected
            }
        }

        private void SetCheer()
        {
            if (Message.Contains("cheered"))
            {
                IsCheer = true;

                string[] messageSplit = Message.Split(' ');
                AffectedUserName = messageSplit[1]; // First word of the message is the user affected  
                CheerAmount = int.Parse(messageSplit[4]); // Fourth word is the cheer amount
            }
        }
    }
}
