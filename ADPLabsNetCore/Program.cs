using ADPLabsNetCore.Db;
using ADPLabsNetCore.Repositories;
using ADPLabsNetCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
                        opts =>
                        {
                            var enumConverter = new JsonStringEnumConverter();
                            opts.JsonSerializerOptions.Converters.Add(enumConverter);
                        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath); //in order to show summaries on swagger
});
builder.Services.AddDbContext<ADPContext>(options =>
                options.UseInMemoryDatabase(databaseName: "ADPDb"));
builder.Services.AddScoped<IADPRepository, ADPRepository>();
builder.Services.AddScoped<IExternalADPServices, ExternalADPServices>();
builder.Services.AddScoped<IADPCalcService, ADPCalcService>();

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
