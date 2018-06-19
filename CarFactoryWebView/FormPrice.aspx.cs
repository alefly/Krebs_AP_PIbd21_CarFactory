using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using System;
using Unity;

namespace CarFactoryWebView
{
    public partial class FormPrice : System.Web.UI.Page
    {
        readonly IReportService reportService = UnityConfig.Container.Resolve<IReportService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "filename=Price.docx");
            Response.ContentType = "application/vnd.ms-word";
            try
            {
                reportService.SaveCommodityPrice(new ReportBindingModel
                {
                    FileName= "D:\\Price.docx"
                });
                Response.WriteFile("D:\\Price.docx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllert", "<script>alert('" + ex.Message + "');</script>");
            }
            Response.End();
        }
    }
}