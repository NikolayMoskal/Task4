using System;
using System.Collections.Generic;
using Task4.Generator.Interfaces;

namespace Task4.Generator.Models
{
    public class CsvLine : ICsvLine
    {
        private string EmployeeName { get; }
        private IList<Product> Products { get; }

        public CsvLine(string employeeName, IList<Product> products)
        {
            Products = products ?? throw new ArgumentNullException(nameof(products));
            EmployeeName = employeeName ?? "";
        }

        public string CombineLine()
        {
            var index = new Random().Next(0, Products.Count);
            return string.Join(",", DateTime.Now.ToString("d"), EmployeeName, Products[index].Name, Products[index].Sum);
        }
    }
}