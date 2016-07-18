using System.Collections.Generic;
using Microsoft.Ajax.Utilities;

namespace BankKata.Models
{
    public abstract class NotificationBuilder
    {
        protected Message _message;

        public Message Message
        {
            get { return _message;}
        }

        public abstract void BodyMessage();
        public abstract void MessageDetails();
    }

    public class LetterBuilder : NotificationBuilder
    {
        public LetterBuilder()
        {
            _message = new Message
            {
                NotificationType = NotificationType.Letter,
                MessageDetails = new Dictionary<string, string>()
            };
        }

        public override void BodyMessage()
        {
            _message.Body = "";
        }

        public override void MessageDetails()
        {
            _message.MessageDetails["clientName"] = "";
            _message.MessageDetails["clientAddressLine1"] = "";
            _message.MessageDetails["clientAddressLine2"] = "";
            _message.MessageDetails["clientPostCode"] = "";
        }
    }

    public class EmailBuilder : NotificationBuilder
    {
        public EmailBuilder()
        {
            _message = new Message
            {
                NotificationType = NotificationType.Email,
                MessageDetails = new Dictionary<string, string>()
            };
        }

        public override void BodyMessage()
        {
            _message.Body = "Your balance is now {0} after the latest transaction {1}";
        }

        public override void MessageDetails()
        {
            _message.MessageDetails["subject"] = "You new balance";
            _message.MessageDetails["emailAddress"] = "something@something.com";
        }
    }

    public class SmsBuilder : NotificationBuilder
    {
        public SmsBuilder()
        {
            _message = new Message
            {
                NotificationType = NotificationType.Sms,
                MessageDetails = new Dictionary<string, string>()
            };
        }

        public override void BodyMessage()
        {
            _message.Body = $"Your balance is now {0} after the latest transaction {1}";
        }

        public override void MessageDetails()
        {
            _message.MessageDetails["mobileNumber"] = "";
        }
    }

    public class Message
    {
        public string Body { get; set; }
        public Dictionary<string, string> MessageDetails { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public enum NotificationType
    {
        Letter,
        Email,
        Sms
    }
}