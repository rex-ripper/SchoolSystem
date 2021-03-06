using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SchoolSystem.Data.Helpers;
using SchoolSystem.Data.Mappers;
using SchoolSystem.Data.Models;
using SchoolSystem.Services.Repositories.Interfaces;
using SchoolSystem.Services.Services.Services;
using Xunit;
using Xunit.Sdk;

namespace SchoolSystem.Tests.Services
{
    public class SchoolServiceTest
    {
        private readonly SchoolServices _services;
        private readonly Mock<ISchoolRepository> _mockRepo = new();


        public SchoolServiceTest()
        {
            _services = new SchoolServices(_mockRepo.Object);
        }

        // Testing GetStudentsInfo
        [Fact]
        public async Task GetStudentsInfo_ShouldReturn_AllStudentsInfo()
        {
            // Arrange


            var expectedStudentsInfo = new List<AllStudentsInfoHelperDto>
            {
                new AllStudentsInfoHelperDto {Name = "Rahat Shawon Eashan", ClassOf = 6, IsGraduated = "No"},
                new AllStudentsInfoHelperDto {Name = "David Baker", ClassOf = 7, IsGraduated = "No"},
                new AllStudentsInfoHelperDto {Name = "Jhon Eashan", ClassOf = null, IsGraduated = "Yes"},
            };

            var studentsFromDb = new List<AllStudentsInfoMapperDto>
            {
                new AllStudentsInfoMapperDto
                    {first_name = "Rahat Shawon", last_name = "Eashan", class_of = 6, is_graduated = "No"},
                new AllStudentsInfoMapperDto
                    {first_name = "David", last_name = "Baker", class_of = 7, is_graduated = "No"},
                new AllStudentsInfoMapperDto
                    {first_name = "Jhon", last_name = "Eashan", class_of = null, is_graduated = "Yes"}
            };

            _mockRepo.Setup(s => s.GetStudentsFromDb()).ReturnsAsync(studentsFromDb);

            // Act

            var studentInfo = (await _services.GetStudentsInfo());

            // Assert

            Assert.Equal(expectedStudentsInfo[0].Name, studentInfo[0].Name);
            Assert.Equal(expectedStudentsInfo[0].ClassOf, studentInfo[0].ClassOf);
            Assert.Equal(expectedStudentsInfo[0].IsGraduated, studentInfo[0].IsGraduated);
            Assert.Equal(expectedStudentsInfo[1].Name, studentInfo[1].Name);
            Assert.Equal(expectedStudentsInfo[1].ClassOf, studentInfo[1].ClassOf);
            Assert.Equal(expectedStudentsInfo[1].IsGraduated, studentInfo[1].IsGraduated);
            Assert.Equal(expectedStudentsInfo[2].Name, studentInfo[2].Name);
            Assert.Equal(expectedStudentsInfo[2].ClassOf, studentInfo[2].ClassOf);
            Assert.Equal(expectedStudentsInfo[2].IsGraduated, studentInfo[2].IsGraduated);
        }

        // Testing GetStudentInfo(string firstName, string lastName)
        [Fact]
        public async Task GetStudentInfo_ShouldReturn_GivenStudentInfo()
        {
            // Arrange
            var expectedStudentInfo = new EnrolledStudentInfoHelperDto
            {
                Name = "Rahat Shawon Eashan",
                ClassOf = 6,
                Subjects = "English, Math, Physics",
                EnglishTeacher = "SRM Khan",
                MathTeacher = "Roddur Roy",
                PhysicsTeacher = "Raju Ahmed"
            };
            var studentFromDb = new StudentInfoMapperDto
            {
                s_class_of = 6,
                s_first_name = "Rahat Shawon", s_last_name = "Eashan",
                sub_1 = "English", sub_2 = "Math", sub_3 = "Physics",
                ET_first_name = "SRM", ET_last_name = "Khan",
                MT_first_name = "Roddur", MT_last_name = "Roy",
                PT_first_name = "Raju", PT_last_name = "Ahmed",
            };
            _mockRepo.Setup(s => s.GetStudentFromDb(studentFromDb.s_first_name, studentFromDb.s_last_name))
                .ReturnsAsync(studentFromDb);
            // Act
            var studentInfo = await _services.GetStudentInfo(studentFromDb.s_first_name, studentFromDb.s_last_name);
            // Assert
            Assert.Equal(expectedStudentInfo.Name, studentInfo.Name);
            Assert.Equal(expectedStudentInfo.Subjects, studentInfo.Subjects);
            Assert.Equal(expectedStudentInfo.EnglishTeacher, studentInfo.EnglishTeacher);
            Assert.Equal(expectedStudentInfo.MathTeacher, studentInfo.MathTeacher);
            Assert.Equal(expectedStudentInfo.PhysicsTeacher, studentInfo.PhysicsTeacher);
        }

