<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormConsumerBookings.aspx.cs" Inherits="CarFactoryWebView.FormConsumerBookings" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        С<br />

            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
            По<br />
            <asp:Calendar ID="Calendar2" runat="server" style="margin-bottom: 0px"></asp:Calendar>

            <asp:Button ID="ButtonMake" runat="server" OnClick="ButtonMake_Click" Text="Сформировать" />
            <asp:Button ID="ButtonToPdf" runat="server" OnClick="ButtonToPdf_Click" Text="Сохранить в PDF" />
            <asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click" Text="Назад" Height="25px" />

        <br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" style="margin-top: 0px" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
            <LocalReport ReportPath="ReportConsumerBookings.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetConsumerBookings" TypeName="CarFactoryService.WorkDB.ReportServiceDB">
            <SelectParameters>
                <asp:Parameter DefaultValue="" Name="model" Type="Object"></asp:Parameter>
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        <br />
        <div style="position: absolute; top: 480px; left: 21px;">

        </div>
    </form>
</body>
</html>
