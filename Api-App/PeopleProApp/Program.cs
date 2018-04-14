using Newtonsoft.Json;
using PeopleProApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PeopleProApp
{
    class Program
    {
        static string UserName = "PeoplePro";
        static string Senha = "1234";
        static string baseUrl = "http://localhost:52397/";
        static string accessToken = null;

        private static void IncluirAmigo(Amigo pAmigo)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var json = JsonConvert.SerializeObject(pAmigo);

                HttpContent stringContent = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = client.PutAsync("api/amigo/put", stringContent).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
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
                }

            }
        }

        private static async Task<string> RecuperarToken()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
                postData.Add(new KeyValuePair<string, string>("userName", UserName));
                postData.Add(new KeyValuePair<string, string>("password", Senha));

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = await client.PostAsync("Token", content);
                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);
                accessToken = ((dynamic)responseData).access_token;
                return ((dynamic)responseData).access_token;
            }
        }

        private static async Task<dynamic> ListarAmigos()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                string url = string.Format("api/amigo");

                HttpResponseMessage response = await client.GetAsync(url);

                string jsonString = await response.Content.ReadAsStringAsync();

                List<AmigoProx> objAmigo = JsonConvert.DeserializeObject<List<AmigoProx>>(jsonString);

                return objAmigo;
            }
        }

        private static async Task<int> Listar()
        {
            //accessToken = await RecuperarToken();

            List<AmigoProx> lstAmigos = await ListarAmigos();

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Listar Amigos Próximos");

            Console.ForegroundColor = ConsoleColor.White;

            if (lstAmigos != null)
            {
                foreach (AmigoProx pAmigo in lstAmigos)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Os 3 mais perto de " + pAmigo.PontoRef.Nome + " são:");

                    foreach (Amigo AProx in pAmigo.lstProx)
                    {
                        if (AProx != null)
                        {
                            Console.WriteLine(" -" + AProx.Nome + " á " + AProx.Distancia.ToString("0.##") + " km");
                        }
                    }
                }

               Console.ReadLine();
            }

            return 0;
        }

        private static void Inserir()
        {
            double ValAux;
            string temp;
            bool result = false;

            Amigo NovoReg = new Amigo();

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

            IncluirAmigo(NovoReg);

            /*
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
            */
            Console.Read();

        }

        static void Main(string[] args)
        {
            string Opcao;

            RecuperarToken().Wait();

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
                        Inserir();
                        break;

                    case "l":
                        Listar().Wait();
                        break;

                    default:
                        break;
                }

                Console.Clear();

            }
            while (Opcao.ToLower() != "s");

        }

       
        

        

    }
}
