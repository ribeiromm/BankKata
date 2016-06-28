using System;

namespace BankKata.Models
{
    public class Clock
    {
        public virtual DateTime Now()
        {
            return DateTime.Now;
        }
    }
}