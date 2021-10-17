using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FlatToComplex
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var employee = new Employee()
            {
                Name = "Bob Jones",
                Salary = 2000,
                Department = "Human resources",
                City = "San Diego",
                State = "CA",
                Country = "United States"
            };

            //Make the object with the complex address element from the flat object
            var employeeDTOTest = JsonConvert.DeserializeObject<EmployeeDTO>(JsonConvert.SerializeObject(employee));

        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
        //properties in the Address object
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        //This makes the Employee object transform when serialized
        [JsonProperty("address")]
        private Address address { get; set; }

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            address = new Address { City = this.City, State = this.State, Country = this.Country };
        }
    }

    public class EmployeeDTO
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
        public Address address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
