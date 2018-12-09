using System;
using System.Collections.Generic;
using FileGenerator.Generator.Interfaces;

namespace FileGenerator.Generator.Models
{
    public class CsvLine : ICsvLine
    {
        private List<string> ClientNames { get; }
        private List<Product> Products { get; }
        private readonly Random _random;

        public CsvLine(List<string> clientNames, List<Product> products)
        {
            Products = products ?? throw new ArgumentNullException(nameof(products));
            ClientNames = clientNames ?? throw new ArgumentNullException(nameof(clientNames));
            _random = new Random();
        }

        public string CombineLine()
        {
            var product = Products[_random.Next(0, Products.Count)];
            var clientName = ClientNames[_random.Next(0, ClientNames.Count)];
            return string.Join(",", DateTime.Now.ToString("d"), clientName, product.Name, product.Sum);
        }
    }
}