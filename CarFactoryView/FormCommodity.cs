using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarFactoryView
{
    public partial class FormCommodity : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        private List<CommodityIngridientView> commodityIngridients;

        public FormCommodity()
        {
            InitializeComponent();
        }

        private void FormCommodity_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var commodity = Task.Run(() => APIConsumer.GetRequestData<CommodityView>("api/Commodity/Get/" + id.Value)).Result;
                    textBoxName.Text = commodity.CommodityName;
                    textBoxPrice.Text = commodity.Price.ToString();
                    commodityIngridients = commodity.CommodityIngridients;
                    LoadData();
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                commodityIngridients = new List<CommodityIngridientView>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (commodityIngridients != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = commodityIngridients;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormCommodityIngridients();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.CommodityId = id.Value;
                    }
                    commodityIngridients.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormCommodityIngridients();
                form.Model = commodityIngridients[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    commodityIngridients[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        commodityIngridients.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (commodityIngridients == null || commodityIngridients.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<BindingCommodityIngridient> commodityIngridientBM = new List<BindingCommodityIngridient>();
            for (int i = 0; i < commodityIngridients.Count; ++i)
            {
                commodityIngridientBM.Add(new BindingCommodityIngridient
                {
                    Id = commodityIngridients[i].Id,
                    CommodityId = commodityIngridients[i].CommodityId,
                    IngridientId = commodityIngridients[i].IngridientId,
                    Count = commodityIngridients[i].Count
                });
            }
            string name = textBoxName.Text;
            int price = Convert.ToInt32(textBoxPrice.Text);
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIConsumer.PostRequestData("api/Commodity/UpdElement", new BindingCommodity
                {
                    Id = id.Value,
                    CommodityName = name,
                    Price = price,
                    CommodityIngridients = commodityIngridientBM
                }));
            }
            else
            {
                task = Task.Run(() => APIConsumer.PostRequestData("api/Commodity/AddElement", new BindingCommodity
                {
                    CommodityName = name,
                    Price = price,
                    CommodityIngridients = commodityIngridientBM
                }));
            }

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith((prevTask) =>
            {
                var ex = (Exception)prevTask.Exception;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }, TaskContinuationOptions.OnlyOnFaulted);

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}