using Domain.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy("default",
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                          });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddOData(options => options.EnableQueryFeatures().AddRouteComponents("odata", GetEdmModel()));
builder.Services.AddDbContext<DemoContext>(options =>
{
    options.UseSqlServer("SQL_SERVER_CONNECTION_STRING");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Student>(nameof(Student));
    builder.EntitySet<Person>(nameof(Person));
    builder.EntitySet<Course>(nameof(Course));
    return builder.GetEdmModel();
}
