using Microsoft.EntityFrameworkCore;
using Repository;
using ApplicationBusiness.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Webapi;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddNewtonsoftJson(config =>
            {
                config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

        builder.Services.AddScoped<UsersService>();
        builder.Services.AddScoped<ImagesService>();
        builder.Services.AddScoped<PostsService>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<HomeRepairContext>(config =>
        {
            config.UseSqlServer(builder.Configuration.GetConnectionString("HomeRepairDatabase"));
        });

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            var tokenSecret = Encoding.Default.GetBytes(GetTokenSecret(builder.Configuration));

            config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidIssuer = "home-repair-token",
                IssuerSigningKey = new SymmetricSecurityKey(tokenSecret),
                ValidateAudience = false
            };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
    private static string GetTokenSecret(IConfiguration configuration)
    {
        string? value = configuration["TokenSecret"];

        if (value == null)
            return "";

        return value;
    }
}