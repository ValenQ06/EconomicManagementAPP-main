using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.IRepositorie
{
    public interface IRepositorieTransactions
    {
        Task Create(Transactions transactions);        
        Task<IEnumerable<Transactions>> getTransactions();
        Task Modify(Transactions transactions);
        Task<Transactions> getTransactionsById(int Id);
        Task Delete(int Id);
    }
}
