using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProjectOnPharmacies.Interfaces
{
    public interface IIOEditor
    {
        Dictionary<string, string> GetDataFromConsole(List<string> fields);
        void SendMessage(string message);
        string GetData();
        void Clear();
    }
}
