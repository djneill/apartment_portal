using Microsoft.EntityFrameworkCore;
using apartment_portal_api.Data;
using apartment_portal_api.Entities;

namespace apartment_portal_api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!context.Users.Any()) // Seed data if db is empty
                {
                    context.Users.AddRange(new List<Users>
                    {
                        new Users { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateOnly(1990, 1, 1), StatusId = 1, CreatedOn = DateTime.UtcNow, CreatedBy = 1, ModifiedOn = DateTime.UtcNow, ModifiedBy = 1 },
                        new Users { Id = 2, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateOnly(1990, 2, 2), StatusId = 1, CreatedOn = DateTime.UtcNow, CreatedBy = 1, ModifiedOn = DateTime.UtcNow, ModifiedBy = 1 }
                    });
                    context.SaveChanges();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

        app.Run();
    }
}
