using System;

namespace SchoolSystem.Data.ParameterDtos
{
    public class AdmitStudentParameterDto
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? ClassOf { get; set; }        
        public string Subject01 { get; set; }
        public string Subject02 { get; set; }
        public string Subject03 { get; set; }
        public string IsGraduated { get; set; }

    }
}