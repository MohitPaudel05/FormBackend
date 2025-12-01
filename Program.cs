using AutoMapper;
using FormBackend.Data;
using FormBackend.Helpers;
using FormBackend.Services;

using FormBackend.Unit_Of_Work;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//json options

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // For enums as strings
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//CORS Policy

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


//  Dependency Injection

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// StudentService with wwwroot injection
builder.Services.AddScoped<IStudentService>(provider =>
{
    var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    var mapper = provider.GetRequiredService<IMapper>();
    var env = provider.GetRequiredService<IWebHostEnvironment>(); // inject wwwroot
    return new StudentService(unitOfWork, mapper, env);
});


//  Build App

var app = builder.Build();


//  Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseStaticFiles(); // serve wwwroot
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
