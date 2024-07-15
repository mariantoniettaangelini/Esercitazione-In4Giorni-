using Esercitazione.Models;

namespace Esercitazione.Services
{
    public interface IClientiService
    {
        IEnumerable<Cliente> GetAll();
        Cliente GetById(int id);
        void Create(Cliente cliente);
    }
}
