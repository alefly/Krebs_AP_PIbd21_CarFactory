<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormStoragesLoad.aspx.cs" Inherits="CarFactoryWebView.FormStoragesLoad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="ButtonSaveExcel" runat="server" OnClick="ButtonSaveExcel_Click" Text="Сохранить в Excel" />
        <br />
        <asp:Table ID="Table" runat="server" BorderColor="Black" BorderStyle="Solid" GridLines="Both">
        </asp:Table>
        <br />
        <asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click" Text="Назад" />
    
    </div>
    </form>
</body>
</html>
