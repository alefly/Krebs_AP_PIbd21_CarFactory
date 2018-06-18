namespace CarFactoryView
{
    partial class FormCreateBooking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer ingridients = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (ingridients != null))
            {
                ingridients.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxConsumer = new System.Windows.Forms.ComboBox();
            this.labelConsumer = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.comboBoxCommodity = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.labelCommodity = new System.Windows.Forms.Label();
            this.labelSum = new System.Windows.Forms.Label();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxConsumer
            // 
            this.comboBoxConsumer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConsumer.FormattingEnabled = true;
            this.comboBoxConsumer.Location = new System.Drawing.Point(87, 6);
            this.comboBoxConsumer.Name = "comboBoxConsumer";
            this.comboBoxConsumer.Size = new System.Drawing.Size(217, 21);
            this.comboBoxConsumer.TabIndex = 1;
            // 
            // labelConsumer
            // 
            this.labelConsumer.AutoSize = true;
            this.labelConsumer.Location = new System.Drawing.Point(12, 9);
            this.labelConsumer.Name = "labelConsumer";
            this.labelConsumer.Size = new System.Drawing.Size(46, 13);
            this.labelConsumer.TabIndex = 0;
            this.labelConsumer.Text = "Клиент:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(12, 63);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 4;
            this.labelCount.Text = "Количество:";
            // 
            // comboBoxCommodity
            // 
            this.comboBoxCommodity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCommodity.FormattingEnabled = true;
            this.comboBoxCommodity.Location = new System.Drawing.Point(87, 33);
            this.comboBoxCommodity.Name = "comboBoxCommodity";
            this.comboBoxCommodity.Size = new System.Drawing.Size(217, 21);
            this.comboBoxCommodity.TabIndex = 3;
            this.comboBoxCommodity.SelectedIndexChanged += new System.EventHandler(this.comboBoxCommodity_SelectedIndexChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(219, 112);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(138, 112);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(87, 60);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(217, 20);
            this.textBoxCount.TabIndex = 5;
            this.textBoxCount.TextChanged += new System.EventHandler(this.textBoxCount_TextChanged);
            // 
            // labelCommodity
            // 
            this.labelCommodity.AutoSize = true;
            this.labelCommodity.Location = new System.Drawing.Point(12, 36);
            this.labelCommodity.Name = "labelCommodity";
            this.labelCommodity.Size = new System.Drawing.Size(54, 13);
            this.labelCommodity.TabIndex = 2;
            this.labelCommodity.Text = "Изделие:";
            // 
            // labelSum
            // 
            this.labelSum.AutoSize = true;
            this.labelSum.Location = new System.Drawing.Point(12, 89);
            this.labelSum.Name = "labelSum";
            this.labelSum.Size = new System.Drawing.Size(44, 13);
            this.labelSum.TabIndex = 6;
            this.labelSum.Text = "Сумма:";
            // 
            // textBoxSum
            // 
            this.textBoxSum.Location = new System.Drawing.Point(87, 86);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.ReadOnly = true;
            this.textBoxSum.Size = new System.Drawing.Size(217, 20);
            this.textBoxSum.TabIndex = 7;
            // 
            // FormCreateBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 151);
            this.Controls.Add(this.labelSum);
            this.Controls.Add(this.textBoxSum);
            this.Controls.Add(this.comboBoxConsumer);
            this.Controls.Add(this.labelConsumer);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.comboBoxCommodity);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.labelCommodity);
            this.Name = "FormCreateBooking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заказ";
            this.Load += new System.EventHandler(this.FormCreateBooking_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxConsumer;
        private System.Windows.Forms.Label labelConsumer;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ComboBox comboBoxCommodity;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label labelCommodity;
        private System.Windows.Forms.Label labelSum;
        private System.Windows.Forms.TextBox textBoxSum;
    }
}