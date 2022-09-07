using ChatApp.API.Extensions;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Infrastructure.Contexts;
using ChatApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChatDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDb"));
});
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
using (var scope= app.Services.CreateScope())
{
    var hashser = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    var db = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
    await db.SeedData(hashser);

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
