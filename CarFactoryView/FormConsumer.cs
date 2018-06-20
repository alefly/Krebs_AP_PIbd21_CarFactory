﻿using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Text.RegularExpressions;
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

                    textBoxMail.Text = client.Mail;
                    dataGridView.DataSource = client.Messages;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

            string mail = textBoxMail.Text;
                        if (!string.IsNullOrEmpty(mail))
                            {
                                if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                                    {
                    MessageBox.Show("Неверный формат для электронной почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                            }
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIConsumer.PostRequestData("api/Consumer/UpdElement", new BindingConsumer

                {

                    Id = id.Value,

                    ConsumerName = name,
                    Mail = mail
                }));
            }
            else
            {
                task = Task.Run(() => APIConsumer.PostRequestData("api/Consumer/AddElement", new BindingConsumer

                {

                    ConsumerName = name,
                    Mail = mail
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
