using Application.Services;
using Common.EmailHelper;
using Common.ReadTemplateHelper;
using Core.Models;
using Repositories.Webservices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICitiesServices, CitiesServices>();
builder.Services.AddScoped<IWebservicesCities, WebservicesCities>();
builder.Services.AddScoped<IReadTemplateHelper, ReadTemplateHelper>();
builder.Services.AddScoped<IEmailHelper, EmailHelper>();
builder.Services.AddScoped<IUserService, UserService>();

string _policyName = "CorsPolicy";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: _policyName, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(_policyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
