using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Data.Helpers;
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

        [HttpGet("StudentInfo/{firstName}_{lastName}")]
        public async Task<ActionResult<EnrolledStudentInfoHelperDto>> GetStudentData(string firstName, string lastName)
        {
            return await _services.GetStudentInfo(firstName, lastName);
        }

        [HttpGet("Classes/{id}")]
        public async Task<ActionResult<ClassInfoHelperDto>> GetClassData(int id)
        {
            return  await _services.GetClassInfo(id );
        }

             
        [HttpGet("Toppers")]
        public async  Task<ActionResult<IList<TopStudentInfoHelperDto>>> GetTopStudentsData()
        {
            return await _services.GetToppersInfo();
        }
        
        [HttpPut("Admission")]
        public async Task<ActionResult<AdmitSatusHelperDto>> AdmitStudent(string firstName, string lastName, string subject01, string subject02, 
            string subject03 = null, string address = null, int? classOf = null, Guid? id = null)
        {
            return await _services.Admission(firstName,lastName,subject01,subject02,address,classOf,subject03, id);
            
        }

    }
}