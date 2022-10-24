using ChatApp.API.AuthFillters;
using ChatApp.API.Extensions;
using ChatApp.API.Hubs;
using ChatApp.Application;
using ChatApp.Application.Interfaces.DAL;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Infrastructure;
using ChatApp.Infrastructure.Contexts;
using ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration,
                typeof(IChatDbContext), typeof(ChatDbContext));
builder.Services.AddAuthentication(options =>
{
  
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
}).AddCookie(options =>
{
    options.Events.OnRedirectToLogin = options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "JWTServicePostmanClient",
        ValidIssuer = "JWTAuthenticationServer",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("SecretKey")))
    };
}).AddGoogle(opts =>
{
    opts.ClientId = "1010640552619-lkbctg2btcb7rllau01pt5pj1nb935t2.apps.googleusercontent.com";
    opts.ClientSecret = "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";
    opts.Scope.Add("profile");
    opts.Events.OnCreatingTicket = (context) =>
    {
        var picture = context.User.GetProperty("picture").GetString();

        context.Identity.AddClaim(new Claim("picture", picture));

        return Task.CompletedTask;
    };
    opts.SaveTokens = true;
}); 

builder.Services.AddDbContext<ChatDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDb"));
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
builder.Services.AddScoped<AuthorizationUserIdFillter>();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.MapHub<UserOperationHub>("/userhub");
app.Run();
