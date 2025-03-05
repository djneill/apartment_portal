using apartment_portal_api.Abstractions;
using apartment_portal_api.Data;
using apartment_portal_api.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<PostgresContext>(
            options =>
                options.UseNpgsql(connectionString));

        builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<PostgresContext>();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.User.RequireUniqueEmail = true;
        });

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
            })
            .RequireAuthorization();

        app.UseHttpsRedirection();

        app.MapIdentityApi<ApplicationUser>();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
