using System;

namespace Task4.Generator.Models
{
    public class Product
    {
        public string Name { get; }
        public double Sum { get; }

        public Product(string name, double sum)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Sum = sum < 0 ? throw new ArgumentException($"Wrong product sum: {sum}") : sum;
        }
    }
}