namespace BankKata.Models
{
    public class NotificationStrategyFactory
    {
        public NotificationStrategy FinNotification(Account account)
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