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
    public class PostgreAdd : IDBAdd
    {
        private string _connectionString;
        public PostgreAdd()
        {
            _connectionString = ConfigurationManager.AppSettings["connectionString"];
        }

        public void CreateShopInDB(string shopName, string addres, string phone)
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"INSERT INTO shops (id, name, addres, phone) VALUES ('{Guid.NewGuid()}','{shopName}','{addres}','{phone}')", pgsqlConnection))
                {
                    int rows = command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
        public void CreateStoreInDB(string storeName, Guid shopId)
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"INSERT INTO stores (id, name, shopid) VALUES ('{Guid.NewGuid()}','{storeName}','{shopId}')", pgsqlConnection))
                {
                    int rows = command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
        public void CreateBatchInDB(Guid storeId, Guid nomenclatureId, string count)
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"INSERT INTO batches (id, storeid, nomenclatureid, count) VALUES ('{Guid.NewGuid()}', '{storeId}', '{nomenclatureId}', '{count}')", pgsqlConnection))
                {
                    int rows = command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
        public void CreateNomenclatureInDB(string nomenclatureName)
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"INSERT INTO nomenclatures (id, name) VALUES ('{Guid.NewGuid()}','{nomenclatureName}')", pgsqlConnection))
                {
                    int rows = command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
    }
}
