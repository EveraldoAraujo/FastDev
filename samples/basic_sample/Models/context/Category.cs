using System;
using System.Collections.Generic;
using FastDev.Infra.Data.Model;

namespace FastDev.Sample.Models.Context.Category;

public class Category : DataModel<Guid>
{
    public String Name { get; set; }

    public Category(string name)
    {
        Name = name;
    }
}