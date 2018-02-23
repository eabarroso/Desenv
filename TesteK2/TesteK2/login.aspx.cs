using System;
using TesteK2.Classes;

namespace TesteK2
{
    public partial class login : System.Web.UI.Page
    {
        public string MsgErro = string.Empty;       //Mensagem de erro após tentativa de login

        protected void Page_Load(object sender, EventArgs e) { }

        protected void cmdEntrar_Click(object sender, EventArgs e)
        {
            Usuario objUsu = new Usuario();
            int pUsuario = -1;

            try
            {
                //Validando Login
                pUsuario = objUsu.ValidarLogin(txtUsu.Text, txtPass.Text);
            }
            catch (Exception ex)
            {
                Session["Erro_Sistema"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
            finally
            {
                //Fechando objetos
                objUsu = null;
            }

            //Caso encontrou um usuario
            if (pUsuario != -1)
            {
                //Armazeno o codigo do usuario na sessao
                Session["cod_usuario"] = pUsuario;
                Response.Redirect("tarefas.aspx");
            }
            else
            {
                //Preenchendo mensagem de erro para exibicao de alerta (javascript)
                MsgErro = "Usuário ou senha inválido.";
            }

        }
    }
}