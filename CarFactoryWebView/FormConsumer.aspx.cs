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
using Unity;
using Unity.Attributes;

namespace CarFactoryWebView
{
    public partial class FormConsumer : System.Web.UI.Page
    {
        public int Id { set { id = value; } }

        private readonly IConsumer service = new ConsumerList();

        private int id;

        private string name;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    ConsumerView view = service.GetElement(id);
                    if (view != null)
                    {
                        name = view.ConsumerName;
                        service.UpdElement(new BindingConsumer
                        {
                            Id = id,
                            ConsumerName = ""
                        });
                        if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(textBoxName.Text))
                        {
                            textBoxName.Text = name;
                        }
                        service.UpdElement(new BindingConsumer
                        {
                            Id = id,
                            ConsumerName = name
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
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните ФИО');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new BindingConsumer
                    {
                        Id = id,
                        ConsumerName = textBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new BindingConsumer
                    {
                        ConsumerName = textBoxName.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Server.Transfer("FormConsumers.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Server.Transfer("FormConsumers.aspx");
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Server.Transfer("FormConsumers.aspx");
        }
    }
}