using System.Security.Cryptography.X509Certificates;

namespace BankKata.Models
{
    public interface ISendMessage
    {
        void Send(Account account);
    }

    public class SendMessage : ISendMessage
    {
        private Notification _notification;

        public void Send(Account account) 
        {
            _notification = new Notification();
            _notification.Set(account);
            SetNotification(account);
            _notification.SendMessage();
        }

        private void SetNotification(Account account)
        {
            if (account.Balance > 70 && account.Balance <= 80)
            {
                _notification.SetNotification(new EmailNotification());
                return;
            }

            if (account.Balance > 60 && account.Balance <= 70)
            {
                _notification.SetNotification(new SmsNotification());
                return;
            }

            _notification.SetNotification(new LetterNotification());
        }
    }
}