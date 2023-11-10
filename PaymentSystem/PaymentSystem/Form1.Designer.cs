
namespace PaymentSystem
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
            this.components = new System.ComponentModel.Container();
            this.bankPort1 = new System.IO.Ports.SerialPort(this.components);
            this.bankPort2 = new System.IO.Ports.SerialPort(this.components);
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.bankPort0 = new System.IO.Ports.SerialPort(this.components);
            this.PSIDLabel = new System.Windows.Forms.Label();
            this.PSNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bankPort1
            // 
            this.bankPort1.ReadBufferSize = 32;
            this.bankPort1.ReceivedBytesThreshold = 27;
            this.bankPort1.WriteBufferSize = 32;
            this.bankPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.bankPort1_DataReceived);
            // 
            // bankPort2
            // 
            this.bankPort2.ReadBufferSize = 32;
            this.bankPort2.ReceivedBytesThreshold = 27;
            this.bankPort2.WriteBufferSize = 32;
            this.bankPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.bankPort2_DataReceived);
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.White;
            this.logTextBox.Location = new System.Drawing.Point(12, 150);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(270, 236);
            this.logTextBox.TabIndex = 2;
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(12, 121);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(75, 23);
            this.clearLogButton.TabIndex = 3;
            this.clearLogButton.Text = "Clear Log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            this.clearLogButton.Click += new System.EventHandler(this.clearLogButton_Click);
            // 
            // bankPort0
            // 
            this.bankPort0.ReadBufferSize = 32;
            this.bankPort0.ReceivedBytesThreshold = 27;
            this.bankPort0.WriteBufferSize = 32;
            this.bankPort0.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.bankPort0_DataReceived);
            // 
            // PSIDLabel
            // 
            this.PSIDLabel.AutoSize = true;
            this.PSIDLabel.Location = new System.Drawing.Point(12, 9);
            this.PSIDLabel.Name = "PSIDLabel";
            this.PSIDLabel.Size = new System.Drawing.Size(35, 13);
            this.PSIDLabel.TabIndex = 6;
            this.PSIDLabel.Text = "label2";
            // 
            // PSNameLabel
            // 
            this.PSNameLabel.AutoSize = true;
            this.PSNameLabel.Location = new System.Drawing.Point(12, 28);
            this.PSNameLabel.Name = "PSNameLabel";
            this.PSNameLabel.Size = new System.Drawing.Size(35, 13);
            this.PSNameLabel.TabIndex = 7;
            this.PSNameLabel.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(288, 398);
            this.Controls.Add(this.PSNameLabel);
            this.Controls.Add(this.PSIDLabel);
            this.Controls.Add(this.clearLogButton);
            this.Controls.Add(this.logTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort bankPort1;
        private System.IO.Ports.SerialPort bankPort2;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Button clearLogButton;
        private System.IO.Ports.SerialPort bankPort0;
        private System.Windows.Forms.Label PSIDLabel;
        private System.Windows.Forms.Label PSNameLabel;
    }
}

