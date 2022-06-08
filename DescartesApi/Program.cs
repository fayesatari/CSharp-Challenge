using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DescartesApi;
using DescartesApi.Data;


var builder = WebApplication.CreateBuilder(args);

//Faye//builder.Services.AddDbContext<DiffContext>(options =>
//Faye//    options.UseSqlServer(builder.Configuration.GetConnectionString("DiffContext") ?? throw new InvalidOperationException("Connection string 'DiffContext' not found.")));

builder.Services.AddDbContext<DiffContext>(opt =>
    opt.UseInMemoryDatabase("DiffDB"));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Faye//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //Faye//app.UseSwagger();
    //Faye//app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
