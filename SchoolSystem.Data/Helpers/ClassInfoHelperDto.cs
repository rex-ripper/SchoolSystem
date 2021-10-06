using System.Collections.Generic;

namespace SchoolSystem.Data.Helpers
{
    public class ClassInfoHelperDto
    {        
        public int Class { get; set; }
        public List<TeacherInfoHelperDto> Teachers{ get; set; }
    }
}