using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositorie
{
    public interface IRepositorieAccounts
    {
        // Agregamos task por el asincronismo
        Task Create(Accounts accounts);
        Task<bool> Exist(string Name);
        Task<IEnumerable<Accounts>> getAccounts(); //Se crea metodo para la creacion de la lista
        Task Modify(Accounts accounts);
        Task<Accounts> getAccountsById(int Id);
        Task Delete(int Id);
    }
}
