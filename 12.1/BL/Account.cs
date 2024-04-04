using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _12._2.BL
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public Client<Account> Owner { get; set; }
        public double StartBalance { get; set; }
        public double CurrentBalance { get; set; }
        public double InterestRate { get; set; }
        public string Name { get; set; }
        public string History { get; set; }
        public bool IsOpened { get; set; }
        public Account()
        {
            Owner = null;
            StartBalance = 0;
            CurrentBalance = StartBalance;
            InterestRate = 0;
            Name = "null";
            History = "null";
            IsOpened = true;
        }
        public Account(Client<Account> owner, double startBalance = 0, double interestRate = 0)
        {
            Owner = owner;
            StartBalance = startBalance;
            CurrentBalance = startBalance;
            Name = "Простой";
            InterestRate = interestRate;
            History = startBalance.ToString() + " | " + DateTime.Now.ToString();
            IsOpened = true;
        }
        public double TopUp(double moneyToTopUp)
        {
            History += "\r\n+" + moneyToTopUp.ToString() + " | " + DateTime.Now.ToString();
            return CurrentBalance += moneyToTopUp;
        }

        public double Withdraw(double moneyToWithdraw)
        {
            History += "\r\n-" + moneyToWithdraw.ToString() + " | " + DateTime.Now.ToString();
            return CurrentBalance -= moneyToWithdraw;
        }
    }
}
