using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SchoolSystem.Data.Helpers;
using SchoolSystem.Data.Mappers;
using SchoolSystem.Services.Repositories.Interfaces;
using SchoolSystem.Services.Services.Services;
using Xunit;

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

            var StudentsFromDb = new List<AllStudentsInfoMapperDto>
            {
                new AllStudentsInfoMapperDto
                    {first_name = "Rahat Shawon", last_name = "Eashan", class_of = 6, is_graduated = "No"},
                new AllStudentsInfoMapperDto
                    {first_name = "David", last_name = "Baker", class_of = 7, is_graduated = "No"},
                new AllStudentsInfoMapperDto
                    {first_name = "Jhon", last_name = "Eashan", class_of = null, is_graduated = "Yes"}
            };

            _mockRepo.Setup(s => s.GetStudentsFromDb()).ReturnsAsync(StudentsFromDb);

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
        public async Task GetStudentInfo_ShouldReturn_GivenStudent()
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
    }
}