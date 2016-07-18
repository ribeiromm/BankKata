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
            MessageBox.Show($"Email sent: Amount in: {account.Amount}  - New Balance: {account.Balance}");
        }
    }

    public class SmsNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            MessageBox.Show($"SMS sent: Amount in: {account.Amount}  - New Balance: {account.Balance}");
        }
    }

    public class LetterNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            MessageBox.Show($"Letter sent : Latest transaction { account.TrasationType } with amount of: {account.Amount} and your new balance is {account.Balance}");
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
}