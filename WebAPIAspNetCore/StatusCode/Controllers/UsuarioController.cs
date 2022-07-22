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
            return Ok(DbSistema.Usuario.ToList());
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
                return Ok(DbSistema.Usuario.Where(Usuario => Usuario.Id == Id));
            }
        }

        [HttpPost]
        public ActionResult<Usuario> PublicarUm(Usuario Usuario)
        {
            Usuario novoUsuario = new Usuario
            {
                Id = Usuario.Id,
                Cpf = Usuario.Cpf,
                Nome = Usuario.Nome,
                Sobrenome = Usuario.Sobrenome
            };

            foreach (Usuario usuario in DbSistema.Usuario.ToList())
            {
                if (usuario.Cpf == novoUsuario.Cpf)
                {
                    return Conflict();
                }
            }
            DbSistema.Usuario.Add(Usuario);
            DbSistema.SaveChanges();
            return Ok(Usuario);
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
            Usuario atualizarUsuario = new Usuario
            {
                Cpf = Usuario.Cpf,
                Nome = Usuario.Nome,
                Sobrenome = Usuario.Sobrenome
            };

            if (Resultado == null)
            {
                return NotFound();
            }
            else
            {
                foreach (Usuario usuario in DbSistema.Usuario.ToList())
                {
                    if (usuario.Cpf == atualizarUsuario.Cpf)
                    {
                        return Conflict();
                    }
                }
            }
            DbSistema.Usuario.Update(atualizarUsuario);
            DbSistema.SaveChanges();
            return Ok();
        }
        /*
        public ActionResult<Usuario> SubstituirUmPelaId(int Id, Usuario Usuario)
        {
            var Resultado = DbSistema.Usuario.Find(Id);

            Usuario novoUsuario = new Usuario
            {
                Cpf = Usuario.Cpf,
                Nome = Usuario.Nome,
                Sobrenome = Usuario.Sobrenome
            };

            if (Id != Usuario.Id)
            {
                return BadRequest();
            }
            else if (Resultado == null)
            {
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


        } */
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
