<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TesteK2.login" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="site.css" rel="stylesheet" />
</head>
<body>
    <form id="frm" runat="server">
    <input id="hdnMsgErro" name="hdnMsgErro" type="hidden" value="<%=MsgErro%>" />
    <table border="1" cellpadding="0" cellspacing="0" style="width:100%;height:100%">
        <tr>
            <td align="center">
                <table border="0" cellpadding="1" cellspacing="1" >
                    <tr>
                        <td align="right" class="textoPadrao">Usuário:</td>
                        <td><asp:TextBox ID="txtUsu" runat="server" MaxLength="10" Width="150px" CssClass="campoPadrao"></asp:TextBox><asp:RequiredFieldValidator ID="ValidaPreenchimento" runat="server" ErrorMessage="Usuário é obrigatório" Text="*" ControlToValidate="txtUsu" Display="Static"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" class="textoPadrao">Senha:</td>
                        <td><asp:TextBox ID="txtPass" runat="server" MaxLength="6" TextMode="Password" Width="150px" CssClass="campoPadrao"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Senha é obrigatória" Text="*" ControlToValidate="txtPass" Display="Static"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right"><asp:Button ID="cmdEntrar" runat="server" Text="Entrar" CssClass="botaoPadrao" OnClick="cmdEntrar_Click"/></td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True" />
            </td>
        </tr>
    </table>
    </form>
</body>
    <script language="javascript" type="text/javascript">

        //Verifica se houve alguma mensagem de erro após a tentativa de login
        if (frm.hdnMsgErro.value != "")
        {
            alert(frm.hdnMsgErro.value);
        }

    </script>
</html>
