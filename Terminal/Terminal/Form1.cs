using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace Terminal
{
    public partial class Form1 : Form
    {
        Byte customerBankID;
        Byte customerPaySystemID;
        Int64 customerCardNumber;

        Byte senderBankID;
        Byte senderPaySystemID;
        Int64 senderCardNumber;

        Int64 amount;

        Int32 operationResult;

        Byte[] outBuffer = new Byte[19];
        Byte[] inputBuffer = new Byte[1];

        public Form1()
        {
            InitializeComponent();
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                Byte[] temp=new Byte[1];
                serialPort1.Read(temp, 0, 1);
                operationResult = Convert.ToInt32(temp[0]);
                switch (operationResult)
                {
                    case 0: textBox2.Text = "Operation Failed!"; break;
                    case 1: textBox2.Text = "Operation Successefull!"; break;
                    default: break;
                }
            }));
        }

        private void payButton_Click(object sender, EventArgs e)
        {
            textBox2.Text = "Connecting to bank...";
           
            senderPaySystemID = Convert.ToByte(textBox4.Text);
            senderBankID = Convert.ToByte(textBox3.Text);
            Byte[] temp = new Byte[8];
            temp = BitConverter.GetBytes(Convert.ToInt64(cardNumberTextBox.Text));
            temp[6] = senderBankID;
            temp[7] = senderPaySystemID;
            senderCardNumber = BitConverter.ToInt64(temp, 0);

            for (int i = 0; i < 8; i++)
            {
                outBuffer[i] = temp[i];
            }
            temp = BitConverter.GetBytes(customerCardNumber);
            for (int i = 0; i < 8; i++)
            {
                outBuffer[i+8] = temp[i];
            }
            amount = Convert.ToInt64(textBox1.Text);
            temp = BitConverter.GetBytes(amount);
            for (int i = 0; i < 3; i++)
            {
                outBuffer[i + 16] = temp[i];
            }


            //serialPort1.Write(prepareData(), 0, 24);
        }

        private void customerCardNumberTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            customerCardNumberTextBox.Text = "";
        }
        private void bankIDTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            bankIDTextBox.Text = "";
        }
        private void paySystemIDTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            paySystemIDTextBox.Text = "";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
            int i = 0;
            int x = 0;
            int y = 0;
            int z = 0;
            //i++;
            x = i++;
            y = ++i;
            z = x + y;

            /*int x()
            {
                i++;
                return 10;
            }
            i += x();*/

        }

        private void createButton_Click(object sender, EventArgs e)
        {
            bankIDTextBox.Enabled = false;
            customerCardNumberTextBox.Enabled = false;
            comboBox1.Enabled = false;

            tabControl1.SelectedIndex = 1;
            createButton.Enabled = false;
            bankIDTextBox.Enabled = false;
            paySystemIDTextBox.Enabled = false;
            customerCardNumberTextBox.Enabled = false;


            customerCardNumber = Convert.ToInt64(customerCardNumberTextBox.Text);
            customerPaySystemID = Convert.ToByte(paySystemIDTextBox.Text);
            customerBankID = Convert.ToByte(bankIDTextBox.Text);
            Byte[] temp = new Byte[8];
            temp = BitConverter.GetBytes(customerCardNumber);
            temp[6] = customerBankID;
            temp[7] = customerPaySystemID;
            customerCardNumber = BitConverter.ToInt64(temp, 0);


            if (comboBox1.Text != "")
            {
                serialPort1.PortName = comboBox1.Text;
            }
            if (serialPort1.PortName != "_")
            {
                serialPort1.Open();
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
        }



        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen==false)
            {
                tabControl1.SelectedIndex = 0;
            }
        }


    }
}
