<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tarefas.aspx.cs" Inherits="TesteK2.tarefas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="site.css" rel="stylesheet" />
</head>
<body>
    <form id="frm" runat="server">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td class="textoPadrao">Título</td>
                <td class="textoPadrao">Descrição</td>
                <td class="textoPadrao">Data execução</td>
                <td class="textoPadrao"></td>
                <td class="textoPadrao"></td>
                <td class="textoPadrao"></td>
                <td class="textoPadrao"></td>
            </tr>
            <tr>
                <td class="textoPadrao">
                    <asp:TextBox ID="txtTitulo" runat="server" MaxLength="50" Width="150" CssClass="campoPadrao"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvValidaTitulo2" runat="server" ErrorMessage="O título é obrigatório." ControlToValidate="txtTitulo" ValidationGroup="Inclusao">*</asp:RequiredFieldValidator>
                </td>
                <td class="textoPadrao">
                    <asp:TextBox ID="txtDescricao" runat="server" MaxLength="100" Width="350" CssClass="campoPadrao"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvValidaDescricao2" runat="server" ErrorMessage="A descrição é obrigatória." ControlToValidate="txtDescricao" ValidationGroup="Inclusao">*</asp:RequiredFieldValidator>
                </td>
                <td class="textoPadrao">
                    <asp:TextBox ID="txtData" runat="server" CssClass="campoPadrao"  MaxLength="10" Width="80"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Data inválida" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ControlToValidate="txtData" ValidationGroup="Inclusao" Text="*"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvValidaData2" runat="server" ErrorMessage="A data de execução é obrigatória." ControlToValidate="txtData" ValidationGroup="Inclusao">*</asp:RequiredFieldValidator>
                </td>
                <td class="textoPadrao"><asp:ImageButton ID="imgbLimpar" runat="server" Width="20" Height="20" ImageUrl="~/Images/limpar.png" ToolTip="Limpar campos" OnClick="imgbLimpar_Click" CausesValidation="False" /></td>
                <td class="textoPadrao"><asp:ImageButton ID="imgbPesq" runat="server" Width="20" Height="20" ImageUrl="~/Images/lupa.png" ToolTip="Pesquisar Tarefa" OnClick="imgbPesq_Click" CausesValidation="True"/></td>
                <td class="textoPadrao"><asp:ImageButton ID="imgbSalvar" runat="server" Width="20" Height="20" ImageUrl="~/Images/Salvar.png" ToolTip="Salvar Tarefa" OnClick="imgbSalvar_Click" ValidationGroup="Inclusao" /></td>
                <td class="textoPadrao"><asp:HiddenField ID="hdnCodTarefa" runat="server" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="vsErrors2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Inclusao" />
        <br />
        <asp:Table ID="tbTarefas" runat="server" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px"></asp:Table>
        <p class="textoPadrao">* Exibindo apenas os 5 primeiros compromissos</p>
        <br />
        <br />
        <asp:Button ID="btnSair" runat="server" Text="Sair" OnClick="btnSair_Click" CssClass="botaoPadrao" />
    </form>
</body>
</html>
