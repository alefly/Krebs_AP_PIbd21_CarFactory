<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormMain.aspx.cs" Inherits="CarFactoryWebView.FormMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 666px;
            width: 1067px;
        }
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Menu ID="Menu" runat="server" BackColor="White" ForeColor="Black" Height="150px">
            <Items>
                <asp:MenuItem Text="Справочники" Value="Справочники">
                    <asp:MenuItem Text="Клиенты" Value="Клиенты" NavigateUrl="~/FormConsumers.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Компоненты" Value="Компоненты" NavigateUrl="~/FormIngridients.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Услуги" Value="Услуги" NavigateUrl="~/FormCommodities.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Склады" Value="Склады" NavigateUrl="~/FormStorages.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Сотрудники" Value="Сотрудники" NavigateUrl="~/FormWorkers.aspx"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Пополнить склад" Value="Пополнить склад" NavigateUrl="~/FormPutOnStorage.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Отчеты" Value="Отчеты">
                    <asp:MenuItem NavigateUrl="~/FormPrice.aspx" Text="Прайс изделий" Value="Прайс изделий"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/FormStoragesLoad.aspx" Text="Загруженность складов" Value="Загруженность складов"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/FormConsumerBookings.aspx" Text="Заказы клиентов" Value="Заказы клиентов"></asp:MenuItem>
                </asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:Button ID="ButtonCreateBooking" runat="server" Text="Создать заказ" OnClick="ButtonCreateBooking_Click" />
        <asp:Button ID="ButtonTakeBookingInWork" runat="server" Text="Отдать на выполнение" OnClick="ButtonTakeBookingInWork_Click" />
        <asp:Button ID="ButtonBookingReady" runat="server" Text="Заказ готов" OnClick="ButtonBookingReady_Click" />
        <asp:Button ID="ButtonBookingPayed" runat="server" Text="Заказ оплачен" OnClick="ButtonBookingPayed_Click" />
        <asp:Button ID="ButtonUpd" runat="server" Text="Обновить список" OnClick="ButtonUpd_Click" />
        <asp:GridView ID="dataGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="ConsumerName" HeaderText="ConsumerName" SortExpression="ConsumerName" />
                <asp:BoundField DataField="CommodityName" HeaderText="CommodityName" SortExpression="CommodityName" />
                <asp:BoundField DataField="WorkerName" HeaderText="WorkerName" SortExpression="WorkerName" />
                <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" />
                <asp:BoundField DataField="Sum" HeaderText="Sum" SortExpression="Sum" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="DateCreate" HeaderText="DateCreate" SortExpression="DateCreate" />
                <asp:BoundField DataField="DateImplement" HeaderText="DateImplement" SortExpression="DateImplement" />
            </Columns>
            <SelectedRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="PayBooking" SelectMethod="GetList" TypeName="CarFactoryService.WorkDB.MainServiceDB">
            <DeleteParameters>
                <asp:Parameter Name="id" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
