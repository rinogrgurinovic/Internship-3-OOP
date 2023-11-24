using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_domaci
{
    public class Contact
    {
        public Contact(string nameAndSurname, string phoneNumber, string preference)
        {
            NameAndSurname = nameAndSurname;
            PhoneNumber = phoneNumber;
            Preference = preference;
        }
        public string NameAndSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Preference { get; set; }
    }
}
