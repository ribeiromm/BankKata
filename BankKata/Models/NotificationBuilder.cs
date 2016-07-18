using System.Collections.Generic;

namespace BankKata.Models
{
    public abstract class NotificationBuilder
    {
        protected Message _message;

        protected Message Message
        {
            get { return _message;}
        }

        public abstract void NotificationType();
        public abstract void BodyMessage();
        public abstract void MessageDetails();
    }

    public class LetterBuilder : NotificationBuilder
    {
        public LetterBuilder()
        {
            _message = new Message();
        }
        
        public override void NotificationType()
        {
            _message.NotificationType = Models.NotificationType.Letter;
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
            _message = new Message();
        }
        public override void NotificationType()
        {
            _message.NotificationType = Models.NotificationType.Email;
        }

        public override void BodyMessage()
        {
            _message.Body = "";
        }

        public override void MessageDetails()
        {
            _message.MessageDetails["sbject"] = "";
            _message.MessageDetails["emailAddress"] = "";
        }
    }

    public class SmsBuilder : NotificationBuilder
    {
        public SmsBuilder()
        {
            _message = new Message();
        }
        public override void NotificationType()
        {
            _message.NotificationType = Models.NotificationType.Sms;
        }

        public override void BodyMessage()
        {
            _message.Body = "";
        }

        public override void MessageDetails()
        {
            _message.MessageDetails["mobileNumber"] = "";
        }
    }

    public class Message
    {
        public NotificationType NotificationType { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> MessageDetails { get; set; } 
    }

    public enum NotificationType
    {
        Letter,
        Email,
        Sms
    }
}