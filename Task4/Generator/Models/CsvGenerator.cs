using System;
using System.IO;
using System.Text;
using NLog;
using Task4.Generator.Interfaces;

namespace Task4.Generator.Models
{
    public class CsvGenerator
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly string _fileExtension;
        private readonly IFileName _fileName;
        private readonly ICsvLine _line;

        public CsvGenerator(IFileName fileName, ICsvLine line)
        {
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            _line = line ?? throw new ArgumentNullException(nameof(line));
            _fileExtension = ".csv";
        }

        private void CreateDir(string directoryPath)
        {
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (PathTooLongException e)
            {
                Logger.Error($"{e.Message}: {e.StackTrace}");
            }
            catch (DirectoryNotFoundException e)
            {
                Logger.Error($"Invalid path: {e.StackTrace}");
            }
            catch (IOException e)
            {
                Logger.Error($"This directory path is a file: {e.StackTrace}");
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Error($"Permission error: {e.StackTrace}");
            }
            catch (ArgumentNullException e)
            {
                Logger.Error($"{e.Message}: {e.StackTrace}");
            }
            catch (ArgumentException e)
            {
                Logger.Error($"Incorrect directory path: {e.StackTrace}");
            }
            catch (NotSupportedException e)
            {
                Logger.Error($"{e.Message}: {e.StackTrace}");
            }
        }

        private FileStream CreateFile(string filePath)
        {
            try
            {
                return File.Create(filePath);
            }
            catch (PathTooLongException e)
            {
                Logger.Error($"{e.Message}: {e.StackTrace}");
            }
            catch (DirectoryNotFoundException e)
            {
                Logger.Error($"Invalid path: {e.StackTrace}");
            }
            catch (IOException e)
            {
                Logger.Error($"Error while creating file: {e.StackTrace}");
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Error($"Permission error: {e.StackTrace}");
            }
            catch (ArgumentNullException e)
            {
                Logger.Error($"{e.Message}: {e.StackTrace}");
            }
            catch (ArgumentException e)
            {
                Logger.Error($"Incorrect directory path: {e.StackTrace}");
            }
            catch (NotSupportedException e)
            {
                Logger.Error($"{e.Message}: {e.StackTrace}");
            }

            return null;
        }

        private void WriteLines(string fullPath, string text)
        {
            try
            {
                using (var stream = CreateFile(fullPath))
                {
                    var info = new UTF8Encoding(true).GetBytes(text);
                    stream.Write(info, 0, info.Length);
                }
            }
            catch (NotSupportedException e)
            {
                Logger.Error($"The current stream doesn't support writing: {e.StackTrace}");
            }
        }
        
        public void Generate(string directoryPath, int linesCount)
        {
            // generate new CSV content
            var builder = new StringBuilder(0);
            for (var index = 0; index < linesCount; index++)
            {
                builder.AppendLine(_line.CombineLine());
            }
            // check directory
            if (!Directory.Exists(directoryPath))
            {
                CreateDir(directoryPath);
            }
            // create a new file on disk by generated file name and write content
            var fileName = _fileName.Build();
            var fullPath = Path.Combine(directoryPath, fileName + _fileExtension);
            WriteLines(fullPath, builder.ToString());
        }
    }
}