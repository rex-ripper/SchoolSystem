using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Data.Helpers;
using SchoolSystem.Data.ParameterDtos;
using SchoolSystem.Services.Services.Interfaces;

namespace SchoolSystem.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AcademicController : ControllerBase
    {
        private readonly ISchoolServices _services;

        public AcademicController(ISchoolServices services)
        {
            _services = services;
        }

        
        [HttpGet("Students")]
        public async Task<ActionResult<IList<AllStudentsInfoHelperDto>>> GetStudentsData()
        {
            return await _services.GetStudentsInfo();
        }

        [HttpPost("Student")]
        public async Task<ActionResult<EnrolledStudentInfoHelperDto>> GetStudentData(StudentDataParameterDto student)
        {
            return await _services.GetStudentInfo(student.FirstName, student.LastName);
        }

        [HttpPost("Class")]
        public async Task<ActionResult<ClassInfoHelperDto>> GetClassData(ClassDataParameterDto className)
        {
            return  await _services.GetClassInfo(className.ClassOf );
        }

             
        [HttpGet("Toppers")]
        public async  Task<ActionResult<IList<TopStudentInfoHelperDto>>> GetTopStudentsData()
        {
            return await _services.GetToppersInfo();
        }
        
        [HttpPost("Admission")]
        public async Task<ActionResult<AdmitSatusHelperDto>> AdmitStudent(AdmitStudentParameterDto student)
        {
            return await _services.Admission(student.FirstName,student.LastName,student.Subject01,student.Subject02,student.Address,student.ClassOf,student.Subject03, student.Id);
            
        }

    }
}