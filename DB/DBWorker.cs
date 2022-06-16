using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeProjectOnPharmacies.DB.Models;
using SomeProjectOnPharmacies.Interfaces;

namespace SomeProjectOnPharmacies.DB
{
    public class DBWorker
    {
        private IDBAdd _postgreAdd;
        private IDBGet _postgreGet;
        private IDBDelete _postgreDelete;

        public DBWorker()
        {
            _postgreAdd = new PostgreAdd();
            _postgreGet = new PostgreGet();
            _postgreDelete = new PostgreDelete();
        }

        //Shop
        public bool TryAddShopToDB(Dictionary<string, string> shopData, out string error)
        {
            if (shopData.TryGetValue("Название", out string shopName) && shopData.TryGetValue("Адрес", out string shopAddres) && shopData.TryGetValue("Телефон", out string shopPhone))
            {
                try
                {
                    _postgreAdd.CreateShopInDB(shopName, shopAddres, shopPhone);
                    error = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
            else
            {
                error = "Неверно указан один из параметров.";
                return false;
            }
        }
        public bool TryGetShopsFromDB(out List<ShopModel> shopData, out string error)
        {
            shopData = new List<ShopModel>();
            error = string.Empty;
            try
            {
                shopData = _postgreGet.GetShopsFromDB();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }
        public bool TryDeleteShopFromDB(Dictionary<string, string> shopData, out string error)
        {
            if (shopData.TryGetValue("Название", out string shopName))
            {
                try
                {
                    Guid shopId = _postgreGet.GetShopIDFromDB(shopName);
                    foreach (var storeId in _postgreGet.GetStoresFromDBByShopID(shopId))
                    {
                        foreach (var batch in _postgreGet.GetBatchesFromDBByStoreId(storeId.Id))
                        {
                            _postgreDelete.DeleteBatchFromDB(batch.Id);
                        }
                        _postgreDelete.DeleteStoreFromDB(storeId.Id);
                    }
                    _postgreDelete.DeleteShopFromDB(shopId);
                    error = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
            else
            {
                error = "Неверно указан один из параметров.";
                return false;
            }

        }
        public bool TryShowNomenclatureFromShop(Dictionary<string, string> shopData, out string showData, out string error)
        {
            showData = string.Empty;
            if (shopData.TryGetValue("Название", out string shopName))
            {
                try
                {
                    Guid shopId = _postgreGet.GetShopIDFromDB(shopName);

                    foreach (StoreModel store in _postgreGet.GetStoresFromDBByShopID(shopId))
                    {
                        showData += $"\n\r Склад: {store.Name}";
                        foreach (var batch in _postgreGet.GetBatchesFromDBByStoreId(store.Id))
                        {
                            showData += $"\n\r Товар: {_postgreGet.GetNomenclatureFromDBById(batch.NomenclatureId).Name} | Количество: {batch.Count} | Партия {batch.Id}";
                        }
                    }
                    error = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
            else
            {
                error = "Неверно указан один из параметров.";
                return false;
            }
        }

        //Store
        public bool TryAddStoreToDB(Dictionary<string, string> storeData, out string error)
        {
            if (storeData.TryGetValue("Название", out string storeName) && storeData.TryGetValue("Аптека", out string shopName))
            {
                try
                {
                    Guid shopId = _postgreGet.GetShopIDFromDB(shopName);
                    if (shopId != Guid.Empty)
                    {
                        _postgreAdd.CreateStoreInDB(storeName, shopId);
                        error = string.Empty;
                        return true;
                    }
                    else
                    {
                        throw new Exception("Неверно указано название аптеки.");
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
            else
            {
                error = "Неверно указан один из параметров.";
                return false;
            }
        }
        public bool TryGetStoresFromDB(out List<StoreModel> storeData, out string error)
        {
            storeData = new List<StoreModel>();
            error = string.Empty;
            try
            {
                storeData = _postgreGet.GetStoresFromDB();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }
        public bool TryDeleteStoreFromDB(Dictionary<string, string> storeData, out string error)
        {
            if (storeData.TryGetValue("Название", out string storeName))
            {
                try
                {
                    Guid storeId = _postgreGet.GetStoreIdFromDB(storeName).Id;
                    foreach (var batch in _postgreGet.GetBatchesFromDBByStoreId(storeId))
                    {
                        _postgreDelete.DeleteBatchFromDB(batch.Id);
                    }
                    _postgreDelete.DeleteStoreFromDB(storeId);
                    error = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
            else
            {
                error = "Неверно указан один из параметров.";
                return false;
            }
        }

        //Nomenclature
        public bool TryAddNomenclatureToDB(Dictionary<string, string> nomenclatureData, out string error)
        {
            if (nomenclatureData.TryGetValue("Название", out string nomenclatureName))
            {
                try
                {
                    _postgreAdd.CreateNomenclatureInDB(nomenclatureName);
                    error = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
            else
            {
                error = "Неверно указан один из параметров.";
                return false;
            }
        }
        public bool TryGetNomenclaturesFromDB(out List<NomenclatureModel> nomenclatureData, out string error)
        {
            nomenclatureData = new List<NomenclatureModel>();
            error = string.Empty;
            try
            {
                nomenclatureData = _postgreGet.GetNomenclaturesFromDB();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }
        public bool TryDeleteNomenclatureFromDB(Dictionary<string, string> nomenclatureData, out string error)
        {
            if (nomenclatureData.TryGetValue("Название", out string nomenclatureName))
            {
                try
                {
                    Guid nomenclatureId = _postgreGet.GetNomenclatureFromDB(nomenclatureName).Id;
                    foreach (var batch in _postgreGet.GetBatchesFromDBByNomenclatureId(nomenclatureId))
                    {
                        _postgreDelete.DeleteBatchFromDB(batch.Id);
                    }
                    _postgreDelete.DeleteNomenclatureFromDB(nomenclatureId);
                    error = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
            else
            {
                error = "Неверно указан один из параметров.";
                return false;
            }
        }

        //Batch
        public bool TryAddBatchToDB(Dictionary<string, string> batchData, out string error)
        {
            if (batchData.TryGetValue("Склад", out string storeName) && batchData.TryGetValue("Товар", out string nomencletureName) && batchData.TryGetValue("Количество", out string count))
            {
                try
                {
                    Guid nomenclatureId = _postgreGet.GetNomenclatureFromDB(nomencletureName).Id;
                    if (nomenclatureId != Guid.Empty)
                    {
                        Guid storeId = _postgreGet.GetStoreIdFromDB(storeName).Id;
                        if (storeId != Guid.Empty)
                        {
                            _postgreAdd.CreateBatchInDB(storeId, nomenclatureId, count);

                            error = string.Empty;
                            return true;
                        }
                        else
                        {
                            throw new Exception("Неверно указано название склада.");
                        }
                    }
                    else
                    {
                        throw new Exception("Неверно указано название товара.");
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
            else
            {
                error = "Неверно указан один из параметров.";
                return false;
            }
        }
        public bool TryDeleteBatchFromDB(Dictionary<string, string> batchData, out string error)
        {
            if (batchData.TryGetValue("Склад", out string storeName) && batchData.TryGetValue("Товар", out string nomencletureName))
            {
                try
                {
                    Guid storeId = _postgreGet.GetStoreIdFromDB(storeName).Id;
                    Guid nomenclatureId = _postgreGet.GetNomenclatureFromDB(nomencletureName).Id;

                    foreach (BatchModel batch in _postgreGet.GetBatchesFromDB(storeId, nomenclatureId))
                    {
                        _postgreDelete.DeleteBatchFromDB(batch.Id);
                    }

                    error = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
            else
            {
                error = "Неверно указан один из параметров.";
                return false;
            }
        }
    }
}