<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormBooking.aspx.cs" Inherits="CarFactoryWebView.FormBooking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Клиент&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownListConsumer" runat="server" Height="16px" Width="285px" DataSourceID="ObjectDataSource1" DataTextField="ConsumerName" DataValueField="Id">
        </asp:DropDownList>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList" TypeName="CarFactoryService.WorkDB.ConsumerServiceDB"></asp:ObjectDataSource>
        <br />
        <br />
        Изделие &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:DropDownList ID="DropDownListCommodity" runat="server" Height="16px" Width="285px" DataSourceID="ObjectDataSource2" DataTextField="CommodityName" DataValueField="Id">
        </asp:DropDownList>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetList" TypeName="CarFactoryService.WorkDB.CommodityServiceDB"></asp:ObjectDataSource>
        <br />
        <br />
        Количество&nbsp;&nbsp; <asp:TextBox ID="TextBoxCount" runat="server" OnTextChanged="TextBoxCount_TextChanged" Width="274px" AutoPostBack="True"></asp:TextBox>
        <br />
        <br />
        Сумма&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBoxSum" runat="server" Enabled="False" Width="274px"></asp:TextBox>
        <br />
        <asp:Button ID="ButtonSave" runat="server" Text="Сохранить" OnClick="ButtonSave_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="ButtonCancel" runat="server" Text="Отмена" OnClick="ButtonCancel_Click" />
    
    </div>
    </form>
</body>
</html>
