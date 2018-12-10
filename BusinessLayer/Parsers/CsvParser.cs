using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace BusinessLayer.Parsers
{
    public class CsvParser : IParser<CsvLine>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public IEnumerable<CsvLine> ParseFile(string fileName)
        {
            try
            {
                var employeeName = fileName.Split('_')[0];
                var list = new List<CsvLine>(0);
                using (var stream = new StreamReader(fileName))
                {
                    string line;
                    while ((line = stream.ReadLine()) != null)
                    {
                        try
                        {
                            list.Add(new CsvLine(employeeName, line.Split(',')));
                        }
                        catch (Exception e)
                        {
                            Logger.Error($"The line <{line}> was not added. Cause: {e.Message}");
                        }
                    }
                }

                return list;
            }
            catch (ArgumentException e)
            {
                Logger.Error($"The path of file is empty or null: {e.StackTrace}");
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Error($"The path is a directory or access is denied: {e.StackTrace}");
            }
            catch (PathTooLongException e)
            {
                Logger.Error($"{e.Message}: {e.StackTrace}");
            }
            catch (DirectoryNotFoundException e)
            {
                Logger.Error($"The path is invalid: {e.StackTrace}");
            }
            catch (FileNotFoundException e)
            {
                Logger.Error($"The file by path {e.FileName} is not found: {e.StackTrace}");
            }
            catch (IOException e)
            {
                Logger.Error($"Error while rading the file {fileName}: {e.StackTrace}");
            }

            return null;
        }
    }
}