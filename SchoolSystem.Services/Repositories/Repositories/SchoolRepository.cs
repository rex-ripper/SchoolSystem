using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SchoolSystem.Data.Mappers;
using SchoolSystem.Data.Models;
using SchoolSystem.Services.Repositories.Interfaces;

namespace SchoolSystem.Services.Repositories.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly IDbConnection _config;
        public SchoolRepository(IDbConnection config)
        {
            _config = config;

        }

        public async Task AddStudentInfoToDb(Student studentData)
        {
            var sql = @$"INSERT INTO students (id,first_name, last_name, address, class_of, subject_01, subject_02, subject_03, is_graduated)
                         VALUES(@id, @first_name, @last_name, @address, @class_of, @subject_01, @subject_02, @subject_03, @is_graduated)";
            await _config.ExecuteAsync(sql, studentData);
        }

        public async Task<ClassInfoMapperDto> GetClassFromDb(int id)
        {
            var sql = @$"SELECT C.id, 
                         MT.first_name AS MT_first_name, MT.last_name AS MT_last_name, MT.address AS MT_address, MT.subject_name AS MT_subject,
                         PT.first_name AS PT_first_name, PT.last_name AS PT_last_name, PT.address AS PT_address, PT.subject_name AS PT_subject,
                         ET.first_name AS ET_first_name, ET.last_name AS ET_last_name, ET.address AS ET_address, ET.subject_name AS ET_subject
                         FROM classes AS C
                         INNER JOIN teachers AS MT ON C.math_teacher_id = MT.id
                         INNER JOIN teachers AS PT ON C.physics_teacher_id = PT.id
                         INNER JOIN teachers AS ET ON C.english_teacher_id = ET.id
                         WHERE c.id = {id}";
            return (await _config.QueryAsync<ClassInfoMapperDto>(sql)).ToList()[0];

        }

        public async Task<StudentInfoMapperDto> GetStudentFromDb(string firstName, string lastName)
        {
            var sql = @$"SELECT S.first_name AS s_first_name, S.last_name AS s_last_name, S.Class_of AS s_class_of,
                         S.subject_01 AS sub_1, S.subject_02 AS sub_2, S.subject_03 AS sub_3,
                         MT.first_name AS MT_first_name, MT.last_name AS MT_last_name,
                         PT.first_name AS PT_first_name, PT.last_name AS PT_last_name,
                         ET.first_name AS ET_first_name, ET.last_name AS ET_last_name
                         FROM students AS S
                         INNER JOIN classes AS C ON S.class_of = C.id 
                         INNER JOIN teachers AS MT ON C.math_teacher_id = MT.id
                         INNER JOIN teachers AS PT ON C.physics_teacher_id = PT.id
                         INNER JOIN teachers AS ET ON C.english_teacher_id = ET.id
                         WHERE S.first_name = '{firstName}' AND S.last_name = '{lastName}'";
            return (await _config.QueryAsync<StudentInfoMapperDto>(sql)).ToList()[0];
        }

        public async Task<List<AllStudentsInfoMapperDto>> GetStudentsFromDb()
        {
             var sql = $"SELECT * FROM  students";

            return (await _config.QueryAsync<AllStudentsInfoMapperDto>(sql)).ToList();
        }

        public async Task<List<TopStudentsInfoMapperDto>> GetTopperFromDb()
        {
            var sql = @$"WITH topper AS
                        (
                            SELECT first_name, last_name, class_of, 
                            ROW_NUMBER() 
                            OVER(PARTITION BY class_of 
                                ORDER BY  class_of ASC) AS top
                            FROM students

                        )
                        SELECT FT.class_of AS classOf,
                                FT.first_name AS fFirstName, FT.last_name AS fLastName,
                                ST.first_name AS sFirstName, ST.last_name AS sLastName,
                                TT.first_name AS tFirstName, TT.last_name AS tLastName
                                
                        FROM topper AS FT
                        INNER JOIN topper AS ST ON ST.class_of = FT.class_of
                        INNER JOIN topper AS TT ON TT.class_of = FT.class_of
                        WHERE FT.top = 1 AND ST.top = 2 AND TT.top =3";
            return (await _config.QueryAsync<TopStudentsInfoMapperDto>(sql)).ToList();
        }
    }
}