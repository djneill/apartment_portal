using Microsoft.EntityFrameworkCore;
using apartment_portal_api.Data;
using apartment_portal_api.Models.Users;

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

                if (!context.User.Any()) // Seed data if db is empty
                {
                    context.User.AddRange(new List<User>
                    {
                        new(1, "John", "Doe", new DateTime(1990, 1, 1), 1, DateTime.UtcNow, 1, DateTime.UtcNow, 1),
                        new(2, "Jane", "Doe",new DateTime(1990, 2, 2), 1, DateTime.UtcNow, 1, DateTime.UtcNow, 1)
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
