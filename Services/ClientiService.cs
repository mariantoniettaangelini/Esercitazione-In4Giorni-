using Esercitazione.Models;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace Esercitazione.Services
{
    public class ClientiService : SqlServerServiceBase, IClientiService
    {
        public ClientiService(IConfiguration config) : base(config)
        {
        }

        private Cliente Create(DbDataReader reader)
        {
            return new Cliente
            {
                Id = reader.GetInt32(0),
                IdPrivato = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                IdAzienda = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                Privato = reader.IsDBNull(3) ? null : new Privato
                {
                    Nome = reader.GetString(3),
                    CodiceFiscale = reader.GetString(4)
                },
                Azienda = reader.IsDBNull(5) ? null : new Azienda
                {
                    Nome = reader.GetString(5),
                    PartitaIVA = reader.GetString(6)
                }
            };
        }

        public IEnumerable<Cliente> GetAll()
        {
            var query = @"
                SELECT 
                    c.id, 
                    c.id_privato, 
                    c.id_azienda, 
                    p.nome AS NomePrivato, 
                    p.codice_fiscale, 
                    a.nome AS NomeAzienda, 
                    a.partita_iva 
                FROM Clienti c 
                LEFT JOIN Privati p ON c.id_privato = p.id 
                LEFT JOIN Aziende a ON c.id_azienda = a.id";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = GetCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var listaClienti = new List<Cliente>();
                        while (reader.Read())
                        {
                            listaClienti.Add(Create(reader));
                        }
                        return listaClienti;
                    }
                }
            }
        }

        public Cliente GetById(int id)
        {
            var query = @"
                SELECT 
                    c.id, 
                    c.id_privato, 
                    c.id_azienda, 
                    p.nome AS NomePrivato, 
                    p.codice_fiscale, 
                    a.nome AS NomeAzienda, 
                    a.partita_iva 
                FROM Clienti c 
                LEFT JOIN Privati p ON c.id_privato = p.id 
                LEFT JOIN Aziende a ON c.id_azienda = a.id
                WHERE c.id = @id";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Create(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void Create(Cliente cliente)
        {
            var query = @"
                INSERT INTO Clienti (id_privato, id_azienda) 
                VALUES (@IdPrivato, @IdAzienda);
                SELECT SCOPE_IDENTITY();";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdPrivato", cliente.IdPrivato));
                    cmd.Parameters.Add(new SqlParameter("@IdAzienda", cliente.IdAzienda));

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        cliente.Id = Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("Creazione del cliente non riuscita.");
                    }
                }
            }
        }
    }
}
