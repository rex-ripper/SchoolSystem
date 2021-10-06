using System;

namespace SchoolSystem.Data.Mappers
{
    public class AllStudentsInfoMapperDto
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public int? class_of { get; set; }        
        public string subject_01 { get; set; }
        public string subject_02 { get; set; }
        public string subject_03 { get; set; }
        public string is_graduated { get; set; }
    }
}