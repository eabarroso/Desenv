using PeopleProApi.Models.Amigos;
using System.Collections.Generic;

namespace PeopleProApi.Repositories
{
    public interface IAmigo
    {
        IEnumerable<Amigo> Listar(string pNome);
        IEnumerable<AmigoProx> ListarTodos();
        void Inserir(Amigo pAmigo);
    }
}
