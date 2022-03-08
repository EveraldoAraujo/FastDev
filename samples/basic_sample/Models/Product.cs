using System;
using FastDev.Infra.Data.Model;

namespace FastDev.Sample.Models;
public class Product : DataModel<Guid>
{
    public String Name { get; set; }
    public float Price { get; set; }
    public Category? Category { get; set; }
    public Guid CategoryId { get; set; }

    public Product(string name, Guid categoryId, float price)
    {
        Name = name;
        CategoryId = categoryId;
        Price = price;
    }
}