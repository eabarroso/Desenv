using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TesteK2.Classes;

namespace TesteK2
{
    public partial class tarefas : System.Web.UI.Page
    {
        //Carrega alista de tarefas do Usuario
        void CarregarTarefas()
        {
            int TotCols = 5;
            string dataAux = string.Empty;

            //Caregando lista de tarefas
            Tarefa objTarefas = new Tarefa();
            List<DadosTarefa> lstTarefas = new List<DadosTarefa>();

            try
            {
                lstTarefas = objTarefas.ListarTarefas((int)Session["cod_usuario"], txtTitulo.Text, txtDescricao.Text);
            }
            catch (Exception ex)
            {
                Session["Erro_Sistema"] = ex.Message;
                Response.Redirect("Error.aspx");
            }

            objTarefas = null;

            if (lstTarefas.Count > 0)
            {

                tbTarefas.Rows.Clear();

                //Criando linha de cabecalho
                TableRow tRow = new TableRow();
                tbTarefas.Rows.Add(tRow);

                for (int col = 1; col <= TotCols; col++)
                {
                    TableCell tCell = new TableCell();

                    tCell.BorderStyle = BorderStyle.Solid;
                    tCell.BorderWidth = 1;
                    tCell.BackColor = System.Drawing.Color.Gray;
                    tCell.ForeColor = System.Drawing.Color.White;
                    tCell.HorizontalAlign = HorizontalAlign.Center;
                    tCell.CssClass = "textoPadrao";

                    switch (col)
                    {
                        case 1:
                            tCell.Text = "Título";
                            break;
                        case 2:
                            tCell.Text = "Descrição";
                            break;
                        case 3:
                            tCell.Text = "Data";
                            break;
                        case 4:
                            tCell.Text = "";
                            break;
                        case 5:
                            tCell.Text = "";
                            break;
                        default:
                            break;
                    }

                    tRow.Cells.Add(tCell);
                }

                foreach (DadosTarefa item in lstTarefas)
                {
                    //Criando linha de cabecalho
                    TableRow Linha = new TableRow();
                    tbTarefas.Rows.Add(Linha);
                    dataAux = item.dat_execucao.ToString("dd/MM/yyyy");

                    for (int col = 1; col <= TotCols; col++)
                    {
                        TableCell tCell = new TableCell();

                        tCell.BorderStyle = BorderStyle.Solid;
                        tCell.BorderWidth = 1;
                        tCell.BackColor = System.Drawing.Color.White;
                        tCell.ForeColor = System.Drawing.Color.Black;
                        tCell.CssClass = "textoPadrao";

                        switch (col)
                        {
                            case 1:
                                tCell.Text = item.des_titulo;
                                break;
                            case 2:
                                tCell.Text = item.des_descricao;
                                break;
                            case 3:
                                tCell.Text = dataAux;
                                break;
                            case 4:
                                ImageButton btnImg = new ImageButton();
                                btnImg.ImageUrl = "images\\editar.png";
                                btnImg.Width = 20;
                                btnImg.Height = 20;
                                btnImg.ToolTip = "Alterar Tarefa";
                                btnImg.ID = "btnImgA" + item.cod_tarefa.ToString();
                                btnImg.CommandArgument = "A|" + item.cod_tarefa.ToString() + "|" + item.des_titulo + "|" + item.des_descricao + "|" + dataAux;
                                btnImg.Click += new ImageClickEventHandler(ImageButton_Click);
                                btnImg.CausesValidation = false;
                                tCell.Controls.Add(btnImg);
                                break;
                            case 5:
                                ImageButton btnImg2 = new ImageButton();
                                btnImg2.ImageUrl = "images\\excluir.png";
                                btnImg2.Width = 20;
                                btnImg2.Height = 20;
                                btnImg2.ToolTip = "Excluir Tarefa";
                                btnImg2.ID = "btnImgE" + item.cod_tarefa.ToString();
                                btnImg2.CommandArgument = "E|" + item.cod_tarefa.ToString() + "|" + item.des_titulo + "|" + item.des_descricao + "|" + dataAux;
                                btnImg2.OnClientClick = "return (confirm('Confirma exclusão?'));";
                                btnImg2.Click += new ImageClickEventHandler(ImageButton_Click);
                                btnImg2.CausesValidation = false;
                                tCell.Controls.Add(btnImg2);
                                break;
                            default:
                                break;
                        }

                        Linha.Cells.Add(tCell);
                    }
                }
            }
        }

        //Limpa os campos da Tela
        void LimparCampos()
        {
            hdnCodTarefa.Value = "";
            txtTitulo.Text = "";
            txtDescricao.Text = "";
            txtData.Text = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cod_usuario"].ToString() == "")
            {
                Response.Redirect("login.aspx");
            }

            CarregarTarefas();    
        }

        //Evento dos botoes da Grid
        void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)(sender);
            string cod_tarefa = btn.CommandArgument;

            string[] Dados = btn.CommandArgument.Split('|');

            //Verificando se é uma exclusão
            if (Dados[0] == "E")
            {
                Tarefa objT = new Tarefa();

                try
                {
                    objT.ExcluirTarefa(int.Parse(Dados[1]));
                }
                catch (Exception ex)
                {
                    Session["Erro_Sistema"] = ex.Message;
                    Response.Redirect("Error.aspx");
                }

                //Fechando objetos
                objT = null;

                LimparCampos();

                CarregarTarefas();
            }
            else
            {
                hdnCodTarefa.Value = Dados[1];
                txtTitulo.Text = Dados[2];
                txtDescricao.Text = Dados[3];
                txtData.Text = Dados[4];
            }

        }

        //Apenas reprocessa a lista com os valores dos campos da tela
        protected void imgbPesq_Click(object sender, ImageClickEventArgs e)
        {
            CarregarTarefas();
        }

        //Inclusão de novo registro
        protected void imgbSalvar_Click(object sender, ImageClickEventArgs e)
        {
            Tarefa objT = new Tarefa();

            string DesTitulo = txtTitulo.Text;
            string DesDescricao = txtDescricao.Text;
            DateTime DatExecucao = DateTime.Parse(txtData.Text);

            //Verificando se é uma alteração se o campo de codigo estiver preenchido
            if (hdnCodTarefa.Value != "")
            {
                int CodTarefa = int.Parse(hdnCodTarefa.Value);

                try
                {
                    objT.AlterarTarefa(CodTarefa, DesTitulo, DesDescricao, DatExecucao);
                }
                catch (Exception ex)
                {
                    Session["Erro_Sistema"] = ex.Message;
                    Response.Redirect("Error.aspx");
                }
                
            }
            else
            {
                try
                {
                    objT.IncluirTarefa(DesTitulo, DesDescricao, DatExecucao, (int)Session["cod_usuario"]);
                }
                catch (Exception ex)
                {
                    Session["Erro_Sistema"] = ex.Message;
                    Response.Redirect("Error.aspx");
                }
            }

            //Fechando objetos
            objT = null;

            LimparCampos();

            CarregarTarefas();
        }

        //Limpando campos da tela e recarregando a lista
        protected void imgbLimpar_Click(object sender, ImageClickEventArgs e)
        {
            LimparCampos();

            CarregarTarefas();
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Session["cod_usuario"] = "";
            Response.Redirect("login.aspx");
        }
    }
}