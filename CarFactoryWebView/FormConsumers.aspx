<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormConsumers.aspx.cs" Inherits="CarFactoryWebView.FormConsumers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="ButtonAdd" runat="server" OnClick="ButtonAdd_Click" Text="Добавить" />
        <asp:Button ID="ButtonChange" runat="server" OnClick="ButtonChange_Click" Text="Изменить" />
        <asp:Button ID="ButtonDelete" runat="server" OnClick="ButtonDelete_Click" Text="Удалить" />
        <asp:Button ID="ButtonUpd" runat="server" Text="Обновить" OnClick="ButtonUpd_Click" />
        <asp:GridView ID="dataGridView" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="ConsumerName" HeaderText="ConsumerName" SortExpression="ConsumerName" />
            </Columns>
            <SelectedRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
    
        <br />
        <br />
        <asp:Button ID="ButtonBack" runat="server" Text="Вернуться" OnClick="ButtonBack_Click" />
    
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList" TypeName="CarFactoryService.WorkDB.ConsumerServiceDB"></asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
