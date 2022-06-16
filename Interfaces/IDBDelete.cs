using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProjectOnPharmacies.Interfaces
{
    public interface IDBDelete
    {
        void DeleteShopFromDB(Guid shopId);
        void DeleteStoreFromDB(Guid storeId);
        void DeleteBatchFromDB(Guid id);
        void DeleteNomenclatureFromDB(Guid nomenclatureId);
    }
}
