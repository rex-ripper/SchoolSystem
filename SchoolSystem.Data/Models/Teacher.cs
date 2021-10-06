namespace SchoolSystem.Data.Models
{
    public class Teacher : Person    
    {
        public string subject_name { get; set; }
        public bool has_class_in_six { get; set; }
        public bool has_class_in_seven { get; set; }
        public bool has_class_in_eight { get; set; }
        public bool has_class_in_nine { get; set; }
        public bool has_class_in_ten { get; set; }        
    }
}