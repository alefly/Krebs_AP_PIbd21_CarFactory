using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarFactoryView
{
    public partial class FormConsumer : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormConsumer()
        {
            InitializeComponent();
        }

        private void FormConsumer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var client = Task.Run(() => APIConsumer.GetRequestData<ConsumerView>("api/Consumer/Get/" + id.Value)).Result;
                    textBoxName.Text = client.ConsumerName;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string name = textBoxName.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIConsumer.PostRequestData("api/Consumer/UpdElement", new BindingConsumer

                {

                    Id = id.Value,
                    ConsumerName = name
                }));
            }
            else
            {
                task = Task.Run(() => APIConsumer.PostRequestData("api/Consumer/AddElement", new BindingConsumer

                {
                    ConsumerName = name
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
