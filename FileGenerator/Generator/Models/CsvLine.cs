using System;
using FileGenerator.Generator.Interfaces;

namespace FileGenerator.Generator.Models
{
    public class CsvLine : ICsvLine
    {
        private string ClientName { get; }
        private Product Product { get; }

        public CsvLine(string clientName, Product product)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            ClientName = string.IsNullOrEmpty(clientName) 
                ? throw new ArgumentException("Client name is not present") 
                : clientName;
        }

        public string CombineLine()
        {
            return string.Join(",", DateTime.Now.ToString("d"), ClientName, Product.Name, Product.Sum);
        }
    }
}