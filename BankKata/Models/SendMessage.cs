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
            _notification = new Notification(account, FinNotification(account));
            _notification.SendMessage();
        }

        //todo this cn become a static factory
        private NotificationStrategy FinNotification(Account account)
        {
            if (account.Balance > 70 && account.Balance <= 80)
            {
                return new EmailNotification();
            }

            if (account.Balance > 60 && account.Balance <= 70)
            {
                return new SmsNotification();
            }

            return new LetterNotification();
        }
    }
}