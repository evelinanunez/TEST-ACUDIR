using System.Text.Json.Serialization;
using System.Text.Json;
using Acudir.Test.Apis.Models;
using Acudir.Test.Apis.Repositories;
using Acudir.Test.Apis.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


var filePath = Path.Combine(builder.Environment.ContentRootPath, "Data", "Test.json");
var json = File.ReadAllText(filePath);
var data = JsonSerializer.Deserialize<List<Persona>>(json, new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

//Dedidi trabajarlo con JsonSerializer
//var dataTest = System.IO.File.ReadAllText(@"Test.json");

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<List<Persona>>(data);
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPersonaService, PersonaService>();


var app = builder.Build();


IWebHostEnvironment environment = app.Environment;
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