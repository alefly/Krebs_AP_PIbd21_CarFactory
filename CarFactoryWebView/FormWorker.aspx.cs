using CarFactoryService.BindingModels;
using CarFactoryService.ImplementationsList;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarFactoryWebView
{
    public partial class FormWorker : System.Web.UI.Page
    {
        private readonly IWorker service = new WorkerList();

        private int id;

        private string name;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    WorkerView view = service.GetElement(id);
                    if (view != null)
                    {
                        name = view.WorkerName;
                        service.UpdElement(new BindingWorkers
                        {
                            Id = id,
                            WorkerName = ""
                        });
                        if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(TextBoxName.Text))
                        {
                            TextBoxName.Text = name;
                        }
                        service.UpdElement(new BindingWorkers
                        {
                            Id = id,
                            WorkerName = name
                        });
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните ФИО');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new BindingWorkers
                    {
                        Id = id,
                        WorkerName = TextBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new BindingWorkers
                    {
                        WorkerName = TextBoxName.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Server.Transfer("FormWorkers.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Server.Transfer("FormWorkers.aspx");
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("FormWorkers.aspx");
        }
    }
}