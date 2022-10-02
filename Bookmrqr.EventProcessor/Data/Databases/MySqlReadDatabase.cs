//using MySql.Data.MySqlClient;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bookmrqr.EventProcessor.Store.Databases
//{
//    public class MySqlReadDatabase : IReadDatabase
//    {
//        private readonly string _connectionString;

//        public MySqlReadDatabase()
//        {
//            _connectionString = "";// ConfigurationManager.ConnectionStrings["MySqlBookmrqrAccounts"].ToString();
//        }

//        public AccountDto GetAccount(string userName)
//        {
//            const string sql = "SELECT id, version, displayName, email FROM Account WHERE id=@userName";
//            using (var conn = new MySqlConnection(_connectionString))
//            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new MySqlParameter("@userName", userName));
//                MySqlDataReader reader = cmd.ExecuteReader();
//                if (reader.Read())
//                {
//                    string displayName = !reader.IsDBNull(2) ? reader.GetString(2) : null;
//                    string email = !reader.IsDBNull(3) ? reader.GetString(3) : null;

//                    return new AccountDto
//                    {
//                        Id = reader.GetString(0),
//                        Version = reader.GetInt32(1),
//                        DisplayName = displayName,
//                        Email = email
//                    };
//                }

//            }
//            Trace.TraceInformation(DateTime.Now.ToString("s") + " - Account not found" + userName);
//            return null;
//        }

//        public void AddAccount(AccountDto accountDto)
//        {
//            if (DoesUserExist(accountDto.Id))
//                return;

//            const string sqlInsert = "INSERT INTO Account (id, version, displayName, email) VALUES(@Id, @Version, @DisplayName, @Email)";
//            using (var conn = new MySqlConnection(_connectionString))
//            using (MySqlCommand cmd = new MySqlCommand(sqlInsert, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new MySqlParameter("@Id", accountDto.Id));
//                cmd.Parameters.Add(new MySqlParameter("@Version", accountDto.Version));
//                cmd.Parameters.Add(new MySqlParameter("@DisplayName", accountDto.DisplayName));
//                cmd.Parameters.Add(new MySqlParameter("@Email", accountDto.Email));

//                cmd.ExecuteNonQuery();
//            }
//        }

//        public void UpdateAccount(AccountDto accountDto)
//        {
//            const string sqlUpdate = "UPDATE Account SET version=@Version, displayName=@DisplayName, email=@Email WHERE id=@Id";
//            using (var conn = new MySqlConnection(_connectionString))
//            using (MySqlCommand cmd = new MySqlCommand(sqlUpdate, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new MySqlParameter("@Id", accountDto.Id));
//                cmd.Parameters.Add(new MySqlParameter("@Version", accountDto.Version));
//                cmd.Parameters.Add(new MySqlParameter("@DisplayName", accountDto.DisplayName));
//                cmd.Parameters.Add(new MySqlParameter("@Email", accountDto.Email));

//                cmd.ExecuteNonQuery();
//            }
//        }

//        public bool DeleteAccount(string userName)
//        {
//            const string sql = "DELETE FROM Account WHERE id=@userName";
//            using (var conn = new MySqlConnection(_connectionString))
//            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new MySqlParameter("@userName", userName));
//                return (cmd.ExecuteNonQuery() == 1);
//            }
//        }

//        private bool DoesUserExist(string userName)
//        {
//            const string sql = "SELECT id FROM Account WHERE id=@userName";
//            using (var conn = new MySqlConnection(_connectionString))
//            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new MySqlParameter("@userName", userName));
//                object exists = cmd.ExecuteScalar();
//                return (exists != null);
//            }
//        }
//    }
//}
