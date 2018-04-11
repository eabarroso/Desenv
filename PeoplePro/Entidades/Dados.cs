using System.Collections.Generic;
using PeoplePro.Entidades.Geral;
using System.Linq;

namespace PeoplePro.Entidades
{
    class Dados : Funcoes 
    {
        //Lista de registros cadastrados (Base de dados)
        public List<InfoDados> lstAmigos = new List<InfoDados>();

        //Listar os 3 registros de coordenadas mais próximas do registro solicitado
        public List<InfoDados> ListarProximos(string pNome)
        {
            List<InfoDados> lstRet = new List<InfoDados>();
            InfoDados[] MaisProx = new InfoDados[3];
            InfoDados PontoRef;

            //Recuperando o ponto de referencia inicial
            PontoRef = lstAmigos.Find(x => x.Nome == pNome);
            
            foreach (InfoDados Reg in lstAmigos)
            {
                //Verificando se não é o próprio ponto de referencia
                if (Reg.Nome != PontoRef.Nome)
                {
                    //Calculando a distancia de todos os pontos cadastrados em relação ao ponto de referencia
                    Reg.Distancia = CalcDistancia(PontoRef.Latitude, PontoRef.Longitude, Reg.Latitude, Reg.Longitude);
                }
            }

            //Varrendo Lista e armazenando os mais proximos em relação a distancia do ponto de referencia(Ordenada decrecente)
            foreach (InfoDados item in lstAmigos.OrderByDescending(i => i.Distancia))
            {
                //Verificando se é diferente do ponto de referencia(para nao contar como ponto proximo o proprio ponto de referencia)
                if (item.Nome != PontoRef.Nome)
                {
                    //Calculando a distancia do ponto em relacao ao ponto de referencia
                    //item.Distancia = CalcDistancia(PontoRef.Latitude, PontoRef.Longitude, item.Latitude, item.Longitude);

                    //Controlando a lista dos mais proximos atraves de um array first-in -> first-out
                    if (MaisProx[0] == null)
                    {
                        MaisProx[0] = item;
                    }
                    else
                    {
                        if (MaisProx[0].Distancia > item.Distancia)
                        {
                            MaisProx[2] = MaisProx[1];
                            MaisProx[1] = MaisProx[0];
                            MaisProx[0] = item;
                        }
                    }
                }
            }

            //Inserindo os 3 mais proximos na lista de retorno
            for(int I=0; I<=2; I++)
            {
                lstRet.Add(MaisProx[I]);
            }
            
            return lstRet;
        }

        //Verifica se já existe algum registro nas coordenadas passadas
        public bool VerificaCoordenadas(double pLat, double pLon)
        {
            bool ret = false;

            //Varrendo todos os registro para verificar se já existe algum registro na mesma coordenada
            foreach (InfoDados Reg in lstAmigos)
            {
                if (Reg.Latitude == pLat && Reg.Longitude == pLon)
                {
                    ret = true;
                }
            }

            return ret;

        }

    }

    class InfoDados
    {
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distancia { get; set; } //Propriedade usada apenas na busca por proximidade
    }
}
