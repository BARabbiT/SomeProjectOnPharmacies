using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using SomeProjectOnPharmacies.DB.Models;
using SomeProjectOnPharmacies.Interfaces;

namespace SomeProjectOnPharmacies.DB
{
    public class PostgreGet : IDBGet
    {
        private string _connectionString;
        public PostgreGet()
        {
            _connectionString = ConfigurationManager.AppSettings["connectionString"];
        }

        public Guid GetShopIDFromDB(string shopName)
        {
            Guid id = Guid.Empty;
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT id FROM shops WHERE name = '{shopName}'", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        id = rows.GetGuid(0);
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return id;
        }
        public StoreModel GetStoreIdFromDB(string storeName)
        {
            StoreModel store = new StoreModel();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM stores WHERE name = '{storeName}'", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        store.Id = rows.GetGuid(0);
                        store.Name = rows.GetString(1);
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return store;
        }
        public List<StoreModel> GetStoresFromDBByShopID(Guid shopId)
        {
            List<StoreModel> stores = new List<StoreModel>();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM stores WHERE shopid = '{shopId}'", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        stores.Add(new StoreModel() { Id = rows.GetGuid(0), Name = rows.GetString(1) });
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return stores;
        }
        public List<BatchModel> GetBatchesFromDB(Guid storeId, Guid nomenclatureId)
        {
            List<BatchModel> batches = new List<BatchModel>();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM batches WHERE storeid = '{storeId}' and nomenclatureid = '{nomenclatureId}'", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        batches.Add(new BatchModel() { Id = rows.GetGuid(0), StoreId = rows.GetGuid(1), NomenclatureId = rows.GetGuid(2), Count = rows.GetInt32(3) });
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return batches;
        }
        public List<BatchModel> GetBatchesFromDBByStoreId(Guid storeId)
        {
            List<BatchModel> batches = new List<BatchModel>();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM batches WHERE storeid = '{storeId}'", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        batches.Add(new BatchModel() { Id = rows.GetGuid(0), StoreId = rows.GetGuid(1), NomenclatureId = rows.GetGuid(2), Count = rows.GetInt32(3) });
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return batches;
        }
        public List<BatchModel> GetBatchesFromDBByNomenclatureId(Guid nomenclatureId)
        {
            List<BatchModel> batches = new List<BatchModel>();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT id FROM batches WHERE nomenclatureid = '{nomenclatureId}'", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        batches.Add(new BatchModel() { Id = rows.GetGuid(0), StoreId = rows.GetGuid(1), NomenclatureId = rows.GetGuid(2), Count = rows.GetInt32(3) });
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return batches;
        }
        public NomenclatureModel GetNomenclatureFromDB(string nomenclatureName)
        {
            NomenclatureModel nomenclature = new NomenclatureModel();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM nomenclatures WHERE name = '{nomenclatureName}'", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        nomenclature = new NomenclatureModel() { Id = rows.GetGuid(0), Name = rows.GetString(1) };
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return nomenclature;
        }
        public NomenclatureModel GetNomenclatureFromDBById(Guid nomenclatureId)
        {
            NomenclatureModel nomenclature = new NomenclatureModel();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM nomenclatures WHERE id = '{nomenclatureId}'", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        nomenclature = new NomenclatureModel() { Id = rows.GetGuid(0), Name = rows.GetString(1) };
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return nomenclature;
        }

        public List<ShopModel> GetShopsFromDB()
        {
            List<ShopModel> shops = new List<ShopModel>();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM shops", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        shops.Add(new ShopModel() { Id = rows.GetGuid(0), Name = rows.GetString(1), Addres = rows.GetString(2), Phone = rows.GetString(3) });
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return shops;
        }
        public List<StoreModel> GetStoresFromDB()
        {
            List<StoreModel> stores = new List<StoreModel>();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM stores", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        stores.Add(new StoreModel() { Id = rows.GetGuid(0), Name = rows.GetString(1), ShopId = rows.GetGuid(2)});
                    }
                    rows.Close();
                }
                foreach(var store in stores)
                {
                    using (var command = new NpgsqlCommand($"SELECT name FROM shops WHERE id = '{store.ShopId}'", pgsqlConnection))
                    {
                        var rows = command.ExecuteReader();
                        while (rows.Read())
                        {
                            store.ShopName = rows.GetString(0);
                        }
                        rows.Close();
                    }
                }
                pgsqlConnection.Close();
            }
            return stores;
        }
        public List<NomenclatureModel> GetNomenclaturesFromDB()
        {
            List<NomenclatureModel> nomenclatures = new List<NomenclatureModel>();
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM nomenclatures", pgsqlConnection))
                {
                    var rows = command.ExecuteReader();
                    while (rows.Read())
                    {
                        nomenclatures.Add(new NomenclatureModel() { Id = rows.GetGuid(0), Name = rows.GetString(1)});
                    }
                    rows.Close();
                }
                pgsqlConnection.Close();
            }
            return nomenclatures;
        }
    }
}
