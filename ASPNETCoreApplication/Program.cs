using ASPNETCoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreApplication.Data;
using ASPNETCoreApplication.Services;
using Microsoft.Extensions.DependencyInjection;

var policyName = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EmployeeeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeAppCon")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbContextClass>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();
app.UseCors(policyName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();
