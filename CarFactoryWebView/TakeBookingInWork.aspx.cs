using CarFactoryService.BindingModels;
using Unity;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace CarFactoryWebView
{
    public partial class TakeBookingInWork : System.Web.UI.Page
    {
        private readonly IWorker serviceP = UnityConfig.Container.Resolve<IWorker>();

        private readonly IMain serviceM = UnityConfig.Container.Resolve<IMain>();

        private int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (!Int32.TryParse((string)Session["id"], out id))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Не указан заказ');</script>");
                        Server.Transfer("FormMain.aspx");

                        List<WorkerView> listI = serviceP.GetList();
                        if (listI != null)
                        {
                            DropDownListWorker.DataSource = listI;
                            DropDownListWorker.DataBind();
                            DropDownListWorker.DataTextField = "WorkerName";
                            DropDownListWorker.DataValueField = "Id";
                            DropDownListWorker.SelectedIndex = -1;
                        }
                        Page.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (DropDownListWorker.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите исполнителя');</script>");
                return;
            }
            try
            {
                serviceM.TakeBookingInWork(new BindingBooking
                {
                    Id = id + 1,
                    WorkerId = Convert.ToInt32(DropDownListWorker.SelectedValue)
                });
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Session["id"] = null;
                Server.Transfer("FormMain.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("FormMain.aspx");
        }
    }
}