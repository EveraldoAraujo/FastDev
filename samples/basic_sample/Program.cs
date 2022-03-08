using FastDev.Service;
using FastDev.Infra.Data;
using FastDev.Infra.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FastDev.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o=>o.JsonSerializerOptions
.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("AppTesteFastDevDB"));

builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddTransient<IUoW, UoW>();
builder.Services.AddTransient(typeof(IRepositoryBase<,>), typeof(FastDev.Infra.Data.EntityFrameworkCore.RepositoryBase<,>));
builder.Services.AddTransient(typeof(IServiceBase<,>), typeof(ServiceBase<,>));

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
