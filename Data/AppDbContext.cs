using online_school_api;
using Microsoft.EntityFrameworkCore;
using online_school_api.Students.Model;
using online_school_api.Books.Model;
using online_school_api.Students.Model;
using online_school_api.Books.Model;

namespace online_school_api.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Student> Students
        {

            get;set;
        }

        public virtual DbSet<Book> Books
        {

            get;set;
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Student>()
                .HasMany(s => s.Books)
                .WithOne(b => b.Student)
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
               

        }




    }
}
