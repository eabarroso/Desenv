using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;

namespace TesteK2.Classes
{
    public class DadosTarefa
    {
        public int cod_tarefa { get; set; }
        public string des_titulo { get; set; }
        public string des_descricao { get; set; }
        public DateTime dat_execucao { get; set; }
    }

    public class Tarefa
    {
        //Overloads
        public List<DadosTarefa> ListarTarefas(int pCodUsuario)
        {
            return ListarTarefas(pCodUsuario, "", "");
        }
        public List<DadosTarefa> ListarTarefas(int pCodUsuario, string pTitulo)
        {
            return ListarTarefas(pCodUsuario, pTitulo, "");
        }

        //Listar as tarefas de acordos com os parametros
        public List<DadosTarefa> ListarTarefas(int pCodUsuario, string pTitulo, string pDescricao)
        {
            string strSQL = string.Empty;
            DBConnect objConn = new DBConnect();
            SqlCeDataReader objR;
            List<DadosTarefa> lstAux =  new List<DadosTarefa>();
            
            strSQL = "SELECT COUNT(*) AS QTD FROM tbTarefa WHERE cod_usuario = @CodUsuario";
            strSQL = strSQL.Replace("@CodUsuario", pCodUsuario.ToString());

            if (pTitulo != "")
            {
                strSQL += " AND des_titulo like '%@Titulo%'";
                strSQL = strSQL.Replace("@Titulo", pTitulo);
            }

            if (pDescricao != "")
            {
                strSQL += " AND des_descricao like '%@Descricao%'";
                strSQL = strSQL.Replace("@Descricao", pDescricao);
            }

            //Verificando se existe registros que atende a condição
            objR = objConn.ExecutarQuery(strSQL);
            objR.Read();

            if ((int)objR["QTD"]> 0)
            {
                strSQL = "SELECT TOP 5 * FROM tbTarefa WHERE cod_usuario = @CodUsuario";
                strSQL = strSQL.Replace("@CodUsuario", pCodUsuario.ToString());

                if (pTitulo != "")
                {
                    strSQL += " AND des_titulo like '%@Titulo%'";
                    strSQL = strSQL.Replace("@Titulo", pTitulo);
                }

                if (pDescricao != "")
                {
                    strSQL += " AND des_descricao like '%@Descricao%'";
                    strSQL = strSQL.Replace("@Descricao", pDescricao);
                }

                strSQL += " ORDER BY dat_execucao";

                objR = objConn.ExecutarQuery(strSQL);

                //Recuperando registros que atendem a condicao
                while (objR.Read())
                {
                    DadosTarefa item = new DadosTarefa();

                    item.cod_tarefa = (int)objR["cod_tarefa"];
                    item.des_titulo = (string)objR["des_titulo"];
                    item.des_descricao = (string)objR["des_descricao"];
                    item.dat_execucao = (DateTime)objR["dat_execucao"];

                    //Carregando lista com os registros encontrados
                    lstAux.Add(item);
                }

            }

            return lstAux;
        }

        //Exclui uma tarefa do banco de dados
        public void ExcluirTarefa(int pCodTarefa)
        {
            string strSQL = string.Empty;
            DBConnect objConn = new DBConnect();
            int ret;

            strSQL = "DELETE FROM tbTarefa WHERE cod_tarefa = @CodTarefa";
            strSQL = strSQL.Replace("@CodTarefa", pCodTarefa.ToString());

            ret = objConn.ExecutarComando(strSQL); 

        }

        //Altera dados de uma tarefa
        public void AlterarTarefa(int pCodTarefa, string pTitulo, string pDescricao, DateTime pDataExecucao)
        {
            string strSQL = string.Empty;
            DBConnect objConn = new DBConnect();
            int ret;

            strSQL = "UPDATE tbTarefa SET des_titulo = '@titulo'";
            strSQL += " ,des_descricao = '@descricao'";
            strSQL += " ,dat_execucao = '@data'";
            strSQL += " WHERE cod_tarefa = @codTarefa";

            strSQL = strSQL.Replace("@titulo", pTitulo);
            strSQL = strSQL.Replace("@descricao", pDescricao);
            strSQL = strSQL.Replace("@data", pDataExecucao.ToString("yyyy-MM-dd"));
            strSQL = strSQL.Replace("@codTarefa", pCodTarefa.ToString());

            ret = objConn.ExecutarComando(strSQL); 
        }

        //Inclui uma tarefa no banco de dados
        public void IncluirTarefa(string pTitulo, string pDescricao, DateTime pDataExecucao, int pCodUsuario)
        {
            string strSQL = string.Empty;
            DBConnect objConn = new DBConnect();
            int ret;

            strSQL = "INSERT INTO tbTarefa(des_titulo, des_descricao, dat_execucao, cod_usuario)";
            strSQL += " VALUES ('@titulo','@descricao','@data', @CodUsu)";

            strSQL = strSQL.Replace("@titulo", pTitulo);
            strSQL = strSQL.Replace("@descricao", pDescricao);
            strSQL = strSQL.Replace("@data", pDataExecucao.ToString("yyyy-MM-dd"));
            strSQL = strSQL.Replace("@CodUsu", pCodUsuario.ToString());

            ret = objConn.ExecutarComando(strSQL);
        }

    }
}