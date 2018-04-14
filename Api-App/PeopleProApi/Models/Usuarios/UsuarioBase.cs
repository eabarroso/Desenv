using System.Collections.Generic;

namespace PeopleProApi.Models.Usuarios
{
    public static class UsuarioBase
    {
        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>
            {
                new Usuario {Nome = "PeoplePro", Senha = "1234"}
            };
        }
    }
}