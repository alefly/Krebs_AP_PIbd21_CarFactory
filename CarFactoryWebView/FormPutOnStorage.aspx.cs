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
    public partial class FormPutOnStorage : System.Web.UI.Page
    {
        private readonly IStorage serviceS = new StorageList();

        private readonly IIngridient serviceE = new IngridientList();

        private readonly IMain serviceM = new MainList();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<IngridientView> listE = serviceE.GetList();
                if (listE != null)
                {
                    DropDownListStorage.DataSource = listE;
                    DropDownListStorage.DataBind();
                    DropDownListStorage.DataTextField = "IngridientName";
                    DropDownListStorage.DataValueField = "Id";
                }
                List<StorageView> listS = serviceS.GetList();
                if (listS != null)
                {
                    DropDownListIngridient.DataSource = listS;
                    DropDownListIngridient.DataBind();
                    DropDownListIngridient.DataTextField = "StorageName";
                    DropDownListIngridient.DataValueField = "Id";
                }
                Page.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле количество');</script>");
                return;
            }
            if (DropDownListIngridient.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите компонент');</script>");
                return;
            }
            if (DropDownListStorage.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите склад');</script>");
                return;
            }
            try
            {
                serviceM.PutIngridientOnStorage(new BindingStorageIngridients
                {
                    IngridientId = Convert.ToInt32(DropDownListIngridient.SelectedValue),
                    StorageId = Convert.ToInt32(DropDownListStorage.SelectedValue),
                    Count = Convert.ToInt32(TextBoxCount.Text)
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