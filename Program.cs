using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using OnlineSchool.Infrastructure.Persistence;
using OnlineSchool.Domain.Students;
using OnlineSchool.Domain.Books;
using OnlineSchool.Application.Students.Commands;
using OnlineSchool.Application.Books.Commands;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add Services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("online-school-api", policy => policy
                .WithOrigins("http://localhost:3000") // Example React dev server
                .AllowAnyHeader()
                .AllowAnyMethod());
        });

        // EF Core with MySQL
        builder.Services.AddDbContext<OnlineSchoolDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("Default")!,
                new MySqlServerVersion(new Version(8, 0, 21))));

        // FluentMigrator
        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddMySql5()
                .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        // AutoMapper
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Register Repositories & Handlers for DDD
        builder.Services.AddScoped<IStudentRepository, EfStudentRepository>();
        builder.Services.AddScoped<IBookRepository, EfBookRepository>();
        builder.Services.AddScoped<CreateStudentCommandHandler>();
        builder.Services.AddScoped<CreateBookCommandHandler>();

        var app = builder.Build();

        // Swagger in Development
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Middleware
        app.UseHttpsRedirection();
        app.UseCors("online-school-api");
        app.MapControllers();

        // Run FluentMigrator Migrations
        using (var scope = app.Services.CreateScope())
        {
            try
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
                Console.WriteLine("Migration successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration failed: {ex.Message}");
            }
        }

        app.Run();
    }
}
