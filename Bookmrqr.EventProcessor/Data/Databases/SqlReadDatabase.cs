//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bookmrqr.EventProcessor.Store.Databases
//{
//    public class SqlReadDatabase : IReadDatabase
//    {
//        private readonly string _connectionString;

//        public SqlReadDatabase()
//        {
//            _connectionString = "";// ConfigurationManager.ConnectionStrings["BookmrqrAccounts"].ToString();
//        }

//        public AccountDto GetAccount(string userName)
//        {
//            const string sql = "SELECT id, version, displayName, email FROM Account WHERE id=@userName";
//            using (var conn = new SqlConnection(_connectionString))
//            using (SqlCommand cmd = new SqlCommand(sql, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new SqlParameter("@userName", userName));
//                SqlDataReader reader = cmd.ExecuteReader();
//                if (reader.Read())
//                {
//                    return new AccountDto
//                    {
//                        Id = reader.GetString(0),
//                        Version = reader.GetInt32(1),
//                        DisplayName = reader.GetString(2),
//                        Email = reader.GetString(3)
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
//            using (var conn = new SqlConnection(_connectionString))
//            using (SqlCommand cmd = new SqlCommand(sqlInsert, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new SqlParameter("@Id", accountDto.Id));
//                cmd.Parameters.Add(new SqlParameter("@Version", accountDto.Version));
//                cmd.Parameters.Add(new SqlParameter("@DisplayName", accountDto.DisplayName));
//                cmd.Parameters.Add(new SqlParameter("@Email", accountDto.Email));

//                cmd.ExecuteNonQuery();
//            }
//        }

//        public void UpdateAccount(AccountDto accountDto)
//        {
//            const string sqlUpdate = "UPDATE Account SET version=@Version, displayName=@DisplayName, email=@Email WHERE id=@Id";
//            using (var conn = new SqlConnection(_connectionString))
//            using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new SqlParameter("@Id", accountDto.Id));
//                cmd.Parameters.Add(new SqlParameter("@Version", accountDto.Version));
//                cmd.Parameters.Add(new SqlParameter("@DisplayName", accountDto.DisplayName));
//                cmd.Parameters.Add(new SqlParameter("@Email", accountDto.Email));

//                cmd.ExecuteNonQuery();
//            }
//        }

//        public bool DeleteAccount(string userName)
//        {
//            const string sql = "DELETE FROM Account WHERE id=@userName";
//            using (var conn = new SqlConnection(_connectionString))
//            using (SqlCommand cmd = new SqlCommand(sql, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new SqlParameter("@userName", userName));
//                return (cmd.ExecuteNonQuery() == 1);
//            }
//        }

//        private bool DoesUserExist(string userName)
//        {
//            const string sql = "SELECT id FROM Account WHERE id=@userName";
//            using (var conn = new SqlConnection(_connectionString))
//            using (SqlCommand cmd = new SqlCommand(sql, conn))
//            {
//                conn.Open();
//                cmd.Parameters.Add(new SqlParameter("@userName", userName));
//                object exists = cmd.ExecuteScalar();
//                return (exists != null);
//            }
//        }
//    }
//}
