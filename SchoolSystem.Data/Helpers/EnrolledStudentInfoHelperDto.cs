namespace SchoolSystem.Data.Helpers
{
    public class EnrolledStudentInfoHelperDto
    {
        public string Name { get; set; }   
        public string Subjects { get; set; }    
        public int? Class { get; set; }
        public string EnglishTeacher { get; set; }
        public string MathTeacher { get; set; }
        public string PhysicsTeacher { get; set; }               
    }
}