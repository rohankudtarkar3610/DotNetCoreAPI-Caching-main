using DataAccess.Data;
using Demo7.Model.Model;
using Demo7.Repository.Contracts;
using Demo7.Repository.Implementations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//string connectionString = builder.Configuration.GetConnectionString("ClickToCallDB");

//builder.Services.AddDbContext<PanditClinicContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<PanditClinicContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("ClickToCallDB")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options=>
    options.Configuration="localhost:5003"
);

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IItemMasterRepository, ItemMasterRepository>();
builder.Services.AddScoped<IValidator<GetItemMasterModel>, GetItemValidator>();
builder.Services.AddScoped<IValidator<saveItem>, SaveItemValidator>();
builder.Services.AddScoped<IValidator<UpdateItem>, UpdateItemValidator>();



builder.Logging.ClearProviders();

var path = builder.Configuration.GetValue<string>("Logging:FilePath") + "/BaseAPINew-" + System.DateTime.Now.ToString("dd-MMM-yyyy") + ".log";

//var logger = new LoggerConfiguration()
//    .WriteTo.File(path)
//    .CreateLogger();

//builder.Logging.AddSerilog(logger);


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
