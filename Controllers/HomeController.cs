using System.Reflection.Metadata.Ecma335;
using CRUDStoredProcedures.Data;
using Microsoft.AspNetCore.Mvc;

namespace CRUDStoredProcedures.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataAcess _dataAcess;
        public HomeController(DataAcess dataAcess)
        {
            _dataAcess = dataAcess;
        }

        public IActionResult Index()
        {
            try
            { 
                var usuarios = _dataAcess.ListarUsuarios();
                return View(usuarios);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao listar os usu�rios";
                return View();
            }
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
    }
}
