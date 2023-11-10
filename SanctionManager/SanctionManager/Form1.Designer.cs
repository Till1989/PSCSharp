
namespace SanctionManager
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.availabilityCheckBox = new System.Windows.Forms.CheckBox();
            this.SenderComboBox = new System.Windows.Forms.ComboBox();
            this.RecipientComboBox = new System.Windows.Forms.ComboBox();
            this.reloadDataButton = new System.Windows.Forms.Button();
            //this.sanctionsDataSet = new SanctionManager.SanctionsDataSet();
           // ((System.ComponentModel.ISupportInitialize)(this.sanctionsDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sender Bank Name (ID)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Recipient Bank Name (ID)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(415, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Transaction Availability";
            // 
            // availabilityCheckBox
            // 
            this.availabilityCheckBox.AutoSize = true;
            this.availabilityCheckBox.Location = new System.Drawing.Point(468, 41);
            this.availabilityCheckBox.Name = "availabilityCheckBox";
            this.availabilityCheckBox.Size = new System.Drawing.Size(15, 14);
            this.availabilityCheckBox.TabIndex = 3;
            this.availabilityCheckBox.UseVisualStyleBackColor = true;
            this.availabilityCheckBox.CheckedChanged += new System.EventHandler(this.availabilityCheckBox_CheckedChanged);
            // 
            // SenderComboBox
            // 
            this.SenderComboBox.FormattingEnabled = true;
            this.SenderComboBox.Location = new System.Drawing.Point(16, 38);
            this.SenderComboBox.Name = "SenderComboBox";
            this.SenderComboBox.Size = new System.Drawing.Size(177, 21);
            this.SenderComboBox.TabIndex = 4;
            this.SenderComboBox.SelectedIndexChanged += new System.EventHandler(this.SenderComboBox_SelectedIndexChanged);
            // 
            // RecipientComboBox
            // 
            this.RecipientComboBox.FormattingEnabled = true;
            this.RecipientComboBox.Location = new System.Drawing.Point(215, 38);
            this.RecipientComboBox.Name = "RecipientComboBox";
            this.RecipientComboBox.Size = new System.Drawing.Size(177, 21);
            this.RecipientComboBox.TabIndex = 5;
            this.RecipientComboBox.SelectedIndexChanged += new System.EventHandler(this.RecipientComboBox_SelectedIndexChanged);
            // 
            // reloadDataButton
            // 
            this.reloadDataButton.Location = new System.Drawing.Point(16, 86);
            this.reloadDataButton.Name = "reloadDataButton";
            this.reloadDataButton.Size = new System.Drawing.Size(75, 23);
            this.reloadDataButton.TabIndex = 6;
            this.reloadDataButton.Text = "Reload Data";
            this.reloadDataButton.UseVisualStyleBackColor = true;
            this.reloadDataButton.Click += new System.EventHandler(this.reloadDataButton_Click);
            // 
            // sanctionsDataSet
            // 
            //this.sanctionsDataSet.DataSetName = "SanctionsDataSet";
            //this.sanctionsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 121);
            this.Controls.Add(this.reloadDataButton);
            this.Controls.Add(this.RecipientComboBox);
            this.Controls.Add(this.SenderComboBox);
            this.Controls.Add(this.availabilityCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SanctionManager";
            this.Load += new System.EventHandler(this.Form1_Load);
            //((System.ComponentModel.ISupportInitialize)(this.sanctionsDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox availabilityCheckBox;
        private System.Windows.Forms.ComboBox SenderComboBox;
        private System.Windows.Forms.ComboBox RecipientComboBox;
        private System.Windows.Forms.Button reloadDataButton;
       // private SanctionsDataSet sanctionsDataSet;
    }
}

