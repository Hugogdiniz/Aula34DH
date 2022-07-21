using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusCode.Models;

namespace StatusCode.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private SistemaContext DbSistema = new SistemaContext();

        [HttpGet]
        public ActionResult<List<Usuario>> RequererTodos()
        {
            return Ok();
        }

        [HttpGet("{Id}")]
        public ActionResult<Usuario> RequererUmPelaId(int Id)
        {
            var Resultado = DbSistema.Usuario.Find(Id);

            if (Resultado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        public ActionResult<Usuario> PublicarUm(Usuario Usuario)
        {

            foreach (Usuario usuario in DbSistema.Usuario)
            {
                if (usuario.Cpf != null)
                {
                    return Conflict();
                }
            }

                return Ok(DbSistema.Usuario.Add(Usuario));
           

            DbSistema.SaveChanges();
        }

        [HttpDelete("{Id}")]
        public ActionResult<Usuario> DeletarUmPelaId(int Id, Usuario Usuario)
        {
            var Resultado = DbSistema.Usuario.Find(Id);

            if (Resultado != null)
            {
                return NotFound();
            }
            else
            {
                return Unauthorized();
            }

            DbSistema.SaveChanges();
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> SubstituirUmPelaId(int Id, Usuario Usuario)
        {
            var Resultado = DbSistema.Usuario.Find(Id);
            var ResultadoCPF = DbSistema.Usuario.Find(Usuario.Cpf);

            if (Id != Usuario.Id)
            {
                return BadRequest();
            } else if(Resultado == null) {
                return NotFound();
            }
            else if (ResultadoCPF != null)
            {
                return Conflict();
            }
            else
            {
                return Ok(DbSistema.Entry(Usuario).State = EntityState.Modified);
            }

            DbSistema.SaveChanges();


        }
    }
}

/*
         - [ ] Ok(object? response)
              Status Code 200

        - [ ] CreatedResult(object? response)
              Status Code 201

        - [ ] NoContent()
	         Status Code 204

        - [ ] LocalRedirect(string localUrl)
              Status Code 302

        - [ ] BadRequest()
              Status Code 400

        - [ ] Unauthorized(object? response)
              Status Code 401

        - [ ] Forbid()
              Status Code 403

        - [ ] NotFound()
              Status Code 404

        - [ ] Conflict()
              Status 409

        - [ ] StatusCode(Int32)

         
         */