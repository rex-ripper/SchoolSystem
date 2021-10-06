using System;

namespace SchoolSystem.Data.Models
{
    public class Classes
    {
        public int id{ get; set; }
        public Guid math_teacher_id { get; set; }
        public Guid physics_teacher_id { get; set; }
        public Guid english_teacher_id { get; set; }
             
        
        
    }
}