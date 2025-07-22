using AspNetJWT.Configuration;
using AspNetJWT.Extensions;
using AspNetJWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
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

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("admin", p => p.RequireRole("admin"));
});

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

app.MapGet("/restricted", (ClaimsPrincipal user) => new
{
    id = user.GetUserId(),
    name = user.GetUserName(),
    givenName = user.GetUserGivenName(),
    email = user.GetUserEmail(),
    image = user.GetUserImage(),
}
).RequireAuthorization();

app.MapGet("/admin", () =>
"You have access").RequireAuthorization("admin");

app.Run();
