using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;
using EconomicManagementAPP.IRepositorie;

namespace EconomicManagementAPP.Controllers
{
    public class AccountsController : Controller
    {
        // Inicializamos la variable repositorieAccounts trayendonos la interfaz
        private readonly IRepositorieAccounts repositorieAccounts;

        public AccountsController(IRepositorieAccounts repositorieAccounts)
        {
            this.repositorieAccounts = repositorieAccounts;
        }

        // Creamos el index para ejecutar la interfaz del Accounts
        public async Task<IActionResult> Index()
        {
            var accounts = await repositorieAccounts.getAccounts();
            return View(accounts);
        }
        public IActionResult Create()
        {
            return View();
        }

        // Realizamos el Create
        [HttpPost]
        public async Task<IActionResult> Create(Accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return View(accounts);
            }

            // Validamos si el nombre ya existe antes de hacer el registro
            var accountExist =
               await repositorieAccounts.Exist(accounts.Name);

            if (accountExist)
            {
                ModelState.AddModelError(nameof(accounts.Name),
                    $"The account {accounts.Name} already exist.");

                return View(accounts);
            }
            await repositorieAccounts.Create(accounts);

            return RedirectToAction("Index");
        }

        // Realiza la validacion en el front
        [HttpGet]
        public async Task<IActionResult> VerificaryAccount(string Name)
        {
            var accountExist = await repositorieAccounts.Exist(Name);

            if (accountExist)
            {
                return Json($"The account {Name} already exist");
            }

            return Json(true);
        }

        // Realizamos el Modify (Actualizar), hace la peticion de los datos
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)
        {
            var account = await repositorieAccounts.getAccountsById(Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(account);
        }

        // Aqui hace la actualizacion guardando los datos ingresados
        [HttpPost]
        public async Task<ActionResult> Modify(Accounts accounts)
        {
            var account = await repositorieAccounts.getAccountsById(accounts.Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Modify(accounts);
            return RedirectToAction("Index");
        }

        // Realizamos el delete haciendo la peticion de los datos
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var account = await repositorieAccounts.getAccountsById(Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(account);
        }

        // Aqui se realiza la accion de eliminar los datos desde la BD
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int Id)
        {
            var account = await repositorieAccounts.getAccountsById(Id);

            if (account is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieAccounts.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
