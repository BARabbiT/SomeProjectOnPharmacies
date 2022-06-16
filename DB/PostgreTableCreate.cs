using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using SomeProjectOnPharmacies.Interfaces;

namespace SomeProjectOnPharmacies.DB
{
    public class PostgreTableCreate : IDBTableCreate
    {
        private string _connectionString;
        public PostgreTableCreate()
        {
            _connectionString = "Host=127.0.0.1;Username=postgres;Password=2700805;Database=someproject";
        }

        public void CreateShopTable()
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS shops(id uuid NOT NULL, name text, addres text, phone text, PRIMARY KEY (id))", pgsqlConnection))
                {
                    command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
        public void CreateStoreTable()
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS stores(id uuid NOT NULL, name text, shopid uuid NOT NULL, PRIMARY KEY (id))", pgsqlConnection))
                {
                    command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
        public void CreateNomenclatureTable()
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS nomenclatures(id uuid NOT NULL, name text, PRIMARY KEY (id))", pgsqlConnection))
                {
                    command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
        public void CreateBatchTable()
        {
            using (var pgsqlConnection = new NpgsqlConnection(_connectionString))
            {
                pgsqlConnection.Open();
                using (var command = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS batches(id uuid NOT NULL, storeid uuid NOT NULL, nomenclature uuid NOT NULL, count int, PRIMARY KEY (id))", pgsqlConnection))
                {
                    command.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }
    }
}
