using System;
using static System.Console;

namespace RabbitMQ.Core.Logging
{
    public class SimpleConsoleLogger<T> where T : class
    {
        private readonly string _typeFullName = typeof(T).FullName;

        private string LogPrefix => $"{DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.FFF")} - {_typeFullName}";

        public void Log(string message)
        {
            WriteLine($"{LogPrefix} - {message}");
        }
    }
}
