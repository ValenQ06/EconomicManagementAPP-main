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
            var accountTypes = await repositorieAccountTypes.getAccounts();
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

            var accountTypeExist =
               await repositorieAccountTypes.Exist(accountTypes.Name);

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
            var accountTypeExist = await repositorieAccountTypes.Exist(Name);

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
            var accountType = await repositorieAccountTypes.getAccountById(id);

            if (accountType is null)
            { 
                return RedirectToAction("NotFound", "Home");
            }
            return View(accountType);
        }
        
        [HttpPost]
        public async Task<ActionResult> Modify(AccountTypes accountTypes)
        {
            var accountType = await repositorieAccountTypes.getAccountById(accountTypes.Id);

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
            var account = await repositorieAccountTypes.getAccountById(id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await repositorieAccountTypes.getAccountById(id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccountTypes.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
