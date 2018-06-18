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
    public partial class FormBooking : System.Web.UI.Page
    {
        private readonly IConsumer serviceC=new ConsumerList();

        private readonly ICommodity serviceS = new CommodityList();

        private readonly IMain serviceM = new MainList();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<ConsumerView> listC = serviceC.GetList();
                if (listC != null)
                {
                    DropDownListConsumer.DataSource = listC;
                    DropDownListConsumer.DataBind();
                    DropDownListConsumer.DataTextField = "ConsumerName";
                    DropDownListConsumer.DataValueField = "Id";
                }
                List<CommodityView> listP = serviceS.GetList();
                if (listP != null)
                {
                    DropDownListCommodity.DataSource = listP;
                    DropDownListCommodity.DataBind();
                    DropDownListCommodity.DataTextField = "CommodityName";
                    DropDownListCommodity.DataValueField = "Id";
                }
                Page.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void CalcSum()
        {
            
            if (DropDownListCommodity.SelectedValue != null && !string.IsNullOrEmpty(TextBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(DropDownListCommodity.SelectedValue);
                    CommodityView product = serviceS.GetElement(id);
                    int count = Convert.ToInt32(TextBoxCount.Text);
                    TextBoxSum.Text = (count * product.Price).ToString();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void DropDownListService_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        protected void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            if (DropDownListConsumer.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите клиента');</script>");
                return;
            }
            if (DropDownListCommodity.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите изделие');</script>");
                return;
            }
            try
            {
                serviceM.CreateBooking(new BindingBooking
                {
                    ConsumerId = Convert.ToInt32(DropDownListConsumer.SelectedValue),
                    CommodityId = Convert.ToInt32(DropDownListCommodity.SelectedValue),
                    Count = Convert.ToInt32(TextBoxCount.Text),
                    Sum = Convert.ToInt32(TextBoxSum.Text)
                });
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("FormMain.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormMain.aspx");
        }
    }
}