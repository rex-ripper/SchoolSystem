using System;
using FluentMigrator;
using SchoolSystem.Migrations.Constants;
using SchoolSystem.Data.Models;

namespace SchoolSystem.Migrations.Migrations
{
    [Migration(1, "teachers")]
    public class _0001_CreateTeacherMigration : Migration
    {
        private string TableName = "teachers";
        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("first_name").AsString().NotNullable()
                .WithColumn("last_name").AsString().NotNullable()
                .WithColumn("address").AsString().Nullable()
                .WithColumn("subject_name").AsString().NotNullable()
                .WithColumn("has_class_in_six").AsBoolean().NotNullable()
                .WithColumn("has_class_in_seven").AsBoolean().NotNullable()
                .WithColumn("has_class_in_eight").AsBoolean().NotNullable()
                .WithColumn("has_class_in_nine").AsBoolean().NotNullable()
                .WithColumn("has_class_in_ten").AsBoolean().NotNullable();

            Load();

        }
        private void Load()
        {
            SaveRecord(TeachersConstants.MathTeacherOf_9_10, "Md Rifatur Rahman", "Mridha", "Math", false, false, false, true, true);
            SaveRecord(TeachersConstants.MathTeacherOf_6To_8, "Mubarrak", "Hossain", "Math", true, true, true, false, false);
            SaveRecord(TeachersConstants.EnglishTeacherOf_6_8, "Ali Murttuza", "Sizan", "English", true, false, true, false, false, "40/2D Tongi, Gazipur");
            SaveRecord(TeachersConstants.EnglishTeacherOf_7_9, "Aonkur", "Mirza", "English", false, true, false, true, false, "20/3F, Dhanmondi 32, Dhaka");
            SaveRecord(TeachersConstants.EnglishTeacherOf_10, "Monjurul", "Hoque", "English", false, false, false, false, true, "23/4D, Uttar kafrulm, Dhaka Cantonment, Dhaka -1260");
            SaveRecord(TeachersConstants.PhysicsTeacherOf_9_10, "Shuvon", "Ovro", "Physics", false, false, false, true, true);
            SaveRecord(TeachersConstants.PhysicsTeacherOf_6, "Annisa", "Karim", "Physics", true, false, false, false, false, "23/2D, Banasree, Dhaka");
            SaveRecord(TeachersConstants.PhysicsTeacherOf_7_8, "Nur-E-Saba", "Tahsin", "Physics", false, true, true, false, false, "#23 # #F, Bashundhara, Dhaka");

        }
        private void SaveRecord(string id, string firstName, string lastName, string subjectName,
             bool hasClassInSix, bool hasClassInSeven, bool hasClassInEight, bool hasClassInNine, bool hasClassInTen, string address = null)
        {
            var teacher = new Teacher
            {
                id = Guid.Parse(id),
                first_name = firstName,
                last_name = lastName,
                address = address,
                subject_name = subjectName,
                has_class_in_six = hasClassInSix,
                has_class_in_seven = hasClassInSeven,
                has_class_in_eight = hasClassInEight,
                has_class_in_nine = hasClassInNine,
                has_class_in_ten = hasClassInTen
            };

            Insert.IntoTable(TableName).Row(teacher);

        }

    }
}