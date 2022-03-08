using System;
using FastDev.Sample.Models;
using FastDev.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FastDev.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : BaseController<Product, Guid>
{
    // public ProductController(): base(o=> o.)
    // {
        
    // }
}