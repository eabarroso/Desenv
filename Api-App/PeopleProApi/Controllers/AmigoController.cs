using PeopleProApi.Models.Amigos;
using PeopleProApi.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace PeopleProApi.Controllers
{
    [RoutePrefix("api/Amigo")]
    public class AmigoController : ApiController
    {
        static readonly IAmigo Repositorio = new AmigoRepository();

        [Authorize]
        public IEnumerable<AmigoProx> GetTodos()
        {
            return Repositorio.ListarTodos();
        }

        [Authorize]
        [Route("Put")]
        public IHttpActionResult Put(Amigo NovoAmigo)
        {
            Repositorio.Inserir(NovoAmigo);

            return Ok();
        }
    }
}
