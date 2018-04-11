using System;
using System.Collections.Generic;
using PeoplePro.Entidades;

namespace PeoplePro
{
    class Program
    {
        static Dados objDados = new Dados();

        //Inserir um novo registro na lista de amigos
        private static void InserirAmigo()
        {
            double ValAux;
            string temp;
            bool result = false;

            InfoDados NovoReg = new InfoDados();

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Novo Amigo");

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Insira o Nome:");
            NovoReg.Nome = Console.ReadLine();

            Console.Write("Insira a Latitude: ");

            //Solicitando Latitude enquanto não inserir dados corretamente
            while (result == false)
            {
                temp = Console.ReadLine();
                result = double.TryParse(temp, out ValAux);
                if (result == false)
                {
                    Console.Write("Insira a Latitude (Valor numérico ex: 10.50): ");
                }
                else
                {
                    NovoReg.Latitude = ValAux;
                }
            }

            result = false;
            Console.Write("Insira a longitude:");

            //Solicitando Longitude enquanto não inserir dados corretamente
            while (result == false)
            {
                temp = Console.ReadLine();
                result = double.TryParse(temp, out ValAux);
                if (result == false)
                {
                    Console.Write("Insira a longitude (Valor numérico ex: 10.50): ");
                }
                else
                {
                    NovoReg.Longitude = ValAux;
                }
            }

            //Verificando se ja existe algum registro nas mesmas coordenadas
            if (objDados.VerificaCoordenadas(NovoReg.Latitude, NovoReg.Longitude) == false)
            {
                objDados.lstAmigos.Add(NovoReg);
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Registro inserido com sucesso.");
            }
            else
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um registro com essas coordenadas.");
            }
           
            Console.Read();

        }

        //Lista para cada registro cadastrado os 3 registros de coordenadas mais próximas
        private static void ListarAmigosProximos()
        {
            List<InfoDados> lstProx;

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Listar Amigos Próximos");

            Console.ForegroundColor = ConsoleColor.White;

            //Varrendo todos os registros da base
            foreach (InfoDados Item in objDados.lstAmigos)
            {
                Console.WriteLine("");
                Console.WriteLine("Os 3 mais perto de " + Item.Nome + " são:");

                //Recuperando lista de registros mais próximos
                lstProx = objDados.ListarProximos(Item.Nome);

                foreach (InfoDados AProx in lstProx)
                {
                    if (AProx !=null)
                    {
                        Console.WriteLine(" -" + AProx.Nome + " á " +  AProx.Distancia.ToString("0.##") + " km");
                    }
                }
            }

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            string Opcao;

            #region Opção de pré-cadastro

            /* ===============================================================
             Pré Cadastro (dados para teste)
            ===================================================================*/
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Cadastro de Amigos");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Carregar Pré cadastro para testes ? (S/N):");
            Opcao = Console.ReadLine();

            //Perguntando ao usuário se deseja ter alguns dados iniciais para testes
            if (Opcao.ToLower() == "s")
            {
                InfoDados item1 = new InfoDados();
                item1.Nome = "André";
                item1.Latitude = -22.44;
                item1.Longitude = -44.22;

                objDados.lstAmigos.Add(item1);

                item1 = new InfoDados();
                item1.Nome = "Beatriz";
                item1.Latitude = 53.12;
                item1.Longitude = -32.60;
                objDados.lstAmigos.Add(item1);

                item1 = new InfoDados();
                item1.Nome = "Carlos";
                item1.Latitude = 4.00;
                item1.Longitude = 23.75;
                objDados.lstAmigos.Add(item1);

                item1 = new InfoDados();
                item1.Nome = "Denis";
                item1.Latitude = 35.14;
                item1.Longitude = -46.75;
                objDados.lstAmigos.Add(item1);

                item1 = new InfoDados();
                item1.Nome = "Eric";
                item1.Latitude = -60.14;
                item1.Longitude = -46.75;
                objDados.lstAmigos.Add(item1);

                item1 = new InfoDados();
                item1.Nome = "Fabio";
                item1.Latitude = -1.14;
                item1.Longitude = -3.40;
                objDados.lstAmigos.Add(item1);
            }
            Console.Clear();

            #endregion

            #region Menu do sistema

            //Apresenta menu ao usuario e só aceita as funções de opção oferecidas
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Cadastro de Amigos");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Menu:");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[ I ] = Inserir Novo Amigo");
                Console.WriteLine("[ L ] = Listar Amigos Próximos");
                Console.WriteLine("[ S ] = Sair");
                Console.WriteLine("");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Escolha uma opção: ");
                Opcao = Console.ReadLine();
                
                switch (Opcao.ToLower())
                {
                    case "i":
                        InserirAmigo();
                        break;

                    case "l":
                        ListarAmigosProximos();
                        break;

                    default:
                        break;
                }

                Console.Clear();

            }
            while (Opcao.ToLower() != "s");

            #endregion

        }
    }
}
