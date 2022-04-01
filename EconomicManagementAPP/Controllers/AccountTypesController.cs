using EconomicManagementAPP.IRepositorie;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class AccountTypesController : Controller
    {
        private readonly IRepositorieAccountTypes repositorieAccountTypes;
        public AccountTypesController(IRepositorieAccountTypes repositorieAccountTypes)
        {
            this.repositorieAccountTypes = repositorieAccountTypes;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var userId = 4;
            var accountTypes = await repositorieAccountTypes.getAccounts(userId);
            return View(accountTypes);
        }
        public IActionResult Create()
        {
            return View();
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> Create(AccountTypes accountTypes)
        {
            if (!ModelState.IsValid)
            {
                return View(accountTypes);
            }

            accountTypes.UserId = 4;
            accountTypes.OrderAccount = 1;

            var accountTypeExist =
               await repositorieAccountTypes.Exist(accountTypes.Name, accountTypes.UserId);

            if (accountTypeExist)
            {
                ModelState.AddModelError(nameof(accountTypes.Name),
                    $"The account {accountTypes.Name} already exist.");

                return View(accountTypes);
            }
            await repositorieAccountTypes.Create(accountTypes);
            return RedirectToAction("Index");
        }

        // Validacion
        [HttpGet]
        public async Task<IActionResult> VerificaryAccountType(string Name)
        {
            var UserId = 4;
            var accountTypeExist = await repositorieAccountTypes.Exist(Name, UserId);

            if (accountTypeExist)
            {
                return Json($"The account {Name} already exist");
            }
            return Json(true);
        }

        // Actualizar
        [HttpGet]
        public async Task<ActionResult> Modify(int id)
        {
            var userId = 4;
            var accountType = await repositorieAccountTypes.getAccountById(id, userId);

            if (accountType is null)
            { 
                return RedirectToAction("NotFound", "Home");
            }
            return View(accountType);
        }
        
        [HttpPost]
        public async Task<ActionResult> Modify(AccountTypes accountTypes)
        {
            var userId = 4;
            var accountType = await repositorieAccountTypes.getAccountById(accountTypes.Id, userId);

            if (accountType is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccountTypes.Modify(accountTypes);
            return RedirectToAction("Index");
        }

        // Eliminar
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = 4;
            var account = await repositorieAccountTypes.getAccountById(id, userId);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var userId = 4;
            var account = await repositorieAccountTypes.getAccountById(id, userId);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccountTypes.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
