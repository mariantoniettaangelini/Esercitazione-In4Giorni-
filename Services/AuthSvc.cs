using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;

namespace Esercitazione.Services
{
    public class AuthSvc : IAuthSvc
    {
        private string connectionString;

        private const string LOGIN_COMMAND = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
        public AuthSvc(IConfiguration config)
        {
            connectionString = config.GetConnectionString("Dd")!;
        }

        public ApplicationUser Login(string username, string password)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(LOGIN_COMMAND, conn);
                cmd.Parameters.AddWithValue("@username", username);  
                cmd.Parameters.AddWithValue("@password", password);  
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Utente autenticato con successo
                    return new ApplicationUser
                    {
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                    };
                }
                return null;  
            }
            catch (Exception ex)
            {
                throw new Exception("Errore di login", ex);
            }
        }
    }
}
