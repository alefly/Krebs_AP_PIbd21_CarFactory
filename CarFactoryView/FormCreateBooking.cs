using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarFactoryView
{
    public partial class FormCreateBooking : Form
    {
        public FormCreateBooking()
        {
            InitializeComponent();
        }

        private void FormCreateBooking_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIConsumer.GetRequest("api/Consumer/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<ConsumerView> list = APIConsumer.GetElement<List<ConsumerView>>(responseC);
                    if (list != null)
                    {
                        comboBoxConsumer.DisplayMember = "ConsumerName";
                        comboBoxConsumer.ValueMember = "Id";
                        comboBoxConsumer.DataSource = list;
                        comboBoxConsumer.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIConsumer.GetError(responseC));
                }
                var responseP = APIConsumer.GetRequest("api/Commodity/GetList");
                if (responseP.Result.IsSuccessStatusCode)
                {
                    List<CommodityView> list = APIConsumer.GetElement<List<CommodityView>>(responseP);
                    if (list != null)
                    {
                        comboBoxCommodity.DisplayMember = "CommodityName";
                        comboBoxCommodity.ValueMember = "Id";
                        comboBoxCommodity.DataSource = list;
                        comboBoxCommodity.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIConsumer.GetError(responseP));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxCommodity.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxCommodity.SelectedValue);
                    var responseP = APIConsumer.GetRequest("api/Commodity/Get/" + id);
                    if (responseP.Result.IsSuccessStatusCode)
                    {
                        CommodityView product = APIConsumer.GetElement<CommodityView>(responseP);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSum.Text = (count * (int)product.Price).ToString();
                    }
                    else
                    {
                        throw new Exception(APIConsumer.GetError(responseP));
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxCommodity_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxConsumer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCommodity.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIConsumer.PostRequest("api/Main/CreateBooking", new BindingBooking
                {
                    ConsumerId = Convert.ToInt32(comboBoxConsumer.SelectedValue),
                    CommodityId = Convert.ToInt32(comboBoxCommodity.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    throw new Exception(APIConsumer.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
