namespace SchoolSystem.Data.Helpers
{
    public class EnrolledStudentInfoHelperDto
    {
        public string Name { get; set; }   
        public string Subjects { get; set; }    
        public int? Class { get; set; }
        public string english_teacher { get; set; }
        public string math_teacher { get; set; }
        public string physics_teacher { get; set; }               
    }
}