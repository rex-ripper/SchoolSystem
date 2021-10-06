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
                name = $"{item.first_name} {item.last_name}", class_of = item.class_of, is_graduated = item.is_graduated

            }).ToList();
        }

        public async Task<ClassInfoHelperDto> GetClassInfo(int id)
        {
            var classFromDb = await _repo.GetClassFromDb(id);
            var englishTeacher = new TeacherInfoHelperDto
            {
                first_name = classFromDb.ET_first_name,
                last_name = classFromDb.ET_last_name,
                address = classFromDb.ET_address ?? "NA",
                subject = classFromDb.ET_subject

            };
            var mathTeacher = new TeacherInfoHelperDto
            {
                first_name = classFromDb.MT_first_name,
                last_name = classFromDb.MT_last_name,
                address = classFromDb.MT_address ?? "NA",
                subject = classFromDb.MT_subject

            };
            var physicsTeacher = new TeacherInfoHelperDto
            {
                first_name = classFromDb.PT_first_name,
                last_name = classFromDb.PT_last_name,
                address = classFromDb.PT_address ?? "NA",
                subject = classFromDb.PT_subject

            };
            var classInfo = new ClassInfoHelperDto
            {
                Class = classFromDb.id,
                Teachers = new List<TeacherInfoHelperDto> {englishTeacher, mathTeacher, physicsTeacher}
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
                    Class_Of = topper.classOf,
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
                Class = studentFromDb.s_class_of,
                Subjects = string.Join(", ", subjects),
                english_teacher = englishTeacher,
                math_teacher = mathTeacher,
                physics_teacher = physicsTeacher
            };
            return studentInfo;
        }

        public async Task<AdmitSatusHelperDto> Admission(string firstName, string lastName, string subject01,
            string subject02,
            string address = null, int? classOf = null, string subject03 = null)
        {
            var newId = Guid.NewGuid(); 
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
            await _repo.AddStudentInfoToDb(studentData).ConfigureAwait(false);
            return new AdmitSatusHelperDto {id = newId, name = $"{firstName} {lastName}", is_admited = true};
        }
    }
}
