using System;

namespace SchoolSystem.Data.Models
{
    public class Person
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }

    }
}