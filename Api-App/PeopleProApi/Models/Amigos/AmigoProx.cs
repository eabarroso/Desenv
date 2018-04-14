using System.Collections.Generic;

namespace PeopleProApi.Models.Amigos
{
    public class AmigoProx
    {
        public Amigo PontoRef { get; set; }
        public List<Amigo> lstProx { get; set; }
    }
}