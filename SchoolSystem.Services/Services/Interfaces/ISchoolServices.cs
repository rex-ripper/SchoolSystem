using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.Data.Helpers;
using SchoolSystem.Data.Models;

namespace SchoolSystem.Services.Services.Interfaces
{
    public interface ISchoolServices
    {
        public Task<List<AllStudentsInfoHelperDto>> GetStudentsInfo();
        public Task<ClassInfoHelperDto> GetClassInfo(int id);
        public Task<List<TopStudentInfoHelperDto>> GetToppersInfo();
        public Task<EnrolledStudentInfoHelperDto> GetStudentInfo(string firstName, string lastName);
        public Task<AdmitSatusHelperDto> Admission(string firstName, string lastName, string subject01, string subject02, string address = null, int? classOf = null, string subject03 = null, Guid? id = null);

    }
}