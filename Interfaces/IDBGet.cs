using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeProjectOnPharmacies.DB.Models;

namespace SomeProjectOnPharmacies.Interfaces
{
    public interface IDBGet
    {
        Guid GetShopIDFromDB(string shopName);
        StoreModel GetStoreIdFromDB(string storeName);
        List<StoreModel> GetStoresFromDBByShopID(Guid shopId);
        List<BatchModel> GetBatchesFromDB(Guid storeId, Guid nomenclatureId);
        List<BatchModel> GetBatchesFromDBByStoreId(Guid storeId);
        List<BatchModel> GetBatchesFromDBByNomenclatureId(Guid nomenclatureId);
        NomenclatureModel GetNomenclatureFromDB(string nomenclatureName);
        NomenclatureModel GetNomenclatureFromDBById(Guid nomenclatureId);

        List<ShopModel> GetShopsFromDB();
        List<StoreModel> GetStoresFromDB();
        List<NomenclatureModel> GetNomenclaturesFromDB();
    }
}
