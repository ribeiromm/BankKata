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
        private readonly NotificationStrategyFactory _notificationStrategyFactory;

        public SendMessage(NotificationStrategyFactory notificationStrategyFactory)
        {
            _notificationStrategyFactory = notificationStrategyFactory;
        }

        public void Send(Account account) 
        {
            _notification = new Notification(account, _notificationStrategyFactory.FinNotification(account));
            _notification.SendMessage();
        }
    }
}