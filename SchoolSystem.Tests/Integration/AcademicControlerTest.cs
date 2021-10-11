using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SchoolSystem.API.Controllers;
using SchoolSystem.Data.Helpers;
using SchoolSystem.Data.Models;
using SchoolSystem.Data.ParameterDtos;
using SchoolSystem.Services.Services.Interfaces;
using Xunit;

namespace SchoolSystem.Tests.Integration
{
    public class AcademicControlerTest
    {
        private readonly AcademicController _academicController;
        private readonly Mock<ISchoolServices> _mockServices = new Mock<ISchoolServices>();


        public AcademicControlerTest()
        {
            _academicController = new AcademicController(_mockServices.Object);
        }

        // TestSessionInfo GetStudentsData
        [Fact]
        public async Task GetStudentsData_ShouldReturn_AllStudentsData()
        {
            // Arrange
            var studentsInfo = new List<AllStudentsInfoHelperDto>
            {
                new AllStudentsInfoHelperDto {Name = "Rahat Shawon Eashan", ClassOf = 6, IsGraduated = "No"},
                new AllStudentsInfoHelperDto {Name = "David Baker", ClassOf = 7, IsGraduated = "No"},
                new AllStudentsInfoHelperDto {Name = "Jhon Eashan", ClassOf = null, IsGraduated = "Yes"}
            };

            _mockServices.Setup(s => s.GetStudentsInfo()).ReturnsAsync(studentsInfo);

            // Act

            var getStudentsData = await _academicController.GetStudentsData();

            // Assert

            Assert.Equal(studentsInfo, getStudentsData.Value);
        }

        // Testing GetStudentData(string firstName, string lastName)
        [Fact]
        public async Task GetStudentData_ShouldReturn_GivenStudentData()
        {
            //Arrange

            var firstName = "Rahat";
            var lastName = "Shawon";
            var studentParam = new StudentDataParameterDto {FirstName = firstName, LastName = lastName};
            var student = new EnrolledStudentInfoHelperDto
            {
                Name = $"{firstName} {lastName}",
                EnglishTeacher = "MockEnglishTeacher",
                MathTeacher = "MockMathTeacher",
                PhysicsTeacher = null,
                ClassOf = 6,
                Subjects = "Math,English"
            };

            _mockServices.Setup(x => x.GetStudentInfo(firstName, lastName)).ReturnsAsync(student);

            // ACT

            var getStudentData = await _academicController.GetStudentData(studentParam);

            // Assert

            Assert.Equal(student, getStudentData.Value);
        }

        // Testing GetClassData
        [Fact]
        public async Task GetClassData_ShouldReturn_GivenClassData()
        {
            // Arrange
            var englishTeacher = "Rahat Zaman";
            var mathTeacher = "Rex Shawon";
            var physicsTeacher = "Rex Ripper";


            var classInfo = new ClassInfoHelperDto
            {
                ClassOf = 6, EnglishTeacher = englishTeacher, MathTeacher = mathTeacher, PhysicsTeacher = physicsTeacher
            };
            var classDataParameter = new ClassDataParameterDto {ClassOf = classInfo.ClassOf};

            _mockServices.Setup(s => s.GetClassInfo(classInfo.ClassOf)).ReturnsAsync(classInfo);

            // Act

            var result = await _academicController.GetClassData(classDataParameter);

            // Assert

            Assert.Equal(classInfo, result.Value);
        }

        //Testing GetTopStudentsData
        [Fact]
        public async Task GetTopStudentsData_ShouldRetrun_AllToppersData()
        {
            // Arrange
            var toppersInfo = new List<TopStudentInfoHelperDto>
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
            _mockServices.Setup(s => s.GetToppersInfo()).ReturnsAsync(toppersInfo);
            // Act
            var toppersData = await _academicController.GetTopStudentsData();
            // Asser
            Assert.Equal(toppersInfo, toppersData.Value);
        }

        // Testing AdmitStudent()
        [Fact]
        public async Task AdmitStudent_ShouldRetun_AdmitedStudent()
        {
            // Arrange
            var newId = Guid.NewGuid();
            var studentBio = new Student()
            {
                id = newId,
                first_name = "Rayhan",
                last_name = "Khan",
                class_of = 6,
                subject_01 = "Math",
                subject_02 = "English",
                subject_03 = "Physics"
            };
            var admitStudentParameterDto = new AdmitStudentParameterDto
            {
                Id = studentBio.id,
                FirstName = studentBio.first_name,
                LastName = studentBio.last_name,
                ClassOf = studentBio.class_of,
                Subject01 = studentBio.subject_01,
                Subject02 = studentBio.subject_02,
                Subject03 = studentBio.subject_03
            };

            var admitedStudent = new AdmitSatusHelperDto
            {
                Id = newId,
                Name = $"{studentBio.first_name} {studentBio.last_name}",
                IsAdmitted = "Yes"
            };

            _mockServices.Setup(s => s
                .Admission(
                    studentBio.first_name,
                    studentBio.last_name,
                    studentBio.subject_01,
                    studentBio.subject_02,
                    studentBio.address,
                    studentBio.class_of,
                    studentBio.subject_03,
                    studentBio.id
                )).ReturnsAsync(admitedStudent);
            // Act
            var admitedStudentData = await _academicController.AdmitStudent(admitStudentParameterDto);

            // Assert
            Assert.Equal(admitedStudent.Id, admitedStudentData.Value.Id);
        }
    }
}