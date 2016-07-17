﻿using System.Windows.Forms;

namespace BankKata.Models
{

    public abstract class NotificationStrategy
    {
        public abstract void SendNotification(Account account);
    }

    public class WithDrawNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            MessageBox.Show($"Amount in: {account.Amount} New Balance {account.Balance}");
        }
    }

    public class DepositNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            MessageBox.Show($"Amount in: {account.Amount} New Balance {account.Balance}");
        }
    }

    public class DirectBebitNotification : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            MessageBox.Show($"We've paid this amount {account.Amount} and your new balance is {account.Balance}");
        }
    }

    public class BellowThreshouldLimitSet : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            MessageBox.Show($"You new balance is {account.Balance} after the latest transaction {account.TrasationType}");
        }
    }

    public class TransferNotifiction : NotificationStrategy
    {
        public override void SendNotification(Account account)
        {
            MessageBox.Show($"You just transferred {account.Amount} you balance is {account.Balance}");
        }
    }

    public class Notification
    {
        private readonly NotificationStrategy _notificationStrategy;

        public Notification(NotificationStrategy notificationStrategy)
        {
            _notificationStrategy = notificationStrategy;
        }

        public void SendMessage(Account account)
        {
            _notificationStrategy.SendNotification(account);
        }
    }
}