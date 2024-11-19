using System.Runtime.Intrinsics.X86;
using System;
using System.Data.SqlClient;

namespace tt.interview.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUser(string username, string password)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection("constring"))
            {
                string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User() 
                            { 
                                FirstName = reader["FirstName"] != null ? reader["FirstName"].ToString() : string.Empty, 
                                LastName = reader["LastName"] != null ? reader["LastName"].ToString() : string.Empty
                            };
                        }
                    }
                }
            }

            return user;
        }
    }
}
