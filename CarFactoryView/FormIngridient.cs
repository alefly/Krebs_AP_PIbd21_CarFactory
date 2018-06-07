using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarFactoryView
{
    public partial class FormIngridient : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormIngridient()
        {
            InitializeComponent();
        }

        private void FormIngridient_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var component = Task.Run(() => APIConsumer.GetRequestData<IngridientView>("api/Ingridient/Get/" + id.Value)).Result;
                    textBoxName.Text = component.IngridientName;
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
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string name = textBoxName.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIConsumer.PostRequestData("api/Ingridient/UpdElement", new BindingIngridients

                {
                    Id = id.Value,
                    IngridientName= name
                }));
            }
            else
            {
                task = Task.Run(() => APIConsumer.PostRequestData("api/Ingridient/AddElement", new BindingIngridients

                {
                    IngridientName = name
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
