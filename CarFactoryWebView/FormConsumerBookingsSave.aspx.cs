using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using System;
using System.Web.UI;
using Unity;

namespace CarFactoryWebView
{
    public partial class FormConsumerBookingsSave : System.Web.UI.Page
    {
        readonly IReportService reportService = UnityConfig.Container.Resolve<IReportService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "filename=ClientOrders.pdf");
            Response.ContentType = "application/vnd.ms-word";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            try
            {
                reportService.SaveConsumerBookings(new ReportBindingModel
                {
                    FileName = "D:\\ClientOrders.pdf",
                    DateFrom = DateTime.Parse(Session["DateFrom"].ToString()),
                    DateTo = DateTime.Parse(Session["DateTo"].ToString())
                });
                Response.WriteFile("D:\\ClientOrders.pdf");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllert", "<script>alert('" + ex.Message + "');</script>");
            }
            Response.End();
        }
    }
}