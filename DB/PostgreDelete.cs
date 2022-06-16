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
    public class PostgreDelete : IDBDelete
    {
        private string _connectionString;
        public PostgreDelete()
        {
            _connectionString = ConfigurationManager.AppSettings["connectionString"];
        }
        public void DeleteShopFromDB(Guid shopId)
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"DELETE FROM shops WHERE id = '{shopId}'", pgsqlConnection))
                {
                    int rows = command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
        public void DeleteStoreFromDB(Guid storeId)
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"DELETE FROM stores WHERE id = '{storeId}'", pgsqlConnection))
                {
                    int rows = command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
        public void DeleteBatchFromDB(Guid id)
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"DELETE FROM batches WHERE id = '{id}'", pgsqlConnection))
                {
                    int rows = command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
        public void DeleteNomenclatureFromDB(Guid nomenclatureId)
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand($"DELETE FROM nomenclatures WHERE id = '{nomenclatureId}'", pgsqlConnection))
                {
                    int rows = command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
    }
}
