using DemoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public static class DataAccess
    {
        private static string personTextFile = "PersonText.txt";

        public static void AddNewPerson(PersonModel person)
        {
            List<PersonModel> people = GetAllPeople();

            // Where this may have been part of this method as a simple people.Add(person) call - refactor out to a separate method
            // This means that the AddNewPerson method has one less responsibility as this is now delegated to its own method
            // It also means that the new method can have error checking in place to prevent attempts to add a person with empty firstname or lastname
            AddPersonToPeopleList(people, person);

            // add a variable to allow easier debugging as we can then capture the contents of the conversion method
            List<string> lines = ConvertModelsToCSV(people);

            File.WriteAllLines(personTextFile, lines);
        }

        public static void AddPersonToPeopleList(List<PersonModel> people, PersonModel person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "FirstName");
            }

            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "LastName");
            }

            people.Add(person);
        }

        // The list of person models can be turned into a separate method to return a lsit of string (in csv format) again by
        // introducing a separate method - the original lsit creation and loop refactored from the add person to people method to this new method
        // again reducing the number of responsibilities in the add person to people method
        public static List<string> ConvertModelsToCSV(List<PersonModel> people)
        {
            List<string> output = new List<string>();

            foreach (PersonModel user in people)
            {
                output.Add($"{ user.FirstName },{ user.LastName }");
            }

            return output;
        }

        public static List<PersonModel> GetAllPeople()
        {
            List<PersonModel> output = new List<PersonModel>();
            string[] content = File.ReadAllLines(personTextFile);

            foreach (string line in content)
            {
                string[] data = line.Split(',');
                output.Add(new PersonModel { FirstName = data[0], LastName = data[1] });
            }

            return output;
        }
    }
}
