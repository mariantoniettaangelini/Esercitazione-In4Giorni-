using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;

namespace Esercitazione.Services
{
    public class AuthSvc : IAuthSvc
    {
        private string connectionString;

        private const string LOGIN_COMMAND = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
        private const string GET_CLIENT_ID_COMMAND = "SELECT Id FROM Clienti WHERE id_privato = (SELECT Id FROM Privati WHERE nome = @username) OR id_azienda = (SELECT Id FROM Aziende WHERE nome = @username)";

        public AuthSvc(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DbBW")!;
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
                        Role = reader["Role"].ToString()
                    };
                }
                return null;  
            }
            catch (Exception ex)
            {
                throw new Exception("Errore di login", ex);
            }
        }

        public int? GetClientIdByUsername(string username)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(GET_CLIENT_ID_COMMAND, conn);
                cmd.Parameters.AddWithValue("@username", username);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Convert.ToInt32(reader["Id"]);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'ID cliente", ex);
            }
        }
    }
}
