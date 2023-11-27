using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            Call call1 = new Call(new DateTime(2022, 12, 03, 09, 43, 55), "propusten");
            Call call2 = new Call(new DateTime(2023, 03, 15, 14, 32, 18), "propusten");
            Call call3 = new Call(new DateTime(2023, 11, 20, 18, 13, 46), "zavrsen");
            Call call4 = new Call(new DateTime(2023, 11, 25, 12, 30, 23), "u tijeku");
            Call call5 = new Call(new DateTime(2021, 05, 19, 23, 54, 03), "zavrsen");
            Call call6 = new Call(new DateTime(2021, 07, 01, 22, 03, 19), "propusten");

            var myDictionary = new Dictionary<Contact, List<Call>>()
            {
                {contact1, new List<Call>() {call1, call2 } },
                {contact2, new List<Call>() {call3, call4 } },
                {contact3, new List<Call>() {call5, call6 } },
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
                        foreach (var item in myDictionary)
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
                            foreach (var item in myDictionary)
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
                            foreach (var item in myDictionary)
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
                        myDictionary.Add(newContact, new List<Call>());

                        Console.WriteLine("Uspjesno dodan kontakt, pritisnite tipku za povratak na izbornik");
                        Console.ReadKey();
                        break;
                    case 3:
                        do
                        {
                            Console.Clear();
                            foreach (var item in myDictionary)
                                Console.WriteLine($"{item.Key.NameAndSurname} - {item.Key.PhoneNumber} - {item.Key.Preference}");

                            if (!flag)
                                Console.WriteLine("Taj kontakt ne postoji, pokusajte ponovno:");
                            Console.WriteLine("Upisite broj kontakta kojeg zelite izbrisati:");
                            phoneNumber = InputString();

                            flag = false;
                            foreach (var item in myDictionary)
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
                                Contact contactToDelete = myDictionary.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
                                myDictionary.Remove(contactToDelete);

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
                            foreach (var item in myDictionary)
                                Console.WriteLine($"{item.Key.NameAndSurname} - {item.Key.PhoneNumber} - {item.Key.Preference}");

                            if (!flag)
                                Console.WriteLine("Taj kontakt ne postoji, pokusajte ponovno:");
                            Console.WriteLine("Upisite broj kontakta kojemu zelite promijeniti preferencu:");
                            phoneNumber = InputString();

                            flag = false;
                            foreach (var item in myDictionary)
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
                                foreach (var item in myDictionary)
                                    if (phoneNumber == item.Key.PhoneNumber)
                                    {
                                        nameAndSurname = item.Key.NameAndSurname;
                                        preference = item.Key.Preference;
                                        break;
                                    }

                                Contact contactToChange = myDictionary.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
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
                            foreach (var item in myDictionary)
                                Console.WriteLine($"{item.Key.NameAndSurname} - {item.Key.PhoneNumber} - {item.Key.Preference}");

                            if (!flag)
                                Console.WriteLine("Taj kontakt ne postoji, pokusajte ponovno:");
                            Console.WriteLine("Upisite broj kontakta kojim zelite upravljati:");
                            phoneNumber = InputString();

                            flag = false;
                            foreach (var item in myDictionary)
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
                                    foreach (var item in myDictionary)
                                        if (item.Key.PhoneNumber == phoneNumber)
                                        {
                                            foreach (var item2 in item.Value.OrderByDescending(c => c.CallingTime))
                                                Console.WriteLine($"{item2.CallingTime} - {item2.Status}");
                                            break;
                                        }
                                      
                                    Console.WriteLine("Pritisnite tipku za povratak na izbornik");
                                    Console.ReadKey();
                                    break;
                                case 2:

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
                        foreach (var item in myDictionary)
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
