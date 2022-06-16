using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProjectOnPharmacies.Interfaces
{
    public interface IDBTableCreate
    {
        void CreateShopTable();
        void CreateStoreTable();
        void CreateNomenclatureTable();
        void CreateBatchTable();
    }
}
