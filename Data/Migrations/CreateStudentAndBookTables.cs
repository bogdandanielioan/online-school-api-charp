using FluentMigrator;

namespace online_school_api.Migrations
{
    [Migration(202310101000)] 
    public class CreateStudentAndBookTables : Migration
    {
        public override void Up()
        {
            Create.Table("students")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("age").AsInt32().NotNullable()
                .WithColumn("university").AsString().NotNullable();

            Create.Table("books")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("created").AsDateTime().NotNullable()
                .WithColumn("student_id").AsInt32().ForeignKey("students", "id").OnDelete(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("books");
            Delete.Table("students");
        }
    }
}