        // Testing GetClassInfo(int id)
        [Fact]
        public async Task GetClassInfo_ShouldReturn_GivenClassInfo()
        {
            // Arrange
            var englishTeacher = "Rahat Zaman";
            var mathTeacher = "Rex Shawon";
            var physicsTeacher = "Rex Ripper";


            var expectedClassInfo = new ClassInfoHelperDto
            {
                ClassOf = 6, EnglishTeacher = englishTeacher, MathTeacher = mathTeacher, PhysicsTeacher = physicsTeacher
            };

            var classFromDb = new ClassInfoMapperDto
            {
                id = 6,
                ET_first_name = "Rakib", ET_last_name = "Rathor",
                MT_first_name = "Kabir", MT_last_name = "Khan",
                PT_first_name = "Rahat", PT_last_name = "Khan",
            };

            _mockRepo.Setup(s => s.GetClassFromDb(classFromDb.id)).ReturnsAsync(classFromDb);
            // Act
            var classInfo = await _services.GetClassInfo(expectedClassInfo.ClassOf);

            // Assert
            Assert.Equal(expectedClassInfo.ClassOf, classInfo.ClassOf);
        }

        // Testing GetToppersInfo
        [Fact]
        public async Task GetToppersInfo_ShouldReturn_AllTheToppers()
        {
            // Arrange
            var toppersFromDb = new List<TopStudentsInfoMapperDto>()

            {
                new TopStudentsInfoMapperDto
                {
                    classOf = 6,
                    fFirstName = "Raju", fLastName = "Rastogi",
                    sFirstName = "Farhan", sLastName = "Kabir",
                    tFirstName = "Rex", tLastName = "Ripper",
                },
                new TopStudentsInfoMapperDto
                {
                    classOf = 7,
                    fFirstName = "Farhan", fLastName = "Rastogi",
                    sFirstName = "Rex", sLastName = "Kabir",
                    tFirstName = "Raju", tLastName = "Ripper",
                },
                new TopStudentsInfoMapperDto
                {
                    classOf = 8,
                    fFirstName = "Farhan", fLastName = "Rastogi",
                    sFirstName = "Rex", sLastName = "Kabir",
                    tFirstName = "Raju", tLastName = "Ripper",
                },
            };

            var expectedToppersInfo = new List<TopStudentInfoHelperDto>
            {
                new TopStudentInfoHelperDto()
                {
                    ClassOf = 6,
                    First = "Raju Rastogi", Second = "Farhan Kabir", Third = "Rex Ripper"
                },
                new TopStudentInfoHelperDto()
                {
                    ClassOf = 7,
                    First = "Farhan Rastogi", Second = "Rex Kabir", Third = "Raju Ripper"
                },
                new TopStudentInfoHelperDto()
                {
                    ClassOf = 8,
                    First = "Farhan Rastogi", Second = "Rex Kabir", Third = "Raju Ripper"
                },
            };

            _mockRepo.Setup(s => s.GetTopperFromDb()).ReturnsAsync(toppersFromDb);

            // Act
            var toppersInfo = (await _services.GetToppersInfo());

            // Assert
            Assert.Equal(expectedToppersInfo[0].ClassOf, toppersInfo[0].ClassOf);
            Assert.Equal(expectedToppersInfo[0].First, toppersInfo[0].First);
            Assert.Equal(expectedToppersInfo[0].Second, toppersInfo[0].Second);
            Assert.Equal(expectedToppersInfo[0].Third, toppersInfo[0].Third);
            Assert.Equal(expectedToppersInfo[1].ClassOf, toppersInfo[1].ClassOf);
            Assert.Equal(expectedToppersInfo[1].First, toppersInfo[1].First);
            Assert.Equal(expectedToppersInfo[1].Second, toppersInfo[1].Second);
            Assert.Equal(expectedToppersInfo[1].Third, toppersInfo[1].Third);
            Assert.Equal(expectedToppersInfo[2].ClassOf, toppersInfo[2].ClassOf);
            Assert.Equal(expectedToppersInfo[2].First, toppersInfo[2].First);
            Assert.Equal(expectedToppersInfo[2].Second, toppersInfo[2].Second);
            Assert.Equal(expectedToppersInfo[2].Third, toppersInfo[2].Third);
        }

        // Testing Admission()
        [Fact]
        public async Task Admission_ShouldReturn_AdmitStatusInfo()
        {
            // Arrange
            var newId = Guid.NewGuid();
            var student = new Student
            {
                id = newId,
                first_name = "Rahat Shawon",
                last_name = "Eashan",
                class_of = 10,
                address = null,
                subject_01 = "Math",
                subject_02 = "English",
                subject_03 = "Physics",
            };

            var expectedResult = new AdmitSatusHelperDto
                {Id = newId, Name = $"{student.first_name} {student.last_name}", IsAdmitted = "Has been admitted"};

            _mockRepo.Setup(s => s.AddStudentInfoToDb(student)).Returns(Task.CompletedTask);

            // Act
            var admission = await _services.Admission(student.first_name, student.last_name, student.subject_01,
                student.subject_02, student.address, student.class_of, student.subject_03, student.id);
            // Assert
            Assert.Equal(expectedResult.Id, admission.Id);
        }
    }
}