using FastDev.Service;
using FastDev.Infra.Data;
using FastDev.Infra.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FastDev.Sample.DbContexts;
using FastDev.Sample.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o=>o.JsonSerializerOptions
.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSwaggerGen(options => options.CustomSchemaIds(x=>x.FullName));

builder.Services.AddDbContext<CategoriesDbContext>(o => o.UseInMemoryDatabase("AppTesteFastDevDB"));
builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("AppTesteFastDevDB"));

builder.Services.AddTransient(typeof(IUoW<>), typeof(UoW<>));

builder.Services.AddTransient<IRepositoryBase<FastDev.Sample.Models.Category,Guid>, FastDev.Infra.Data.EntityFrameworkCore.RepositoryBase<FastDev.Sample.Models.Category,Guid,AppDbContext>>();
builder.Services.AddTransient<IRepositoryBase<FastDev.Sample.Models.Product,Guid>, FastDev.Infra.Data.EntityFrameworkCore.RepositoryBase<FastDev.Sample.Models.Product,Guid,AppDbContext>>();


builder.Services.AddTransient<IRepositoryBase<FastDev.Sample.Models.Context.Category.Category,Guid>, FastDev.Infra.Data.EntityFrameworkCore.RepositoryBase<FastDev.Sample.Models.Context.Category.Category,Guid,CategoriesDbContext>>();

builder.Services.AddTransient(typeof(IServiceBase<,,>), typeof(ServiceBase<,,>));

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
