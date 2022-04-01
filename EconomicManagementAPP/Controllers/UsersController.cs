using EconomicManagementAPP.IRepositorie;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomicManagementAPP.Controllers
{
    public class UsersController: Controller
    {
        private readonly IRepositorieUsers repositorieUsers;

        public UsersController(IRepositorieUsers repositorieUsers)
        {
            this.repositorieUsers = repositorieUsers;
        }

        // Index
        public async Task<IActionResult> Index()        {
            
            var users = await repositorieUsers.getUser();
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> Create(Users users)
        {
            if (!ModelState.IsValid)
            {
                return View(users);
            }

            // Validamos si el usuario ya existe mediante el email, no enviamos parametros predefinidos, pero si el primer email que tome
            var usersExist =
               await repositorieUsers.Exist(users.Email);

            if (usersExist)
            {
                ModelState.AddModelError(nameof(users.Email),
                    $"The account {users.Email} already exist.");

                return View(users);
            }
            await repositorieUsers.Create(users);
            return RedirectToAction("Index");
        }

        // Validacion
        [HttpGet]
        public async Task<IActionResult> VerificaryUsers(string Email)
        {   
            var usersExist = await repositorieUsers.Exist(Email);

            if (usersExist)
            {
                return Json($"The account {Email} already exist.");
            }
            return Json(true);
        }

        // Actualizar
        [HttpGet]
        public async Task<ActionResult> Modify(int Id)        
        {
            var user = await repositorieUsers.getUserById(Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(user);
        }
        
        [HttpPost]
        public async Task<ActionResult> Modify(Users users)
        {           
            var user = await repositorieUsers.getUserById(users.Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(users);
            }

            await repositorieUsers.Modify(users);
            return RedirectToAction("Index");
        }
       
        
        // Eliminar
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {            
            var user = await repositorieUsers.getUserById(Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int Id)
        {          
            var user = await repositorieUsers.getUserById(Id);

            if (user is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorieUsers.Delete(Id);
            return RedirectToAction("Index");
        }

        // Login: Aqui va a buscar la interfaz del login
        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        //Aqui ingresa los datos a la BD con sus respectivas validaciones
        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var result = await repositorieUsers.Login(loginViewModel.Email, loginViewModel.Password);

            if(result is null)
            {
                ModelState.AddModelError(String.Empty, "Wrong Email or Password");
                return View(loginViewModel);
            }
            else
            {
                return RedirectToAction("Index", "AccountTypes");
            }

        }
    }
}
