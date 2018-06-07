using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarFactoryView
{
    public partial class FormTakeBookingInWork : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormTakeBookingInWork()
        {
            InitializeComponent();
        }

        private void FormTakeBookingInWork_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                var response = APIConsumer.GetRequest("api/Worker/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<WorkerView> list = APIConsumer.GetElement<List<WorkerView>>(response);
                    if (list != null)
                    {
                        comboBoxWorker.DisplayMember = "WorkerName";
                        comboBoxWorker.ValueMember = "Id";
                        comboBoxWorker.DataSource = list;
                        comboBoxWorker.SelectedItem = null;
                    }
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxWorker.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIConsumer.PostRequest("api/Main/TakeBookingInWork", new BindingBooking
                {
                    Id = id.Value,
                    WorkerId = Convert.ToInt32(comboBoxWorker.SelectedValue)
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
