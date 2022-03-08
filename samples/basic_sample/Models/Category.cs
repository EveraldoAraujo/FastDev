using System;
using System.Collections.Generic;
using FastDev.Infra.Data.Model;

namespace FastDev.Sample.Models;

public class Category : DataModel<Guid>
{
    public String Name { get; set; }
    public IEnumerable<Product>? Products { get; set; }

    public Category(string name)
    {
        Name = name;
    }
}