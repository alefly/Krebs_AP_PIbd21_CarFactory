﻿using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarFactoryView
{
    public partial class FormPutOnStorage : Form
    {
        public FormPutOnStorage()
        {
            InitializeComponent();
        }

        private void FormPutOnStorage_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIConsumer.GetRequest("api/Ingridient/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<IngridientView> list = APIConsumer.GetElement<List<IngridientView>>(responseC);
                    if (list != null)
                    {
                        comboBoxIngridient.DisplayMember = "IngridientName";
                        comboBoxIngridient.ValueMember = "Id";
                        comboBoxIngridient.DataSource = list;
                        comboBoxIngridient.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIConsumer.GetError(responseC));
                }
                var responseS = APIConsumer.GetRequest("api/Storage/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                {
                    List<StorageView> list = APIConsumer.GetElement<List<StorageView>>(responseS);
                    if (list != null)
                    {
                        comboBoxStorage.DisplayMember = "StorageName";
                        comboBoxStorage.ValueMember = "Id";
                        comboBoxStorage.DataSource = list;
                        comboBoxStorage.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIConsumer.GetError(responseC));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIConsumer.PostRequest("api/Main/PutIngridientOnStorage", new BindingStorageIngridients
                {
                    IngridientId = Convert.ToInt32(comboBoxIngridient.SelectedValue),
                    StorageId = Convert.ToInt32(comboBoxStorage.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
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
