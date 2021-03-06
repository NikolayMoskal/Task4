using System;
using FileGenerator.Generator.Interfaces;

namespace FileGenerator.Generator.Models
{
    public class SurnameDateFileName : IFileName
    {
        private string Separator { get; }
        private string Surname { get; }
        private DateTime Date { get; }

        public SurnameDateFileName(string surname, DateTime date, string separator = "-")
        {
            Separator = string.IsNullOrEmpty(separator) ? "-" : separator;
            Surname = surname;
            Date = date;
        }

        public string Build()
        {
            return Surname + Separator + Date.ToString("ddMMyyyy");
        }
    }
}