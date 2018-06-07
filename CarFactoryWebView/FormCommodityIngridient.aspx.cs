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
    public partial class FormCommodityIngridient : System.Web.UI.Page
    {
        private readonly IIngridient service = new IngridientList();

        private CommodityIngridientView model;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<IngridientView> list = service.GetList();
                if (list != null)
                {
                    DropDownListIngridient.DataSource = list;
                    DropDownListIngridient.DataValueField = "Id";
                    DropDownListIngridient.DataTextField = "IngridientName";
                    DropDownListIngridient.SelectedIndex = -1;
                    Page.DataBind();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
            if (Session["SEId"] != null)
            {
                DropDownListIngridient.Enabled = false;
                DropDownListIngridient.SelectedValue = (string)Session["SEIngridientId"];
                TextBoxCount.Text = (string)Session["SECount"];
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            if (DropDownListIngridient.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите компонент');</script>");
                return;
            }
            try
            {
                if (Session["SEId"] == null)
                {
                    model = new CommodityIngridientView
                    {
                        IngridientId = Convert.ToInt32(DropDownListIngridient.SelectedValue),
                        IngridientName = DropDownListIngridient.SelectedItem.Text,
                        Count = Convert.ToInt32(TextBoxCount.Text)
                    };
                    Session["SEId"] = model.Id;
                    Session["SECommodityId"] = model.CommodityId;
                    Session["SEIngridientId"] = model.IngridientId;
                    Session["SEIngridientName"] = model.IngridientName;
                    Session["SECount"] = model.Count;
                }
                else
                {
                    model.Count = Convert.ToInt32(TextBoxCount.Text);
                    Session["SEId"] = model.Id;
                    Session["SEServiceId"] = model.CommodityId;
                    Session["SEIngridientId"] = model.IngridientId;
                    Session["SEIngridientName"] = model.IngridientName;
                    Session["SECount"] = model.Count;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("FormCommodity.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormCommodity.aspx");
        }
    }
}