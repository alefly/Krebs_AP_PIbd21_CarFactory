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
                    var response = APIConsumer.GetRequest("api/Ingridient/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var ingridient = APIConsumer.GetElement<IngridientView>(response);
                        textBoxName.Text = ingridient.IngridientName;
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
            this.reportViewer1.RefreshReport();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIConsumer.PostRequest("api/Ingridient/UpdElement", new BindingIngridients
                    {
                        Id = id.Value,
                        IngridientName = textBoxName.Text
                    });
                }
                else
                {
                    response = APIConsumer.PostRequest("api/Ingridient/AddElement", new BindingIngridients
                    {
                        IngridientName = textBoxName.Text
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
