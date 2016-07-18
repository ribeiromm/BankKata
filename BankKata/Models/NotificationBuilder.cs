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
            _message.Body = "Dear {0} the balance in your account is {1} after the latest transaction {2}";
        }

        public override void MessageDetails()
        {
            _message.MessageDetails["clientName"] = "Sir Bla Blau";
            _message.MessageDetails["clientAddressLine1"] = "The Number";
            _message.MessageDetails["clientAddressLine2"] = "At street";
            _message.MessageDetails["clientPostCode"] = "STH12 12STH";
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
            _message.Body = "Balance {0}, latest transaction {1}";
        }

        public override void MessageDetails()
        {
            _message.MessageDetails["mobileNumber"] = "0123456";
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