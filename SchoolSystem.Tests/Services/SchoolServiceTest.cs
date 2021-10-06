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
                new AllStudentsInfoHelperDto {name = "Rahat Shawon Eashan", class_of = 6, is_graduated = "No"},
                new AllStudentsInfoHelperDto {name = "David Baker", class_of = 7, is_graduated = "No"},
                new AllStudentsInfoHelperDto {name = "Jhon Eashan", class_of = null, is_graduated = "Yes"},
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

            Assert.Equal(expectedStudentsInfo[0].name, studentInfo[0].name);
            Assert.Equal(expectedStudentsInfo[0].class_of, studentInfo[0].class_of);
            Assert.Equal(expectedStudentsInfo[0].is_graduated, studentInfo[0].is_graduated);
            Assert.Equal(expectedStudentsInfo[1].name, studentInfo[1].name);
            Assert.Equal(expectedStudentsInfo[1].class_of, studentInfo[1].class_of);
            Assert.Equal(expectedStudentsInfo[1].is_graduated, studentInfo[1].is_graduated);
            Assert.Equal(expectedStudentsInfo[2].name, studentInfo[2].name);
            Assert.Equal(expectedStudentsInfo[2].class_of, studentInfo[2].class_of);
            Assert.Equal(expectedStudentsInfo[2].is_graduated, studentInfo[2].is_graduated);
        }

        // Testing GetStudentInfo(string firstName, string lastName)
        [Fact]
        public async Task GetStudentInfo_ShouldReturn_GivenStudentInfo()
        {
            // Arrange
            var expectedStudentInfo = new EnrolledStudentInfoHelperDto
            {
                Name = "Rahat Shawon Eashan",
                Class = 6,
                Subjects = "English, Math, Physics",
                english_teacher = "SRM Khan",
                math_teacher = "Roddur Roy",
                physics_teacher = "Raju Ahmed"
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
            Assert.Equal(expectedStudentInfo.english_teacher, studentInfo.english_teacher);
            Assert.Equal(expectedStudentInfo.math_teacher, studentInfo.math_teacher);
            Assert.Equal(expectedStudentInfo.physics_teacher, studentInfo.physics_teacher);
        }

        // Testing GetClassInfo(int id)
        [Fact]
        public async Task GetClassInfo_ShouldReturn_GivenClassInfo()
        {
            // Arrange
            var listOfTeachers = new List<TeacherInfoHelperDto>
            {
                new TeacherInfoHelperDto
                    {first_name = "Rakib", last_name = "Rathor", address = "NA", subject = "English"},
                new TeacherInfoHelperDto {first_name = "Kabir", last_name = "Khan", address = "NA", subject = "Math"},
                new TeacherInfoHelperDto {first_name = "Rahat", last_name = "Khan", address = "NA", subject = "Physics"}
            };

            var expectedClassInfo = new ClassInfoHelperDto
            {
                Class = 6,
                Teachers = listOfTeachers
            };

            var classFromDb = new ClassInfoMapperDto
            {
                id = 6,
                ET_first_name = "Rakib", ET_last_name = "Rathor", ET_address = null, ET_subject = "English",
                MT_first_name = "Kabir", MT_last_name = "Khan", MT_address = null, MT_subject = "Math",
                PT_first_name = "Rahat", PT_last_name = "Khan", PT_address = null, PT_subject = "Physics"
            };

            _mockRepo.Setup(s => s.GetClassFromDb(classFromDb.id)).ReturnsAsync(classFromDb);
            // Act
            var classInfo = await _services.GetClassInfo(expectedClassInfo.Class);

            // Assert
            Assert.Equal(expectedClassInfo.Class, classInfo.Class);
            Assert.Equal(expectedClassInfo.Teachers.GetType(), classInfo.Teachers.GetType());
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
                    Class_Of = 6,
                    First = "Raju Rastogi", Second = "Farhan Kabir", Third = "Rex Ripper"
                },
                new TopStudentInfoHelperDto()
                {
                    Class_Of = 7,
                    First = "Farhan Rastogi", Second = "Rex Kabir", Third = "Raju Ripper"
                },
                new TopStudentInfoHelperDto()
                {
                    Class_Of = 8,
                    First = "Farhan Rastogi", Second = "Rex Kabir", Third = "Raju Ripper"
                },
            };

            _mockRepo.Setup(s => s.GetTopperFromDb()).ReturnsAsync(toppersFromDb);

            // Act
            var toppersInfo = (await _services.GetToppersInfo());

            // Assert
            Assert.Equal(expectedToppersInfo[0].Class_Of, toppersInfo[0].Class_Of);
            Assert.Equal(expectedToppersInfo[0].First, toppersInfo[0].First);
            Assert.Equal(expectedToppersInfo[0].Second, toppersInfo[0].Second);
            Assert.Equal(expectedToppersInfo[0].Third, toppersInfo[0].Third);
            Assert.Equal(expectedToppersInfo[1].Class_Of, toppersInfo[1].Class_Of);
            Assert.Equal(expectedToppersInfo[1].First, toppersInfo[1].First);
            Assert.Equal(expectedToppersInfo[1].Second, toppersInfo[1].Second);
            Assert.Equal(expectedToppersInfo[1].Third, toppersInfo[1].Third);
            Assert.Equal(expectedToppersInfo[2].Class_Of, toppersInfo[2].Class_Of);
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
            
            var expectedResult = new AdmitSatusHelperDto{id = newId, name = $"{student.first_name} {student.last_name}", is_admited = true};

            _mockRepo.Setup(s => s.AddStudentInfoToDb(student)).Returns(Task.CompletedTask);

            // Act
            var admission = await _services.Admission(student.first_name, student.last_name, student.subject_01,
                student.subject_02, student.address, student.class_of, student.subject_03, student.id);
            // Assert
            Assert.Equal(expectedResult.id, admission.id);
            
        }
    }
}