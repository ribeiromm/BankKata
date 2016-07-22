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
            MessageBox.Show("Email sent to customer");
        }
    }

    public class SmsNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            MessageBox.Show("SMS sent to customer");
        }
    }

    public class LetterNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            MessageBox.Show("Letter sent to customer");
        }
    }

    public class Notification
    {
        private readonly NotificationStrategy _notificationStrategy;
        private readonly Account _account;

        public Notification(Account account, NotificationStrategy notificationStrategy)
        {
            _account = account;
            _notificationStrategy = notificationStrategy;
        }

        public void SendMessage()
        {
            _notificationStrategy.SendNotification(_account);
        }
    }
}