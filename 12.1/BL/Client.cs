using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _12._2.BL
{
    public class Client<Account>
        where Account : BL.Account
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronimic { get; set; }
        public string PhoneNumber { get; set; }
        public string SeriesAndNumberOfThePassport { get; set; }
        public DateTime ModificationTime { get; set; }
        public string ModificatedData { get; set; }
        public string TypeOfModification { get; set; }
        public string WhoChangeData { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }

        public Client(string surname, string name, string patronimic, string phoneNumber, string seriesAndNumberOfThePassport, string whoChangeData)
        {
            Surname = surname;
            Name = name;
            Patronimic = patronimic;
            PhoneNumber = PhoneNumberUniformization(phoneNumber);
            SeriesAndNumberOfThePassport = seriesAndNumberOfThePassport;
            ModificationTime = DateTime.Now;
            ModificatedData = "All data";
            TypeOfModification = "Create";
            WhoChangeData = whoChangeData;
            Accounts = new List<Account>();
        }

        public static string PhoneNumberUniformization(string phoneNumber)
        {
            return Regex.Replace(phoneNumber, @"[^\d]", "");
        }
    }
}
