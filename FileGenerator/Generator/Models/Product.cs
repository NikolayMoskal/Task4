using System;

namespace FileGenerator.Generator.Models
{
    public class Product
    {
        public string Name { get; }
        public double Sum { get; }

        public Product(string name, double sum = 0)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Sum = sum <= 0 ? new Random().Next(10, 100) : sum;
        }
    }
}