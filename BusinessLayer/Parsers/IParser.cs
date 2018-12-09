using System.Collections.Generic;
using System.IO;

namespace BusinessLayer.Parsers
{
    public interface IParser<T> where T : class
    {
        IEnumerable<T> ParseFile(string fileName);
    }
}