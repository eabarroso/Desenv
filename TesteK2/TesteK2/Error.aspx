<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TesteK2.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="1" cellspacing="1">
            <tr>
                <td><img src="Images/Excluir.png" width="30" height="30"/></td>
                <td>Erro Inexperado</td>
            </tr>
            <tr>
                <td colspan="2" class="textoPadrao"><%=Session["Erro_Sistema"]%></td>
            </tr>
        </table>
    </form>
</body>
</html>
