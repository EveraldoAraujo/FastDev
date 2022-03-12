using System;
using FastDev.Sample.Models;
using FastDev.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using FastDev.Sample.DbContexts;

namespace FastDev.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : BaseController<FastDev.Sample.Models.Context.Category.Category,Guid,CategoriesDbContext>
{

}
