using SampleMongoDbDriver.Models;
using SampleMongoDbDriver.Repository.Interface;
using SampleMongoDbDriver.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add MongoDatabase
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("DefaultMongoDbDatabase"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Interface and implementation
builder.Services.AddScoped<IDbRepository, DbRepository>();

var app = builder.Build();

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