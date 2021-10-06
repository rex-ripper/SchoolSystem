namespace SchoolSystem.Data.Models
{
    public class Student : Person
    {
        public int? class_of { get; set; }        
        public string subject_01 { get; set; }
        public string subject_02 { get; set; }
        public string subject_03 { get; set; }
        public string is_graduated { get; set; }
      
    }
}