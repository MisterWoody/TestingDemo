using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DemoLibrary;
using DemoLibrary.Models;

namespace DemoLibrary.Tests
{
    public class DataAccessTests
    {
        [Fact]
        public void AddPersonToPeopleList_ShouldWork()
        {
            // Create a list of personmodel for our test, exercise the method and assert that 
            // 1 - the number of person model entries in the list is one
            // 2 - the person list contains our newly added test person
            PersonModel newPerson = new PersonModel { FirstName = "Tim", LastName = "Corey" };
            List<PersonModel> people = new List<PersonModel>();

            DataAccess.AddPersonToPeopleList(people, newPerson);

            Assert.True(people.Count == 1);
            Assert.Contains<PersonModel>(newPerson, people);
        }

        [Theory]
        [InlineData("Tim", "", "LastName")]
        [InlineData("", "Corey", "FirstName")]
        public void AddPersonToPeopleList_ShouldFail(string firstName, string lastName, string param)
        {

            // Business logic test - if either the firstname or lastname of a person is null or empty
            // then throw an argument exception.
            PersonModel newPerson = new PersonModel { FirstName = firstName, LastName = lastName };
            List<PersonModel> people = new List<PersonModel>();

            Assert.Throws<ArgumentException>(param, () => DataAccess.AddPersonToPeopleList(people, newPerson));
        }

        [Fact]
        public void ConvertModelsToCSV_ShouldPass()
        {
            PersonModel tim = new PersonModel { FirstName = "Tim", LastName = "Corey" };
            PersonModel sue = new PersonModel { FirstName = "Sue", LastName = "Storm" };
            List<PersonModel> people = new List<PersonModel>();
            people.Add(tim);
            people.Add(sue);
            var expected = "Tim,Corey";

            // Action
            var peopleAsStrings = DataAccess.ConvertModelsToCSV(people);

            // Assert
            Assert.True(peopleAsStrings.Count == 2);
            Assert.Contains(expected, peopleAsStrings);
        }
    }
}
