using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeProjectOnPharmacies.Interfaces;

namespace SomeProjectOnPharmacies.IO
{
    public class ConsoleEditor: IIOEditor
    {
        public Dictionary<string,string> GetDataFromConsole(List<string> fields)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var fieldName in fields)
            {
                Console.WriteLine($"Введите {fieldName}:");
                result.Add(fieldName, Console.ReadLine());
            }
            return result;
        }

        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
        public string GetData()
        {
            return Console.ReadLine();
        }
        public void Clear()
        {
            Console.Clear();
        }
    }
}
