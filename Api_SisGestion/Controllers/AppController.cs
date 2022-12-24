using Api_SisGestion.Models;
using Api_SisGestion.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api_SisGestion.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppController : Controller
    {
        [HttpGet("data")] //Nombre de la organizacion
        public ActionResult<List<Data>> NameOrganization()
        {
            try
            {
                List<Data> lista = new();
                Data orgdata = new(DBConnect.Organization());
                lista.Add(orgdata);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return ValidationProblem(ex.Message);
            }
        }
    }
}
