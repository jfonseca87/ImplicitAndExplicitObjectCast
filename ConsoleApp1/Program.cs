using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string something = "'3974953'";
            string newString = something.Replace("'", "z");

            Person person = new Person
            {
                Id = 1,
                Name = "Pepito",
                LastName = "Perez",
                Email = "pperez@domain.com",
                Age = 58
            };
            PersonDto personDto = (PersonDto)person;

            PersonDto personDto1 = new PersonDto
            {
                Id = 1,
                Name = "Pepito",
                LastName = "Perez",
                Email = "pperez@domain.com",
                CalculatedAge = 78
            };
            Person person1 = (Person)personDto1;
        }
    }

    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Addresses { get; set; }
    }

    class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CalculatedAge { get; set; }
        public IEnumerable<Address> Addresses { get; set; }

        public static explicit operator Person(PersonDto person)
        {
            string addresses = person.Addresses == null ? JsonConvert.SerializeObject("[]") : JsonConvert.SerializeObject(person.Addresses);

            return new Person
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                Email = person.Email,
                Age = person.CalculatedAge,
                Addresses = addresses
            };
        }

        public static explicit operator PersonDto(Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                Email = person.Email,
                CalculatedAge = person.Age,
                Addresses = string.IsNullOrEmpty(person.Addresses) ? null : JsonConvert.DeserializeObject<IEnumerable<Address>>(person.Addresses)
            };
        }
    }

    class Address { }
}
