using System.Windows.Forms;

namespace BankKata.Models
{
    public abstract class NotificationStrategy
    {
        public abstract void SendNotification(Account account);
    }

    public class EmailNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            var messageDetails = new MessageDetails();
            var notifycationBuilder = new EmailBuilder();
            messageDetails.Construct(notifycationBuilder);

            var messages = string.Format(notifycationBuilder.Message.Body, account.Balance, account.TrasationType);
            
            MessageBox.Show($"Subject: {notifycationBuilder.Message.MessageDetails["subject"]} to be sent to this email: {notifycationBuilder.Message.MessageDetails["emailAddress"]} and this is the body: {messages}");
        }
    }

    public class SmsNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            var messageDetails = new MessageDetails();
            var notifycationBuilder = new SmsBuilder();
            messageDetails.Construct(notifycationBuilder);

            var messages = string.Format(notifycationBuilder.Message.Body, account.Balance, account.TrasationType);

            MessageBox.Show($"Mobile number: { notifycationBuilder.Message.MessageDetails["mobileNumber"] } sms: {messages}");
        }
    }

    public class LetterNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            var messageDetails = new MessageDetails();
            var notifycationBuilder = new LetterBuilder();
            messageDetails.Construct(notifycationBuilder);

            var messages = string.Format(notifycationBuilder.Message.Body, notifycationBuilder.Message.MessageDetails["clientName"], account.Balance, account.TrasationType);
            MessageBox.Show($"Letter sent : AddressLine1 { notifycationBuilder.Message.MessageDetails["clientAddressLine1"] } addressline2: { notifycationBuilder.Message.MessageDetails["clientAddressLine1"] } " +
                            $"postcode: {notifycationBuilder.Message.MessageDetails["clientPostCode"] } and leeter content: { messages }");
        }
    }

    public class Notification
    {
        private NotificationStrategy _notificationStrategy;
        private Account _account;

        public void SetNotification(NotificationStrategy notificationStrategy)
        {
            _notificationStrategy = notificationStrategy;
        }

        public void Set(Account account)
        {
            _account = account;
        }

        public void SendMessage()
        {
            _notificationStrategy.SendNotification(_account);
        }
    }

    public class MessageDetails
    {
        public void Construct(NotificationBuilder notificationBuilder)
        {
            notificationBuilder.MessageDetails();
            notificationBuilder.BodyMessage();
        }
    }
}