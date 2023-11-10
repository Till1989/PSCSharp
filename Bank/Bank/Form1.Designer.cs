
namespace Bank
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
            this.terminalPort = new System.IO.Ports.SerialPort(this.components);
            this.paySystem1Port = new System.IO.Ports.SerialPort(this.components);
            this.paySystem2Port = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.inputBlinkTimer = new System.Windows.Forms.Timer(this.components);
            this.outputBlinkTimer = new System.Windows.Forms.Timer(this.components);
            this.terminalWrite = new System.Windows.Forms.Timer(this.components);
            this.paySystem0Port = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.outputIndicator = new System.Windows.Forms.Panel();
            this.inputIndicator = new System.Windows.Forms.Panel();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.bankIDLabel = new System.Windows.Forms.Label();
            this.bankRegCountryLabel = new System.Windows.Forms.Label();
            this.bankNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // terminalPort
            // 
            this.terminalPort.PortName = "_";
            this.terminalPort.ReceivedBytesThreshold = 27;
            this.terminalPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.terminalPort_DataReceived);
            // 
            // paySystem1Port
            // 
            this.paySystem1Port.PortName = "_";
            this.paySystem1Port.ReceivedBytesThreshold = 27;
            this.paySystem1Port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.paySystem1Port_DataReceived);
            // 
            // paySystem2Port
            // 
            this.paySystem2Port.PortName = "_";
            this.paySystem2Port.ReceivedBytesThreshold = 27;
            this.paySystem2Port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.paySystem2Port_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // inputBlinkTimer
            // 
            this.inputBlinkTimer.Interval = 500;
            this.inputBlinkTimer.Tick += new System.EventHandler(this.inputBlinkTimer_Tick);
            // 
            // outputBlinkTimer
            // 
            this.outputBlinkTimer.Interval = 500;
            this.outputBlinkTimer.Tick += new System.EventHandler(this.outputBlinkTimer_Tick);
            // 
            // paySystem0Port
            // 
            this.paySystem0Port.PortName = "_";
            this.paySystem0Port.ReceivedBytesThreshold = 27;
            this.paySystem0Port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.paySystem0Port_DataReceived);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "CardNumber__CVV__ExpDate::::Balance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Data Out";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Data In";
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(310, 252);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(75, 23);
            this.clearLogButton.TabIndex = 20;
            this.clearLogButton.Text = "Clear Log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            // 
            // outputIndicator
            // 
            this.outputIndicator.Location = new System.Drawing.Point(118, 232);
            this.outputIndicator.Name = "outputIndicator";
            this.outputIndicator.Size = new System.Drawing.Size(32, 26);
            this.outputIndicator.TabIndex = 19;
            // 
            // inputIndicator
            // 
            this.inputIndicator.BackColor = System.Drawing.Color.Transparent;
            this.inputIndicator.Location = new System.Drawing.Point(51, 232);
            this.inputIndicator.Name = "inputIndicator";
            this.inputIndicator.Size = new System.Drawing.Size(32, 26);
            this.inputIndicator.TabIndex = 18;
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.White;
            this.logTextBox.Location = new System.Drawing.Point(11, 300);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(401, 321);
            this.logTextBox.TabIndex = 17;
            // 
            // infoTextBox
            // 
            this.infoTextBox.Location = new System.Drawing.Point(11, 71);
            this.infoTextBox.Multiline = true;
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.Size = new System.Drawing.Size(401, 95);
            this.infoTextBox.TabIndex = 16;
            // 
            // bankIDLabel
            // 
            this.bankIDLabel.AutoSize = true;
            this.bankIDLabel.Location = new System.Drawing.Point(12, 33);
            this.bankIDLabel.Name = "bankIDLabel";
            this.bankIDLabel.Size = new System.Drawing.Size(35, 13);
            this.bankIDLabel.TabIndex = 15;
            this.bankIDLabel.Text = "label1";
            // 
            // bankRegCountryLabel
            // 
            this.bankRegCountryLabel.AutoSize = true;
            this.bankRegCountryLabel.Location = new System.Drawing.Point(12, 20);
            this.bankRegCountryLabel.Name = "bankRegCountryLabel";
            this.bankRegCountryLabel.Size = new System.Drawing.Size(35, 13);
            this.bankRegCountryLabel.TabIndex = 14;
            this.bankRegCountryLabel.Text = "label1";
            // 
            // bankNameLabel
            // 
            this.bankNameLabel.AutoSize = true;
            this.bankNameLabel.Location = new System.Drawing.Point(12, 7);
            this.bankNameLabel.Name = "bankNameLabel";
            this.bankNameLabel.Size = new System.Drawing.Size(35, 13);
            this.bankNameLabel.TabIndex = 13;
            this.bankNameLabel.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(424, 626);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clearLogButton);
            this.Controls.Add(this.outputIndicator);
            this.Controls.Add(this.inputIndicator);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.bankIDLabel);
            this.Controls.Add(this.bankRegCountryLabel);
            this.Controls.Add(this.bankNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bank";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort terminalPort;
        private System.IO.Ports.SerialPort paySystem1Port;
        private System.IO.Ports.SerialPort paySystem2Port;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer inputBlinkTimer;
        private System.Windows.Forms.Timer outputBlinkTimer;
        private System.Windows.Forms.Timer terminalWrite;
        private System.IO.Ports.SerialPort paySystem0Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.Panel outputIndicator;
        private System.Windows.Forms.Panel inputIndicator;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.Label bankIDLabel;
        private System.Windows.Forms.Label bankRegCountryLabel;
        private System.Windows.Forms.Label bankNameLabel;
    }
}

