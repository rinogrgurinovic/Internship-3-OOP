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

            int InputInt(bool InputVerification)
            {
                int input;
                do
                {
                    Console.Clear();
                    Menu();
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

            var myDictionary = new Dictionary<Contact, List<Call>>()
            {
                {new Contact("Marin Marinovic", "0998765432", "blokiran"), new List<Call>() {new Call(new DateTime(2022, 12, 03, 09, 43, 55), "propusten"),
                                                                                          new Call(new DateTime(2023, 03, 15, 14, 32, 18), "propusten") } },
                {new Contact("Ivan Ivanovic", "0912345678", "favorit"), new List<Call>() {new Call(new DateTime(2023, 11, 20, 18, 13, 46), "zavrsen"),
                                                                                         new Call(new DateTime(2023, 11, 25, 12, 30, 23), "u tijeku")} },
                {new Contact("Lara Laric", "0985342187", "normalan"), new List<Call>() {new Call(new DateTime(2021, 05, 19, 23, 54, 03), "zavrsen"),
                                                                                    new Call(new DateTime(2021, 07, 01, 22, 03, 19), "propusten")} },
            };

            do
            {
                int mainInput = InputInt(inputVerification);

                Console.Clear();

                switch (mainInput)
                {
                    case 1:
                        foreach (var item in myDictionary)
                            Console.WriteLine($"{item.Key.NameAndSurname} - {item.Key.PhoneNumber} - {item.Key.Preference}");
                        Console.WriteLine("Pritisnite tipku za povratak na izbornik");
                        Console.ReadKey();
                        break;
                    case 2:
                        bool flag = true;

                        Console.WriteLine("Unesite ime i prezime novog kontakta:");
                        string nameAndSurname = InputString();

                        string phoneNumber;
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
                                    flag = false;
                        } while (!flag);

                        string preference;
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

                        var contact = new Contact(nameAndSurname, phoneNumber, preference);
                        myDictionary.Add(contact, new List<Call>());
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
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
