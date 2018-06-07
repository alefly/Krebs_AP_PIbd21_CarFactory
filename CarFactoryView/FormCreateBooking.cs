using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                List<ConsumerView> listC = Task.Run(() => APIConsumer.GetRequestData<List<ConsumerView>>("api/Consumer/GetList")).Result;
                if (listC != null)
                {
                    comboBoxConsumer.DisplayMember = "ConsumerFIO";
                    comboBoxConsumer.ValueMember = "Id";
                    comboBoxConsumer.DataSource = listC;
                    comboBoxConsumer.SelectedItem = null;
                }

                List<CommodityView> listP = Task.Run(() => APIConsumer.GetRequestData<List<CommodityView>>("api/Commodity/GetList")).Result;
                if (listP != null)
                {
                    comboBoxCommodity.DisplayMember = "CommodityName";
                    comboBoxCommodity.ValueMember = "Id";
                    comboBoxCommodity.DataSource = listP;
                    comboBoxCommodity.SelectedItem = null;
                }
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

        private void CalcSum()
        {
            if (comboBoxCommodity.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxCommodity.SelectedValue);
                    CommodityView Commodity = Task.Run(() => APIConsumer.GetRequestData<CommodityView>("api/Commodity/Get/" + id)).Result;
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * (int)Commodity.Price).ToString();
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
            int ConsumerId = Convert.ToInt32(comboBoxConsumer.SelectedValue);
            int CommodityId = Convert.ToInt32(comboBoxCommodity.SelectedValue);
            int count = Convert.ToInt32(textBoxCount.Text);
            int sum = Convert.ToInt32(textBoxSum.Text);
            Task task = Task.Run(() => APIConsumer.PostRequestData("api/Main/CreateBooking", new BindingBooking
            {
                ConsumerId = ConsumerId,
                CommodityId = CommodityId,
                Count = count,
                Sum = sum
            }));

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