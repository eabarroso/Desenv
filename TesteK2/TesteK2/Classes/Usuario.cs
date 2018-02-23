using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlServerCe;

namespace TesteK2.Classes
{
    public class Usuario
    {
        //Valida se o login passado é valido
        public int ValidarLogin(string pUsuario, string pSenha)
        {
            int Result= -1;
            DBConnect objConn = new DBConnect();
            SqlCeDataReader objR;
            string strSQL = string.Empty;

            strSQL = "SELECT COUNT(*) AS QTD FROM tbUsuario WHERE des_login = '@Usuario' AND des_senha = '@Senha'";
            strSQL = strSQL.Replace("@Usuario", pUsuario);
            strSQL = strSQL.Replace("@Senha", pSenha);

            objR = objConn.ExecutarQuery(strSQL);

            objR.Read();

            //Verificando se existe algum usuario com os parametros passado
            if ((int)objR["QTD"]> 0)
            {
                strSQL = "SELECT cod_usuario FROM tbUsuario WHERE des_login = '@Usuario' AND des_senha = '@Senha'";
                strSQL = strSQL.Replace("@Usuario", pUsuario);
                strSQL = strSQL.Replace("@Senha", pSenha);

                objR = objConn.ExecutarQuery(strSQL);
                objR.Read();

                //Devolvendo código do usuario
                Result = (int)objR["cod_usuario"];
            }

            objR.Close();
            objConn = null;

            return Result;
        }

    }
}