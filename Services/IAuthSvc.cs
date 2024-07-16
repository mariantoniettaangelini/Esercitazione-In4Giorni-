namespace Esercitazione.Services
{
    public interface IAuthSvc
    {
        ApplicationUser Login (string username, string password);
    }
}
