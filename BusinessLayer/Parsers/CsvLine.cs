using System;

namespace BusinessLayer.Parsers
{
    public class CsvLine
    {
        public DateTime Date { get; }
        public string ClientName { get; }
        public string ProductName { get; }
        public double ProductSum { get; }

        public CsvLine(DateTime dateTime, string clientName, string productName, double productSum)
        {
            Date = dateTime;
            ClientName = clientName ?? throw new ArgumentNullException(nameof(clientName));
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            ProductSum = productSum;
        }

        public CsvLine(params string[] values)
        {
            if (values.Length != 4)
            {
                throw new ArgumentException($"Incorrect params count {values.Length}. Expected 4");
            }

            Date = DateTime.Parse(values[0]);
            ClientName = values[1] ?? throw new ArgumentNullException("ClientName");
            ProductName = values[2] ?? throw new ArgumentNullException("ProductName");
            ProductSum = double.Parse(values[3]);
        }
    }
}