using PeopleProApi.Models.Amigos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;

namespace PeopleProApi.Repositories
{
    public class AmigoRepository : IAmigo
    {
        private void Log(string pAcao, string pValor)
        {
            string strSQL;

            SqlConnection Db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoBanco"].ConnectionString);
            Db.Open();

            strSQL = "INSERT INTO CalculoHistoricoLog (ds_acao, vl_valor) VALUES ('@ds_acao', '@vl_valor')";
            strSQL = strSQL.Replace("@ds_acao", pAcao);
            strSQL = strSQL.Replace("@vl_valor", pValor);
            SqlCommand SQLcommand = new SqlCommand(strSQL, Db);

            try
            {
               SQLcommand.ExecuteNonQuery();
            }
            finally
            {
                SQLcommand = null;
                Db.Close();
                Db.Dispose();
            }
        }

        //Lista de amigos
        private List<Amigo> lstAmigos = new List<Amigo>();

        //Verifica se já existe algum registro nas coordenadas passadas
        private bool VerificaCoordenadas(double pLat, double pLon)
        {
            bool ret = false;

            //Varrendo todos os registro para verificar se já existe algum registro na mesma coordenada
            foreach (Amigo Reg in lstAmigos)
            {
                if (Reg.Latitude == pLat && Reg.Longitude == pLon)
                {
                    ret = true;
                }
            }

            Log("Verificando se já existe um usuario com as coordenadas pedidas", ret.ToString());

            return ret;
        }

        //Converte graus em radianos
        private double DegRad(double x)
        {
            double resp = x * ((4 * Math.Atan(1)) / 180);

            Log("Transformando em radianos",resp.ToString());

            return resp;
        }

        //Calcula a distancia em quilometros (1.00000) entre dois pontos atraves de latitude e longitude
        private double CalcDistancia(double Lat1, double Lon1, double Lat2, double Lon2)
        {
            double er = 0;
            double pr = 0;
            double fl = 0;
            double F = 0;
            double G = 0;
            double L = 0;
            double S = 0;
            double C = 0;
            double W = 0;
            double R = 0;
            double D = 0;
            double H1 = 0;
            double H2 = 0;

            er = 6378.1370000;
            pr = 6356.7523142;

            fl = (er - pr) / er;

            F = (DegRad(Lat1) + DegRad(Lat2)) / 2;

            G = (DegRad(Lat1) - DegRad(Lat2)) / 2;
            L = (DegRad(Lon1) - DegRad(Lon2)) / 2;

            S = (Math.Pow(Math.Sin(G), 2)) * Math.Pow(Math.Cos(L), 2) + Math.Pow(Math.Cos(F), 2) * Math.Pow(Math.Sin(L), 2);
            C = (Math.Pow(Math.Cos(G), 2)) * Math.Pow(Math.Cos(L), 2) + Math.Pow(Math.Sin(F), 2) * Math.Pow(Math.Sin(L), 2);

            W = Math.Atan(Math.Sqrt(S / C));

            R = Math.Sqrt(S * C) / W;

            D = 2 * W * er;

            H1 = (3 * R - 1) / (2 * C);
            H2 = (3 * R + 1) / (2 * S);

            double resp = (D * (1 + fl * H1 * Math.Pow(Math.Sin(F), 2) * Math.Pow(Math.Cos(G), 2) - fl * H2 * Math.Pow(Math.Cos(F), 2) * Math.Pow(Math.Sin(G), 2)) / 1.00000);

            Log("Calculando Distancia em relacao ao ponto de referencia", resp.ToString());

            return resp;
        }

        //Construtor
        public AmigoRepository()
        {
            lstAmigos.Add(new Amigo { Nome = "André", Latitude = -22.44, Longitude = -44.22 });
            lstAmigos.Add(new Amigo { Nome = "Beatriz", Latitude = 53.12, Longitude = -32.6 });
            lstAmigos.Add(new Amigo { Nome = "Carlos", Latitude = 4, Longitude = 23.75 });
            lstAmigos.Add(new Amigo { Nome = "Denis", Latitude = 35.14, Longitude = -46.75 });
            lstAmigos.Add(new Amigo { Nome = "Eric", Latitude = -60.14, Longitude = -46.75 });
            lstAmigos.Add(new Amigo { Nome = "Fábio", Latitude = -1.14, Longitude = -3.4 });
        }

        //Inserir Novo Amigo na lista
        public void Inserir(Amigo pAmigo)
        {
            //Verificar se ja existe algum registro com as coordenadas
            if (VerificaCoordenadas(pAmigo.Latitude, pAmigo.Longitude) == false)
            {
                lstAmigos.Add(pAmigo);
            }
            else
            {
                throw new Exception("Já existe um amigo com essas coordenadas.");
            }
        }

        //Lista os 3 amigos mais proximos do amigo passado
        public IEnumerable<Amigo> Listar(string pNome)
        {
            List<Amigo> lstRet = new List<Amigo>();
            Amigo[] MaisProx = new Amigo[3];
            Amigo PontoRef;

            //Recuperando o ponto de referencia inicial
            PontoRef = lstAmigos.Find(x => x.Nome == pNome);

            foreach (Amigo Reg in lstAmigos)
            {
                //Verificando se não é o próprio ponto de referencia
                if (Reg.Nome != PontoRef.Nome)
                {
                    //Calculando a distancia de todos os pontos cadastrados em relação ao ponto de referencia
                    Reg.Distancia = CalcDistancia(PontoRef.Latitude, PontoRef.Longitude, Reg.Latitude, Reg.Longitude);
                }
            }

            //Varrendo Lista e armazenando os mais proximos em relação a distancia do ponto de referencia(Ordenada decrecente)
            foreach (Amigo item in lstAmigos.OrderByDescending(i => i.Distancia))
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
            for (int I = 0; I <= 2; I++)
            {
                lstRet.Add(MaisProx[I]);
            }

            return lstRet;
        }

        public IEnumerable<AmigoProx> ListarTodos()
        {
            List<AmigoProx> lstAux = new List<AmigoProx>();

            foreach (Amigo Reg in lstAmigos)
            {
                AmigoProx NewProx = new AmigoProx();
                NewProx.PontoRef = Reg;
                NewProx.lstProx = Listar(Reg.Nome).ToList();

                lstAux.Add(NewProx);
            }

            return lstAux;
        }
    }
}