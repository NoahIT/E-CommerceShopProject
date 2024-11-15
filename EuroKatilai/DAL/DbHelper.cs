﻿using Dapper;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DbHelper : IDbHelper
    {
        public static string ConnString = "";

        public async Task ExecuteAsync(string sql, object model)
        {
            using (var connection = new MySqlConnection(ConnString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task<T> QueryScalarAsync<T>(string sql, object model)
        {
            using (var connection = new MySqlConnection(ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<T>(sql, model);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using (var connection = new MySqlConnection(ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<T>(sql, model);
            }
        }

    }
}
