using Esercitazione.Models;
using System.Collections.Generic;

namespace Esercitazione.Services
{
    public interface IShipmentService
    {
        IEnumerable<Spedizioni> GetAll();
        List<Spedizioni> GetSpedizioniCliente(int idCliente);
    }
}
