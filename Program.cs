using AspNetJWT.Configuration;
using AspNetJWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfiguration.PrivateKey)),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (TokenService service) =>
{
    return service.Create(new AspNetJWT.Models.User(
       1,
       "matheus@gmail.com",
       "Matheus",
       "Password123",
       "https://google.com",
       new[] { "student", "premium" }));
});

app.MapGet("/restricted", () =>
"You have access").RequireAuthorization();

app.Run();
