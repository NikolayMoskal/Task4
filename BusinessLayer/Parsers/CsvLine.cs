using System;

namespace BusinessLayer.Parsers
{
    public class CsvLine
    {
        public string EmployeeName { get; }
        public DateTime Date { get; }
        public string ClientName { get; }
        public string ProductName { get; }
        public double ProductSum { get; }

        public CsvLine(string employeeName, params string[] csvValues)
        {
            if (csvValues.Length != 4)
            {
                throw new ArgumentException($"Incorrect params count {csvValues.Length}. Expected 4");
            }

            EmployeeName = string.IsNullOrEmpty(employeeName)
                ? throw new ArgumentException($"Invalid employee name")
                : employeeName;
            Date = DateTime.Parse(csvValues[0]);
            ClientName = csvValues[1] ?? throw new ArgumentNullException("ClientName");
            ProductName = csvValues[2] ?? throw new ArgumentNullException("ProductName");
            ProductSum = double.Parse(csvValues[3]);
        }
    }
}