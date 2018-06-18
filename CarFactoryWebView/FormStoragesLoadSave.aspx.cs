using System;
using Unity;
using CarFactoryService.Interfaces;
using CarFactoryService.BindingModels;

namespace CarFactoryWebView
{
    public partial class FormStoragesLoadSave : System.Web.UI.Page
    {
        readonly IReportService reportService = UnityConfig.Container.Resolve<IReportService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename=StoragesLoad.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            try
            {
                reportService.SaveStoragesLoad(new ReportBindingModel
                {
                    FileName = "D:\\SLoad.xls"
                });
                Response.WriteFile("D:\\SLoad.xls");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllert", "<script>alert('" + ex.Message + "');</script>");
            }
            Response.End();
        }
    }
}