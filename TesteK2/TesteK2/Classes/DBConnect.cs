using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlServerCe;
using System.Configuration;

namespace TesteK2.Classes
{
    public class DBConnect
    {
        private SqlCeConnection objConn;    //Conexao com o banco de dados 
        private string strConn = ConfigurationManager.ConnectionStrings["bancoSDF"].ToString().Trim(); //String de conexao do Web config

        //Construtor da classe (Abre a conexao com o banco)
        public DBConnect()
        {
            objConn = new SqlCeConnection(strConn);
            objConn.Open();
        }

        //Destructor (Fecha a conexao com o banco)
        ~DBConnect()
        {
            objConn.Close();
        }

        //Executa querys do tipo Select e devolve um Reader
        public SqlCeDataReader ExecutarQuery(string pSQL)
        {
            SqlCeDataReader objR;
            SqlCeCommand objCmd = new SqlCeCommand();

            objCmd.CommandText = pSQL;
            objCmd.Connection = objConn;
            
            objR = objCmd.ExecuteReader();
            
            objCmd.Dispose();
            
            return objR;

        }

        //Esecuta comando de acao na base de dados (INSERT, DELETE, UPDATE)
        public int ExecutarComando(string pSQL)
        {
            int ret;
            SqlCeCommand objCmd = new SqlCeCommand();

            objCmd.CommandText = pSQL;
            objCmd.Connection = objConn;

            ret = objCmd.ExecuteNonQuery();

            objCmd.Dispose();

            return ret;
        }
    }
}