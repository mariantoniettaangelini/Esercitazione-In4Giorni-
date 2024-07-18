using Esercitazione.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;

namespace Esercitazione.Services
{
    public class ShipService : SqlServerServiceBase, IShipmentService
    {
        public ShipService(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Spedizioni> GetAll()
        {
            var query = @"
                SELECT 
                    s.id_spedizione, 
                    s.id_cliente, 
                    s.data_spedizione, 
                    s.peso, 
                    s.città_destinataria, 
                    s.indirizzo, 
                    s.destinatario, 
                    s.costo, 
                    s.data_consegna,
                    p.id AS IdPrivato,
                    p.nome AS NomePrivato,
                    p.codice_fiscale,
                    a.id AS IdAzienda,
                    a.nome AS NomeAzienda,
                    a.partita_iva
                FROM Spedizioni s
                LEFT JOIN Clienti c ON s.id_cliente = c.id
                LEFT JOIN Privati p ON c.id_privato = p.id
                LEFT JOIN Aziende a ON c.id_azienda = a.id";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = GetCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var listaSpedizioni = new List<Spedizioni>();
                        while (reader.Read())
                        {
                            var cliente = new Cliente
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                                IdPrivato = reader.IsDBNull(reader.GetOrdinal("IdPrivato")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("IdPrivato")),
                                IdAzienda = reader.IsDBNull(reader.GetOrdinal("IdAzienda")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("IdAzienda")),
                                TipoCliente = reader.IsDBNull(reader.GetOrdinal("IdPrivato")) ? "Azienda" : "Privato",
                                Privato = reader.IsDBNull(reader.GetOrdinal("IdPrivato")) ? null : new Privato
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("IdPrivato")),
                                    Nome = reader.GetString(reader.GetOrdinal("NomePrivato")),
                                    CodiceFiscale = reader.GetString(reader.GetOrdinal("codice_fiscale"))
                                },
                                Azienda = reader.IsDBNull(reader.GetOrdinal("IdAzienda")) ? null : new Azienda
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("IdAzienda")),
                                    Nome = reader.GetString(reader.GetOrdinal("NomeAzienda")),
                                    PartitaIVA = reader.GetString(reader.GetOrdinal("partita_iva"))
                                }
                            };

                            var spedizione = new Spedizioni
                            {
                                id_spedizione = reader.GetInt32(reader.GetOrdinal("id_spedizione")),
                                DataSpedizione = reader.GetDateTime(reader.GetOrdinal("data_spedizione")),
                                Peso = reader.GetDecimal(reader.GetOrdinal("peso")),
                                CittaDestinataria = reader.GetString(reader.GetOrdinal("città_destinataria")),
                                Indirizzo = reader.GetString(reader.GetOrdinal("indirizzo")),
                                Destinatario = reader.GetString(reader.GetOrdinal("destinatario")),
                                Costo = reader.GetDecimal(reader.GetOrdinal("costo")),
                                DataConsegna = reader.GetDateTime(reader.GetOrdinal("data_consegna")),
                                Cliente = cliente
                            };
                            listaSpedizioni.Add(spedizione);
                        }
                        return listaSpedizioni;
                    }
                }
            }
        }

        public List<Spedizioni> GetSpedizioniCliente(int idCliente)
        {
            var result = new List<Spedizioni>();
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = @"
                    SELECT 
                        s.id_spedizione, 
                        s.id_cliente, 
                        s.data_spedizione, 
                        s.peso, 
                        s.città_destinataria, 
                        s.indirizzo, 
                        s.destinatario, 
                        s.costo, 
                        s.data_consegna,
                        p.id AS IdPrivato,
                        p.nome AS NomePrivato,
                        p.codice_fiscale,
                        a.id AS IdAzienda,
                        a.nome AS NomeAzienda,
                        a.partita_iva
                    FROM Spedizioni s
                    LEFT JOIN Clienti c ON s.id_cliente = c.id
                    LEFT JOIN Privati p ON c.id_privato = p.id
                    LEFT JOIN Aziende a ON c.id_azienda = a.id
                    WHERE s.id_cliente = @idCliente";

                using (var cmd = GetCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = new Cliente
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                                IdPrivato = reader.IsDBNull(reader.GetOrdinal("IdPrivato")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("IdPrivato")),
                                IdAzienda = reader.IsDBNull(reader.GetOrdinal("IdAzienda")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("IdAzienda")),
                                TipoCliente = reader.IsDBNull(reader.GetOrdinal("IdPrivato")) ? "Azienda" : "Privato",
                                Privato = reader.IsDBNull(reader.GetOrdinal("IdPrivato")) ? null : new Privato
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("IdPrivato")),
                                    Nome = reader.GetString(reader.GetOrdinal("NomePrivato")),
                                    CodiceFiscale = reader.GetString(reader.GetOrdinal("codice_fiscale"))
                                },
                                Azienda = reader.IsDBNull(reader.GetOrdinal("IdAzienda")) ? null : new Azienda
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("IdAzienda")),
                                    Nome = reader.GetString(reader.GetOrdinal("NomeAzienda")),
                                    PartitaIVA = reader.GetString(reader.GetOrdinal("partita_iva"))
                                }
                            };

                            var spedizione = new Spedizioni
                            {
                                id_spedizione = reader.GetInt32(reader.GetOrdinal("id_spedizione")),
                                DataSpedizione = reader.GetDateTime(reader.GetOrdinal("data_spedizione")),
                                Peso = reader.GetDecimal(reader.GetOrdinal("peso")),
                                CittaDestinataria = reader.GetString(reader.GetOrdinal("città_destinataria")),
                                Indirizzo = reader.GetString(reader.GetOrdinal("indirizzo")),
                                Destinatario = reader.GetString(reader.GetOrdinal("destinatario")),
                                Costo = reader.GetDecimal(reader.GetOrdinal("costo")),
                                DataConsegna = reader.GetDateTime(reader.GetOrdinal("data_consegna")),
                                Cliente = cliente
                            };
                            result.Add(spedizione);
                        }
                    }
                }
            }
            return result;
        }
    }
}
