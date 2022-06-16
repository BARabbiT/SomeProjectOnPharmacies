using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeProjectOnPharmacies.DB.Models;

namespace SomeProjectOnPharmacies.Interfaces
{
    public interface IDBAdd
    {
        void CreateShopInDB(string shopName, string addres, string phone);
        void CreateStoreInDB(string storeName, Guid shopId);
        void CreateBatchInDB(Guid storeId, Guid nomenclatureId, string count);
        void CreateNomenclatureInDB(string nomenclatureName);
    }
}
