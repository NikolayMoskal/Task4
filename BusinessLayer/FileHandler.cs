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
                    Save<ClientRepository, Client>(client, out var savedClient);
                    var employee = new Employee {Name = line.EmployeeName};
                    Save<EmployeeRepository, Employee>(employee, out var savedEmployee);
                    var product = new Product {Name = line.ProductName, Price = line.ProductSum};
                    Save<ProductRepository, Product>(product, out var savedProduct);
                    var booking = new Booking
                    {
                        Date = line.Date, 
                        Client = savedClient ?? client, 
                        Employee = savedEmployee ?? employee,
                        Product = savedProduct ?? product
                    };
                    Save<BookingRepository, Booking>(booking, out _);
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }

            File.Move(_fileName, _fileName + "_");
        }

        private void Save<TRepository, TItem>(TItem item, out TItem foundItem)
            where TRepository : RepositoryBase<TItem>, new()
            where TItem : class
        {
            using (var repository = new TRepository())
            {
                try
                {
                    repository.BeginTransaction();
                    // проверяет существование объекта в базе и, если он существует, возвращает первый найденный
                    if (!repository.Exists(item, out var existsItem))
                    {
                        repository.Save(item);
                    }

                    // т.о., на выходе всегда получим или сохраненный объект с заданным Id, или уже существующий
                    foundItem = existsItem ?? item;
                }
                catch
                {
                    repository.RollbackTransaction();
                    foundItem = null;
                }
            }
        }

        ~FileHandler()
        {
            _locker?.Dispose();
        }
    }
}