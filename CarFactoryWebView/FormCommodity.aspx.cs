using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarFactoryWebView
{
    public partial class FormCommodity : System.Web.UI.Page
    {
        private readonly ICommodity service = UnityConfig.Container.Resolve<ICommodity>();

        private int id;

        private List<CommodityIngridientView> commodityIngridient;

        private CommodityIngridientView model;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    CommodityView view = service.GetElement(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            textBoxName.Text = view.CommodityName;
                            textBoxPrice.Text = view.Price.ToString();
                        }
                        commodityIngridient = view.CommodityIngridients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                commodityIngridient = new List<CommodityIngridientView>();
            }

            if (Session["SEId"] != null)
            {
                if (Session["SEIs"] != null)
                {
                    model = new CommodityIngridientView
                    {
                        Id = (int)Session["SEId"],
                        CommodityId = (int)Session["SECommodityId"],
                        IngridientId = (int)Session["SEIngridientId"],
                        IngridientName = (string)Session["SEIngridientName"],
                        Count = (int)Session["SECount"]
                    };
                    commodityIngridient[(int)Session["SEIs"]] = model;
                }
                else
                {
                    model = new CommodityIngridientView
                    {
                        CommodityId = (int)Session["SECommodityId"],
                        IngridientId = (int)Session["SEIngridientId"],
                        IngridientName = (string)Session["SEIngridientName"],
                        Count = (int)Session["SECount"]
                    };
                    commodityIngridient.Add(model);
                }
                Session["SEId"] = null;
                Session["SECommodityId"] = null;
                Session["SEIngridientId"] = null;
                Session["SEIngridientName"] = null;
                Session["SECount"] = null;
                Session["SEIs"] = null;
            }
            List<BindingCommodityIngridient> commodityIngridientBM = new List<BindingCommodityIngridient>();
            for (int i = 0; i < commodityIngridient.Count; ++i)
            {
                commodityIngridientBM.Add(new BindingCommodityIngridient
                {
                    Id = this.commodityIngridient[i].Id,
                    CommodityId = this.commodityIngridient[i].CommodityId,
                    IngridientId = this.commodityIngridient[i].IngridientId,
                    Count = this.commodityIngridient[i].Count
                });
            }
            if (commodityIngridientBM.Count != 0)
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new BindingCommodity
                    {
                        Id = id,
                        CommodityName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        CommodityIngridients = commodityIngridientBM
                    });
                }
                else
                {
                    service.AddElement(new BindingCommodity
                    {
                        CommodityName = "-0",
                        Price = 0,
                        CommodityIngridients = commodityIngridientBM
                    });
                    Session["id"] = service.GetList().Last().Id.ToString();
                    Session["Change"] = "0";
                }
            }
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (commodityIngridient != null)
                {
                    dataGridView.DataBind();
                    dataGridView.DataSource = commodityIngridient;
                    dataGridView.DataBind();
                    dataGridView.ShowHeaderWhenEmpty = true;
                    dataGridView.SelectedRowStyle.BackColor = Color.Silver;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormCommodityIngridient.aspx");
        }

        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                model = service.GetElement(id).CommodityIngridients[dataGridView.SelectedIndex];
                Session["SEId"] = model.Id;
                Session["SECommodityId"] = model.CommodityId;
                Session["SEIngridientId"] = model.IngridientId;
                Session["SEIngridientName"] = model.IngridientName;
                Session["SECount"] = model.Count;
                Session["SEIs"] = dataGridView.SelectedIndex;
                Session["Change"] = "0";
                Server.Transfer("FormCommodityIngridient.aspx");
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                try
                {
                    commodityIngridient.RemoveAt(dataGridView.SelectedIndex);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
                LoadData();
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните название');</script>");
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните цену');</script>");
                return;
            }
            if (commodityIngridient == null || commodityIngridient.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните компоненты');</script>");
                return;
            }
            try
            {
                List<BindingCommodityIngridient> commodityIngridientBM = new List<BindingCommodityIngridient>();
                for (int i = 0; i < commodityIngridient.Count; ++i)
                {
                    commodityIngridientBM.Add(new BindingCommodityIngridient
                    {
                        Id = commodityIngridient[i].Id,
                        CommodityId = commodityIngridient[i].CommodityId,
                        IngridientId = commodityIngridient[i].IngridientId,
                        Count = commodityIngridient[i].Count
                    });
                }
                service.DelElement(service.GetList().Last().Id);
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new BindingCommodity
                    {
                        Id = id,
                        CommodityName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        CommodityIngridients = commodityIngridientBM
                    });
                }
                else
                {
                    service.AddElement(new BindingCommodity
                    {
                        CommodityName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        CommodityIngridients = commodityIngridientBM
                    });
                }
                Session["id"] = null;
                Session["Change"] = null;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("FormCommodities.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (service.GetList().Count != 0 && service.GetList().Last().CommodityName == null)
            {
                service.DelElement(service.GetList().Last().Id);
            }
            Session["id"] = null;
            Server.Transfer("FormCommodities.aspx");
        }

        protected void dataGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
}