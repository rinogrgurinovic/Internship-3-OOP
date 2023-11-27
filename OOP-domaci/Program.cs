using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OOP_domaci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            bool inputVerification = true;

            void Menu()
            {
                Console.WriteLine("1 - Ispis svih kontakata");
                Console.WriteLine("2 - Dodavanje novih kontakata u imenik");
                Console.WriteLine("3 - Brisanje kontakta iz imenika");
                Console.WriteLine("4 - Editiranje preference kontakta");
                Console.WriteLine("5 - Upravljanje kontaktom");
                Console.WriteLine("6 - Ispis svih poziva");
                Console.WriteLine("7 - Izlaz iz aplikacije");
            }

            void Submenu()
            {
                Console.WriteLine("1 - Ispis svih poziva");
                Console.WriteLine("2 - Kreiranje novog poziva");
                Console.WriteLine("3 - Povratak na glavni izbornik");
            }

            int InputInt(bool InputVerification, string menu)
            {
                int input;
                do
                {
                    Console.Clear();
                    if (menu == "Menu")
                        Menu();
                    else
                        Submenu();
                    if (!InputVerification)
                        Console.WriteLine("Krivi unos, pokusajte ponovno:");
                    InputVerification = int.TryParse(Console.ReadLine(), out input);
                } while(!InputVerification);
                inputVerification = InputVerification;
                return input;
            }

            string InputString()
            {
                string input;
                do
                {
                    input = Console.ReadLine();
                } while (input.Trim() == "");
                return input;
            }

            Contact contact1 = new Contact("Marin Marinovic", "0998765432", "blokiran");
            Contact contact2 = new Contact("Ivan Ivanovic", "0912345678", "favorit");
            Contact contact3 = new Contact("Lara Laric", "0985342187", "normalan");

            Call call1 = new Call(new DateTime(2023, 11, 20, 18, 13, 46), "zavrsen");
            Call call2 = new Call(new DateTime(2023, 11, 25, 12, 30, 23), "propusten");
            Call call3 = new Call(new DateTime(2021, 05, 19, 23, 54, 03), "zavrsen");
            Call call4 = new Call(new DateTime(2021, 07, 01, 22, 03, 19), "propusten");

            var phoneBook = new Dictionary<Contact, List<Call>>()
            {
                {contact1, new List<Call>()},
                {contact2, new List<Call>() {call1, call2 } },
                {contact3, new List<Call>() {call3, call4 } },
            };

            do
            {
                int mainInput = InputInt(inputVerification, "Menu");

                Console.Clear();

                string nameAndSurname = "";
                string phoneNumber = "";
                string preference = "";
                bool flag = true;
                string confirmation;
                switch (mainInput)
                {
                    case 1:
                        foreach (var item in phoneBook)
                            Console.WriteLine($"{item.Key.NameAndSurname} - {item.Key.PhoneNumber} - {item.Key.Preference}");
                        Console.WriteLine("Pritisnite tipku za povratak na izbornik");
                        Console.ReadKey();
                        break;
                    case 2:
                        do
                        {
                            Console.Clear();
                            if (!flag)
                                Console.WriteLine("To je ime vec u imeniku, pokusajte ponovno:");
                            Console.WriteLine("Unesite ime i prezime novog kontakta:");
                            nameAndSurname = InputString();

                            flag = true;
                            foreach (var item in phoneBook)
                                if (nameAndSurname == item.Key.NameAndSurname)
                                {
                                    flag = false;
                                    break;
                                }
                        } while (!flag);

                        do
                        {
                            Console.Clear();
                            if (!flag)
                                Console.WriteLine("Taj je broj vec u imeniku, pokusajte ponovno:");
                            Console.WriteLine("Unesite broj novog kontakta:");
                            phoneNumber = InputString();

                            flag = true;
                            foreach (var item in phoneBook)
                                if (phoneNumber == item.Key.PhoneNumber)
                                {
                                    flag = false;
                                    break;
                                }
                        } while (!flag);

                        do
                        {
                            Console.Clear();
                            if (!flag)
                                Console.WriteLine("Nepostojeca preferenca, pokusajte ponovno:");
                            Console.WriteLine("Unesite preferencu novog kontakta (blokiran, favorit ili normalan):");
                            preference = InputString();

                            flag = true;
                            if (preference != "blokiran" && preference != "favorit" && preference != "normalan")
                                flag = false;
                        } while (!flag);

                        Contact newContact = new Contact(nameAndSurname, phoneNumber, preference);
                        phoneBook.Add(newContact, new List<Call>());

                        Console.WriteLine("Uspjesno dodan kontakt, pritisnite tipku za povratak na izbornik");
                        Console.ReadKey();
                        break;
                    case 3:
                        do
                        {
                            Console.Clear();
                            foreach (var item in phoneBook)
                                Console.WriteLine($"{item.Key.NameAndSurname} - {item.Key.PhoneNumber} - {item.Key.Preference}");

                            if (!flag)
                                Console.WriteLine("Taj kontakt ne postoji, pokusajte ponovno:");
                            Console.WriteLine("Upisite broj kontakta kojeg zelite izbrisati:");
                            phoneNumber = InputString();

                            flag = false;
                            foreach (var item in phoneBook)
                                if (phoneNumber == item.Key.PhoneNumber)
                                {
                                    flag = true;
                                    break;
                                }
                        } while (!flag);

                        do
                        {
                            Console.Clear();
                            if (!flag)
                                Console.WriteLine("Krivi unos, pokusajte ponovno:");
                            Console.WriteLine("Jeste li sigurni da zelite izbrisati ovaj kontakt (da/ne):");
                            confirmation = InputString();

                            flag = true;
                            if (confirmation == "da")
                            {
                                Contact contactToDelete = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
                                phoneBook.Remove(contactToDelete);

                                Console.WriteLine("Uspjesno izbrisan kontakt, pritisnite tipku za povratak na izbornik");
                                Console.ReadKey();
                            }
                            else if (confirmation == "ne")
                            {
                                Console.WriteLine("Akcija ponistena, pritisnite tipku za povratak na izbornik");
                                Console.ReadKey();
                                break;
                            }
                            else
                                flag = false;
                        } while (!flag);
                        break;
                    case 4:
                        do
                        {
                            Console.Clear();
                            foreach (var item in phoneBook)
                                Console.WriteLine($"{item.Key.NameAndSurname} - {item.Key.PhoneNumber} - {item.Key.Preference}");

                            if (!flag)
                                Console.WriteLine("Taj kontakt ne postoji, pokusajte ponovno:");
                            Console.WriteLine("Upisite broj kontakta kojemu zelite promijeniti preferencu:");
                            phoneNumber = InputString();

                            flag = false;
                            foreach (var item in phoneBook)
                                if (phoneNumber == item.Key.PhoneNumber)
                                {
                                    flag = true;
                                    break;
                                }
                        } while (!flag);

                        string newPreference;
                        do
                        {
                            Console.Clear();
                            if (!flag)
                                Console.WriteLine("Nepostojeca preferenca, pokusajte ponovno:");
                            Console.WriteLine("Unesite novu preferencu ovog kontakta (blokiran, favorit ili normalan):");
                            newPreference = InputString();

                            flag = true;
                            if (newPreference != "blokiran" && newPreference != "favorit" && newPreference != "normalan")
                                flag = false;
                        } while (!flag);

                        do
                        {
                            Console.Clear();
                            if (!flag)
                                Console.WriteLine("Krivi unos, pokusajte ponovno:");
                            Console.WriteLine("Jeste li sigurni da zelite promijeniti ovaj kontakt (da/ne):");
                            confirmation = InputString();

                            flag = true;
                            if (confirmation == "da")
                            {
                                foreach (var item in phoneBook)
                                    if (phoneNumber == item.Key.PhoneNumber)
                                    {
                                        nameAndSurname = item.Key.NameAndSurname;
                                        preference = item.Key.Preference;
                                        break;
                                    }

                                Contact contactToChange = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
                                contactToChange.Preference = newPreference;

                                Console.WriteLine("Uspjesno promijenjena preferenca kontakta, pritisnite tipku za povratak na izbornik");
                                Console.ReadKey();
                            }
                            else if (confirmation == "ne")
                            {
                                Console.WriteLine("Akcija ponistena, pritisnite tipku za povratak na izbornik");
                                Console.ReadKey();
                                break;
                            }
                            else
                                flag = false;
                        } while (!flag);
                        break;
                    case 5:
                        do
                        {
                            Console.Clear();
                            foreach (var item in phoneBook)
                                Console.WriteLine($"{item.Key.NameAndSurname} - {item.Key.PhoneNumber} - {item.Key.Preference}");

                            if (!flag)
                                Console.WriteLine("Taj kontakt ne postoji, pokusajte ponovno:");
                            Console.WriteLine("Upisite broj kontakta kojim zelite upravljati:");
                            phoneNumber = InputString();

                            flag = false;
                            foreach (var item in phoneBook)
                                if (phoneNumber == item.Key.PhoneNumber)
                                {
                                    flag = true;
                                    break;
                                }
                        } while (!flag);

                        do
                        {
                            int subInput = InputInt(inputVerification, "Submenu");

                            Console.Clear();

                            switch (subInput)
                            {
                                case 1:
                                    foreach (var item in phoneBook)
                                        if (item.Key.PhoneNumber == phoneNumber)
                                        {
                                            if (!item.Value.Any())
                                            {
                                                Console.WriteLine("Ne postoji poziv s odabranim kontaktom");
                                                break;
                                            }
                                            foreach (var item2 in item.Value.OrderByDescending(c => c.CallingTime))
                                                Console.WriteLine($"{item2.CallingTime} - {item2.Status}");
                                            break;
                                        }
                                      
                                    Console.WriteLine("Pritisnite tipku za povratak na izbornik");
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    Contact contactToManage = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
                                    
                                    if (phoneBook[contactToManage].Any(c => c.Status == "u tijeku"))
                                    {
                                        Console.WriteLine("Već postoji poziv u tijeku s tim kontaktom. Pričekajte da završi.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    
                                    int random = new Random().Next(0, 2);

                                    Call newCall = new Call(DateTime.Now, random == 0 ? "u tijeku" : "propusten");
                                    
                                    if (contactToManage.Preference == "blokiran")
                                    {
                                        Console.WriteLine("Nemoguće uspostaviti poziv s blokiranim kontaktom.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    
                                    if (newCall.Status == "u tijeku")
                                    {
                                        Console.WriteLine("Poziv u tijeku");
                                        int duration = new Random().Next(1, 21);
                                        System.Threading.Thread.Sleep(duration * 1000);
                                        Console.WriteLine("Poziv zavrsen");
                                        newCall.Status = "zavrsen";
                                        Console.ReadKey();
                                    }

                                    if (newCall.Status == "propusten")
                                    {
                                        Console.WriteLine("Osoba se nije javila na poziv");
                                        Console.ReadKey();
                                    }
                                    
                                    phoneBook[contactToManage].Add(newCall);

                                    Console.WriteLine("Poziv uspješno dodan.");
                                    break;
                                case 3:
                                    exit = true;
                                    break;
                                default:
                                    inputVerification = false;
                                    break;
                            }
                        } while (!exit);
                        exit = false;
                        break;
                    case 6:
                        foreach (var item in phoneBook)
                        {
                            if (!item.Value.Any())
                                continue;
                            Console.WriteLine(item.Key.NameAndSurname);
                            foreach (var item2 in item.Value)
                                Console.WriteLine($"\t{item2.CallingTime} - {item2.Status}");
                        }
                        Console.WriteLine("Pritisnite tipku za povratak na izbornik");
                        Console.ReadKey();
                        break;
                    case 7:
                        exit = true;
                        break;
                    default:
                        inputVerification = false;
                        break;
                }
            } while (!exit);
        }
    }
}
