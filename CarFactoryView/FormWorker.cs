using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarFactoryView
{
    public partial class FormWorker : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormWorker()
        {
            InitializeComponent();
        }

        private void FormWorker_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIConsumer.GetRequest("api/Worker/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var implementer = APIConsumer.GetElement<WorkerView>(response);
                        textBoxName.Text = implementer.WorkerName;
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
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIConsumer.PostRequest("api/Worker/UpdElement", new BindingWorkers
                    {
                        Id = id.Value,
                        WorkerName = textBoxName.Text
                    });
                }
                else
                {
                    response = APIConsumer.PostRequest("api/Worker/AddElement", new BindingWorkers
                    {
                        WorkerName = textBoxName.Text
                    });
                }
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
