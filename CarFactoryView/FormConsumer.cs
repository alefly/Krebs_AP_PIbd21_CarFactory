using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Net.Http;
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
                    var response = APIConsumer.GetRequest("api/Consumer/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var client = APIConsumer.GetElement<ConsumerView>(response);
                        textBoxName.Text = client.ConsumerName;
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
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIConsumer.PostRequest("api/Consumer/UpdElement", new BindingConsumer
                    {
                        Id = id.Value,
                        ConsumerName = textBoxName.Text
                    });
                }
                else
                {
                    response = APIConsumer.PostRequest("api/Consumer/AddElement", new BindingConsumer
                    {
                        ConsumerName = textBoxName.Text
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
