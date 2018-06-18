﻿using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarFactoryView
{
    public partial class FormCommodityIngridients : Form
    {
        public CommodityIngridientView Model { set { model = value; }  get { return model; } }

        private CommodityIngridientView model;

        public FormCommodityIngridients()
        {
            InitializeComponent();
        }

        private void FormCommodityIngridient_Load(object sender, EventArgs e)
        {
            try
            {
                var response = APIConsumer.GetRequest("api/Ingridient/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    comboBoxIngridient.DisplayMember = "IngridientName";
                    comboBoxIngridient.ValueMember = "Id";
                    comboBoxIngridient.DataSource = APIConsumer.GetElement<List<IngridientView>>(response);
                    comboBoxIngridient.SelectedItem = null;
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
            if (model != null)
            {
                comboBoxIngridient.Enabled = false;
                comboBoxIngridient.SelectedValue = model.IngridientId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngridient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new CommodityIngridientView
                    {
                        IngridientId = Convert.ToInt32(comboBoxIngridient.SelectedValue),
                        IngridientName = comboBoxIngridient.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
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
