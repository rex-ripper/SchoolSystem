using System;

namespace SchoolSystem.Data.Helpers
{
    public class AllStudentsInfoHelperDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? ClassOf { get; set; }
        public string IsGraduated { get; set; }
    }
}