using System;
using BusinessLayer.Parsers;
using DataAccessLayer.Repositories;

namespace BusinessLayer
{
    public class FileHandler
    {
        private IParser<CsvLine> _parser;

        public FileHandler(IParser<CsvLine> parser)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public void StartHandle(string fileName)
        {
            var lines = _parser.ParseFile(fileName);
            foreach (var line in lines)
            {
                
            }
        }

        private void PutInDatabase<T>(T item) where T : class
        {
            using (var repository = new RepositoryBase<T>())
            {
                try
                {
                    repository.BeginTransaction();
                    repository.Save(item);
                }
                catch
                {
                    repository.RollbackTransaction();
                }
            }
        }
    }
}