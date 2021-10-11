using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolSystem.Data.Helpers;
using SchoolSystem.Data.Models;
using SchoolSystem.Services.Repositories.Interfaces;
using SchoolSystem.Services.Services.Interfaces;

namespace SchoolSystem.Services.Services.Services
{
    public class SchoolServices : ISchoolServices
    {
        private readonly ISchoolRepository _repo;

        public SchoolServices(ISchoolRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AllStudentsInfoHelperDto>> GetStudentsInfo()
        {
            var studentsFromDb = (await _repo.GetStudentsFromDb());

            return studentsFromDb.Select(item => new AllStudentsInfoHelperDto
            {
               Id = item.id, Name = $"{item.first_name} {item.last_name}", ClassOf = item.class_of, IsGraduated = item.is_graduated

            }).ToList();
        }

        public async Task<ClassInfoHelperDto> GetClassInfo(int id)
        {
            var classFromDb = await _repo.GetClassFromDb(id);
            var classInfo = new ClassInfoHelperDto
            {
                ClassOf = classFromDb.id,
                EnglishTeacher = $"{classFromDb.ET_first_name} {classFromDb.ET_last_name}",
                MathTeacher = $"{classFromDb.MT_first_name} {classFromDb.MT_last_name}",
                PhysicsTeacher = $"{classFromDb.PT_first_name} {classFromDb.PT_last_name}"

            };

            return classInfo;
        }

        public async Task<List<TopStudentInfoHelperDto>> GetToppersInfo()
        {
            var topperFromDb = await _repo.GetTopperFromDb();
            var listOfToppers = new List<TopStudentInfoHelperDto>();
            foreach (var topper in topperFromDb)
            {
                var topperInfo = new TopStudentInfoHelperDto
                {
                    ClassOf = topper.classOf,
                    First = $"{topper.fFirstName} {topper.fLastName}",
                    Second = $"{topper.sFirstName} {topper.sLastName}",
                    Third = $"{topper.tFirstName} {topper.tLastName}"
                };
                listOfToppers.Add(topperInfo);

            }

            return listOfToppers;
        }


        public async Task<EnrolledStudentInfoHelperDto> GetStudentInfo(string firstName, string lastName)
        {
            var studentFromDb = await _repo.GetStudentFromDb(firstName, lastName);
            var subjects = studentFromDb.sub_3 != null
                ? new HashSet<string> {studentFromDb.sub_1, studentFromDb.sub_2, studentFromDb.sub_3}
                : new HashSet<string> {studentFromDb.sub_1, studentFromDb.sub_2};
            var englishTeacher = subjects.Contains("English")
                ? $"{studentFromDb.ET_first_name} {studentFromDb.ET_last_name}"
                : "N/A";
            var mathTeacher = subjects.Contains("Math")
                ? $"{studentFromDb.MT_first_name} {studentFromDb.MT_last_name}"
                : "N/A";
            var physicsTeacher = subjects.Contains("Physics")
                ? $"{studentFromDb.PT_first_name} {studentFromDb.PT_last_name}"
                : "N/A";

            var studentInfo = new EnrolledStudentInfoHelperDto
            {
                Name = $"{studentFromDb.s_first_name} {studentFromDb.s_last_name}",
                ClassOf = studentFromDb.s_class_of,
                Subjects = string.Join(", ", subjects),
                EnglishTeacher = englishTeacher,
                MathTeacher = mathTeacher,
                PhysicsTeacher = physicsTeacher
            };
            return studentInfo;
        }

        public async Task<AdmitSatusHelperDto> Admission(string firstName, string lastName, string subject01,
            string subject02, string address = null, int? classOf = null, string subject03 = null, Guid? id = null)
        {
            var newId = id ?? Guid.NewGuid(); 
            var isGraduated = classOf == null ? "Yes" : "No";
            var studentData = new Student
            {
                id = newId,
                first_name = firstName,
                last_name = lastName,
                address = address,
                class_of = classOf,
                subject_01 = subject01,
                subject_02 = subject02,
                subject_03 = subject03,
                is_graduated = isGraduated
            };
            await _repo.AddStudentInfoToDb(studentData);
            var admitedStudent = new AdmitSatusHelperDto {Id = newId, Name = $"{firstName} {lastName}", IsAdmitted = "Admitted"};
            return admitedStudent;
        }
    }
}
