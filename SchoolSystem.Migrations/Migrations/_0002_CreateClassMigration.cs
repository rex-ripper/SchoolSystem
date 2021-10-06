using System;
using FluentMigrator;
using SchoolSystem.Migrations.Constants;
using SchoolSystem.Data.Models;

namespace SchoolSystem.Migrations.Migrations
{
    [Migration(2, "classes")]
    public class _0002_CreateClassMigration : Migration
    {
        public string TableName = "classes";


        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("id").AsInt16().PrimaryKey()
                .WithColumn("math_teacher_id").AsGuid().ForeignKey("teachers", "id")
                .WithColumn("physics_teacher_id").AsGuid().ForeignKey("teachers", "id")
                .WithColumn("english_teacher_id").AsGuid().ForeignKey("teachers", "id");
            Load();
        }
        private void Load()
        {
            SaveRecord(ClassesConstants.ClassSix, TeachersConstants.MathTeacherOf_6To_8, TeachersConstants.PhysicsTeacherOf_6, TeachersConstants.EnglishTeacherOf_6_8);
            SaveRecord(ClassesConstants.ClassSeven, TeachersConstants.MathTeacherOf_6To_8, TeachersConstants.PhysicsTeacherOf_7_8, TeachersConstants.EnglishTeacherOf_7_9);
            SaveRecord(ClassesConstants.ClassEight, TeachersConstants.MathTeacherOf_6To_8, TeachersConstants.PhysicsTeacherOf_7_8, TeachersConstants.EnglishTeacherOf_6_8);
            SaveRecord(ClassesConstants.ClassNine, TeachersConstants.MathTeacherOf_9_10, TeachersConstants.PhysicsTeacherOf_9_10, TeachersConstants.EnglishTeacherOf_7_9);
            SaveRecord(ClassesConstants.ClassTen, TeachersConstants.MathTeacherOf_9_10, TeachersConstants.PhysicsTeacherOf_9_10, TeachersConstants.EnglishTeacherOf_10);


        }
        private void SaveRecord(int id, string mathTeacherId, string physicsTeacherId, string englishTeacherId)
        {
            var classes = new Classes
            {
                id = id,
                math_teacher_id = Guid.Parse(mathTeacherId),
                physics_teacher_id = Guid.Parse(physicsTeacherId),
                english_teacher_id = Guid.Parse(englishTeacherId),
            };

            Insert.IntoTable(TableName).Row(classes);
        }
    }
}