using System;
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
            int InputInt()
            {
                bool inputVerification = true;
                int input;
                do
                {
                    if (!inputVerification)
                        Console.WriteLine("Krivi unos, pokusajte ponovno:");
                    inputVerification = int.TryParse(Console.ReadLine(), out input);
                    if (input > 7 || input < 1)
                        inputVerification = false;
                } while(!inputVerification);
                return input;
            }

            var myDictionary = new Dictionary<Contact, List<Call>>()
            {
                {new Contact("Marin Marinovic", "0998765432", "blocked"), new List<Call>() {new Call(new DateTime(2022, 12, 03, 09, 43, 55), "missed"),
                                                                                          new Call(new DateTime(2023, 03, 15, 14, 32, 18), "missed") } },
                {new Contact("Ivan Ivanovic", "0912345678", "favorite"), new List<Call>() {new Call(new DateTime(2023, 11, 20, 18, 13, 46), "ended"),
                                                                                         new Call(new DateTime(2023, 11, 25, 12, 30, 23), "ongoing")} },
                {new Contact("Lara Laric", "0985342187", "normal"), new List<Call>() {new Call(new DateTime(2021, 05, 19, 23, 54, 03), "ended"),
                                                                                    new Call(new DateTime(2021, 07, 01, 22, 03, 19), "missed")} },
            };

            Console.WriteLine("1 - Ispis svih kontakata");
            Console.WriteLine("2 - Dodavanje novih kontakata u imenik");
            Console.WriteLine("3 - Brisanje kontakta iz imenika");
            Console.WriteLine("4 - Editiranje preference kontakta");
            Console.WriteLine("5 - Upravljanje kontaktom");
            Console.WriteLine("6 - Ispis svih poziva");
            Console.WriteLine("7 - Izlaz iz aplikacije");

            int mainInput = InputInt();

            Console.Clear();

            switch (mainInput)
            {
                case 1:
                    foreach (var item in myDictionary)
                        Console.WriteLine($"{item.Key.NameAndSurname} - {item.Key.PhoneNumber} - {item.Key.Preference}");
                    Console.ReadKey();
                    break;
                case 2:
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
                        Console.WriteLine(item.Key.NameAndSurname);
                        foreach (var item2 in item.Value)
                            Console.WriteLine($"\t{item2.CallingTime} - {item2.Status}");
                    }
                    Console.ReadKey();
                    break;
                case 7:
                    break;
            }
        }
    }
}
