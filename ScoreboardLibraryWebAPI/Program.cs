using ScoreboardLibrary.Repository;
using ScoreboardLibrary.Repository.Interfaces;
using ScoreboardLibrary.DAL.DBContext;
using ScoreboardLibrary.DAL.Entities;
using ScoreboardLibrary.DAL.Interfaces;
using ScoreboardLibraryWebAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using ScoreboardLibraryWebAPI.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBScoreboard>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<DataSeeder>();

builder.Services.AddScoped<IScoreboardRepository, ScoreboardRepository>();
builder.Services.AddScoped<IDBScoreboard, DBScoreboard>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

//Seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DBScoreboard>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}
var scopeFactory = app.Services.GetService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var service = scope.ServiceProvider.GetService<DataSeeder>();
    service.Initial();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();