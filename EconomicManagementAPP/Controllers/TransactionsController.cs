using EconomicManagementAPP.IRepositorie;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class TransactionsController : Controller
    {

        private readonly IRepositorieTransactions repositorieTransactions;

        public TransactionsController(IRepositorieTransactions repositorieTransactions)
        {
            this.repositorieTransactions = repositorieTransactions;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var transactions = await repositorieTransactions.getTransactions();
            return View(transactions);
        }
        public IActionResult Create()
        {
            return View();
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> Create(Transactions transactions)
        {
            if (!ModelState.IsValid)
            {
                return View(transactions);
            }

            await repositorieTransactions.Create(transactions);
            return RedirectToAction("Index");
        }

        // Actualizar
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {
            var transaction = await repositorieTransactions.getTransactionsById(Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(transaction);
        }
       
        [HttpPost]
        public async Task<ActionResult> Modify(Transactions transactions)
        {
            var transaction = await repositorieTransactions.getTransactionsById(transactions.Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieTransactions.Modify(transactions);
            return RedirectToAction("Index");
        }

        // Eliminar
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var transaction = await repositorieTransactions.getTransactionsById(Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int Id)
        {
            var transaction = await repositorieTransactions.getTransactionsById(Id);

            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieTransactions.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
