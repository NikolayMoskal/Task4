using System;
using System.IO;
using System.Threading;
using BusinessLayer.Parsers;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer
{
    public class FileHandler
    {
        private readonly ReaderWriterLockSlim _locker;
        private readonly IParser<CsvLine> _parser;
        private readonly string _fileName;

        public FileHandler(string fileName, IParser<CsvLine> parser)
        {
            _fileName = fileName;
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _locker = new ReaderWriterLockSlim();
        }

        public void StartHandle()
        {
            var lines = _parser.ParseFile(_fileName);
            foreach (var line in lines)
            {
                _locker.EnterWriteLock();
                try
                {
                    var client = new Client {Name = line.ClientName};
                    var employee = new Employee {Name = line.EmployeeName};
                    var order = new Order {Client = client, Employee = employee};
                    var product = new Product
                        {Date = line.Date, Name = line.ProductName, Price = line.ProductSum, Order = order};
                    Save<ClientRepository, Client>(client);
                    Save<EmployeeRepository, Employee>(employee);
                    Save<OrderRepository, Order>(order);
                    Save<ProductRepository, Product>(product);
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }

            File.Move(_fileName, _fileName + "_");
        }

        private void Save<TRepository, TItem>(TItem item)
            where TRepository : RepositoryBase<TItem>, new()
            where TItem : class
        {
            using (var repository = new TRepository())
            {
                try
                {
                    repository.BeginTransaction();
                    if (!repository.Exists(item))
                    {
                        repository.Save(item);
                    }
                }
                catch
                {
                    repository.RollbackTransaction();
                }
            }
        }

        ~FileHandler()
        {
            _locker?.Dispose();
        }
    }
}