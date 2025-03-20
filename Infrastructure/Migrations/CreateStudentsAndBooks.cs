using FluentMigrator;

namespace OnlineSchool.Infrastructure.Migrations
{
    [Migration(20240320001)]
    public class CreateStudentsAndBooks : Migration
    {
        public override void Up()
        {
            // Create Students Table
            Create.Table("Students")
                .WithColumn("StudentId").AsGuid().PrimaryKey()
                .WithColumn("FirstName").AsString(100).NotNullable()
                .WithColumn("LastName").AsString(100).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable();

            // Create Books Table
            Create.Table("Books")
                .WithColumn("BookId").AsGuid().PrimaryKey()
                .WithColumn("Title").AsString(255).NotNullable()
                .WithColumn("Author").AsString(255).NotNullable()
                .WithColumn("ISBN").AsString(13).NotNullable()
                .WithColumn("StudentId").AsGuid().NotNullable()
                .ForeignKey("FK_Books_Students", "Students", "StudentId")
                .OnDelete(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            // Drop tables in reverse order to avoid foreign key issues
            Delete.Table("Books");
            Delete.Table("Students");
        }
    }
}