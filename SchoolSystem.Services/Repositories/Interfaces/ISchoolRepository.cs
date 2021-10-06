using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolSystem.Data.Mappers;
using SchoolSystem.Data.Models;

namespace SchoolSystem.Services.Repositories.Interfaces
{
    public interface ISchoolRepository
    {
        public Task<List<AllStudentsInfoMapperDto>> GetStudentsFromDb();
        public Task<ClassInfoMapperDto> GetClassFromDb(int id);
        public Task<List<TopStudentsInfoMapperDto>> GetTopperFromDb();
        public Task<StudentInfoMapperDto> GetStudentFromDb(string firstName, string lastName);
        public Task AddStudentInfoToDb(Student studentData);
    }
}