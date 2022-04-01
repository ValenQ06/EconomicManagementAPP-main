using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositorie
{
    public interface IRepositorieUsers
    {
        Task Create(Users users); // Se agrega task por el asincronismo
        Task<bool> Exist(string Email); //Validamos el email existente
        Task<IEnumerable<Users>> getUser();
        Task Modify(Users users);
        Task<Users> getUserById(int Id);
        Task Delete(int Id);
        Task<Users> Login(string Email, string Password);
    }
}
