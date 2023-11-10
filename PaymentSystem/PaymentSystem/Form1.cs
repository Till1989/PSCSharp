using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PaymentSystem
{
    public partial class Form1 : Form
    {
        Int64 paysystemID = -1;
        String name = "";

        Int64 senderBankID = -1;
        Int64 recipientBankID = -1;

        Byte[] bankInputBuffer = new Byte[27];
        Byte[] bankOutputBuffer = new Byte[27];


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createPaymentSystem();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            OleDbConnection dbConnection = new OleDbConnection(SetProvider("PaymentSystems.accdb"));
            OleDbCommand command;

            dbConnection.Open();
            command = new OleDbCommand("UPDATE PaymentSystemsData SET IsCreated=False WHERE ID=" + Convert.ToString(paysystemID), dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();




            bankPort0.Close();
            while (bankPort0.IsOpen == true) ;
            bankPort1.Close();
            while (bankPort1.IsOpen == true) ;
            bankPort2.Close();
            while (bankPort2.IsOpen == true) ;
        }

        private bool checkIsCreated(string ID)
        {
            bool result = false;
            OleDbConnection dbConnection = new OleDbConnection(SetProvider("PaymentSystems.accdb"));
            OleDbCommand command;

            dbConnection.Open();
            command = new OleDbCommand("SELECT IsCreated FROM PaymentSystemsData WHERE ID=" + ID, dbConnection);
            String tmp = "";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tmp = reader[0].ToString();
            }
            dbConnection.Close();
            if (tmp == "True")
            {
                result = true;
            }
            if (tmp == "False")
            {
                result = false;
            }


            return result;
        }
        private void createPaymentSystem()
        {
            OleDbConnection dbConnection = new OleDbConnection(SetProvider("PaymentSystems.accdb"));
            OleDbCommand command;

            dbConnection.Open();
            command = new OleDbCommand("SELECT ID FROM PaymentSystemsData", dbConnection);
            String[] PaymentSystemsIDRemp = new String[100];
            int tmp = 0;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PaymentSystemsIDRemp[tmp] = reader[0].ToString();
                tmp++;
            }
            dbConnection.Close();



            for (int i = 0; i < PaymentSystemsIDRemp.Length; i++)
            {
                if (PaymentSystemsIDRemp[i] != null)
                {
                    if (!checkIsCreated(PaymentSystemsIDRemp[i]))
                    {
                        dbConnection.Open();
                        command = new OleDbCommand("SELECT * FROM PaymentSystemsData WHERE ID=" + PaymentSystemsIDRemp[i], dbConnection);
                        String data = "";
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            data = reader[0].ToString() + "_" + reader[1].ToString() + "_" + reader[2].ToString() + "_" + reader[3].ToString() + "_" + reader[4].ToString();
                        }
                        dbConnection.Close();

                        String[] PaymentSystemRegData = data.Split('_');

                        
                        name = PaymentSystemRegData[1];
                        paysystemID = Convert.ToInt64(PaymentSystemRegData[0]);
                        if (PaymentSystemRegData[2] != "")
                        {
                            bankPort0.PortName = PaymentSystemRegData[2];
                            bankPort0.Open();
                        }
                        if (PaymentSystemRegData[3] != "")
                        {
                            bankPort1.PortName = PaymentSystemRegData[3];
                            bankPort1.Open();
                        }
                        if (PaymentSystemRegData[4] != "")
                        {
                            bankPort2.PortName = PaymentSystemRegData[4];
                            bankPort2.Open();
                        }

                        PSNameLabel.Text = "Name:" + name;
                        this.Text = name;
                        PSIDLabel.Text = "ID:" + Convert.ToString(paysystemID);
                        i = PaymentSystemsIDRemp.Length;
                    }
                }
                else
                {
                    this.Close();
                }
            }

            dbConnection.Open();
            command = new OleDbCommand("UPDATE PaymentSystemsData SET IsCreated=True WHERE ID=" + Convert.ToString(paysystemID), dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        private string SetProvider(string DataBaseName)
        {
            String[] temp = new String[1];
            temp = Environment.CurrentDirectory.Split('\\');
            String tmp = "";
            for (int i = 0; i < temp.Length - 4; i++)
            {
                tmp += temp[i];
                tmp += "\\";
            }
            tmp += "DataBases\\";
            tmp += DataBaseName;
            String provider = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + tmp;

            return provider;
        }
        private bool checkSanctions(Int64 senderBankID, Int64 recipientBankID)
        {
            bool result = false;

            OleDbConnection dbConnection = new OleDbConnection(SetProvider("Sanctions.accdb"));
            OleDbCommand command;

            

            dbConnection.Open();
            command = new OleDbCommand("SELECT Avaliability FROM Sanctions WHERE Recipient=" + recipientBankID + " AND Sender=" + senderBankID, dbConnection);
            String tempResult = "";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tempResult = reader[0].ToString();
            }
            dbConnection.Close();

            if(tempResult=="True")
            {
                result = true;
            }
            if (tempResult == "False")
            {
                result = false;
            }


            return result;
        }


        private void bank0Processing()
        {
            if (checkCRC(bankInputBuffer))
            {
                Int64 command = Convert.ToInt64(bankInputBuffer[24]);

                if (command == 0)
                {
                    logTextBox.Text += ("Received request from Bank0" + "\r\n");


                    bankOutputBuffer = bankInputBuffer;


                    senderBankID = Convert.ToInt64(bankInputBuffer[6]);
                    recipientBankID = Convert.ToInt64(bankInputBuffer[14]);





                    if(checkSanctions(senderBankID,recipientBankID))
                    {
                        if (senderBankID == 0)
                        {
                            bankPort0.Write(bankOutputBuffer, 0, 27);
                            logTextBox.Text += ("Sent request to Bank0" + "\r\n");
                        }
                        if (senderBankID == 1)
                        {
                            bankPort1.Write(bankOutputBuffer, 0, 27);
                            logTextBox.Text += ("Sent request to Bank1" + "\r\n");
                        }
                        if (senderBankID == 2)
                        {
                            bankPort2.Write(bankOutputBuffer, 0, 27);
                            logTextBox.Text += ("Sent request to Bank2" + "\r\n");
                        }
                    }
                    else
                    {
                        logTextBox.Text += ("Access denied!" + "\r\n");
                        

                        for(int i=0;i<bankOutputBuffer.Length;i++)
                        {
                            bankOutputBuffer[i] = 0;
                        }

                        bankOutputBuffer[0] = Convert.ToByte(4);
                        bankOutputBuffer[24] = Convert.ToByte(1);

                        Int64 CRC = 0;

                        for (int i = 0; i < 25; i++)
                        {
                            CRC += bankOutputBuffer[i];
                        }
                        bankOutputBuffer[25] = Convert.ToByte(CRC >> 8);
                        bankOutputBuffer[26] = Convert.ToByte(CRC);

                        bankPort0.Write(bankOutputBuffer, 0, 27);
                    }

                }
                else
                {
                    logTextBox.Text += ("Received result from Bank0" + "\r\n");
                    bankOutputBuffer[0] = bankInputBuffer[0];

                    Int64 CRC = 0;

                    for (int i = 0; i < 25; i++)
                    {
                        CRC += bankOutputBuffer[i];
                    }
                    bankOutputBuffer[25] = Convert.ToByte(CRC >> 8);
                    bankOutputBuffer[26] = Convert.ToByte(CRC);

                    if (recipientBankID == 0)
                    {
                        bankPort0.Write(bankOutputBuffer, 0, 27);
                        logTextBox.Text += ("Sent result to Bank0" + "\r\n");
                    }
                    if (recipientBankID == 1)
                    {
                        bankPort1.Write(bankOutputBuffer, 0, 27);
                        logTextBox.Text += ("Sent result to Bank1" + "\r\n");
                    }
                    if (recipientBankID == 2)
                    {
                        bankPort2.Write(bankOutputBuffer, 0, 27);
                        logTextBox.Text += ("Sent result to Bank2" + "\r\n");
                    }
                }
            }
        }
        private void bank1Processing()
        {
            if (checkCRC(bankInputBuffer))
            {
                Int64 command = Convert.ToInt64(bankInputBuffer[24]);

                if (command == 0)
                {
                    logTextBox.Text += ("Received request from Bank1" + "\r\n");
                    bankOutputBuffer = bankInputBuffer;


                    senderBankID = Convert.ToInt64(bankInputBuffer[6]);
                    recipientBankID = Convert.ToInt64(bankInputBuffer[14]);














                    if (checkSanctions(senderBankID, recipientBankID))
                    {
                        if (senderBankID == 0)
                        {
                            bankPort0.Write(bankOutputBuffer, 0, 27);
                            logTextBox.Text += ("Sent request to Bank0" + "\r\n");
                        }
                        if (senderBankID == 1)
                        {
                            bankPort1.Write(bankOutputBuffer, 0, 27);
                            logTextBox.Text += ("Sent request to Bank1" + "\r\n");
                        }
                        if (senderBankID == 2)
                        {
                            bankPort2.Write(bankOutputBuffer, 0, 27);
                            logTextBox.Text += ("Sent request to Bank2" + "\r\n");
                        }
                    }
                    else
                    {
                        logTextBox.Text += ("Access denied!" + "\r\n");
                        for (int i = 0; i < bankOutputBuffer.Length; i++)
                        {
                            bankOutputBuffer[i] = 0;
                        }

                        bankOutputBuffer[0] = Convert.ToByte(4);
                        bankOutputBuffer[24] = Convert.ToByte(1);

                        Int64 CRC = 0;

                        for (int i = 0; i < 25; i++)
                        {
                            CRC += bankOutputBuffer[i];
                        }
                        bankOutputBuffer[25] = Convert.ToByte(CRC >> 8);
                        bankOutputBuffer[26] = Convert.ToByte(CRC);

                        bankPort1.Write(bankOutputBuffer, 0, 27);
                    }





                }
                else
                {
                    logTextBox.Text += ("Received result from Bank1" + "\r\n");
                    bankOutputBuffer[0] = bankInputBuffer[0];

                    Int64 CRC = 0;

                    for (int i = 0; i < 25; i++)
                    {
                        CRC += bankOutputBuffer[i];
                    }
                    bankOutputBuffer[25] = Convert.ToByte(CRC >> 8);
                    bankOutputBuffer[26] = Convert.ToByte(CRC);

                    if (recipientBankID == 0)
                    {
                        bankPort0.Write(bankOutputBuffer, 0, 27);
                        logTextBox.Text += ("Sent result to Bank0" + "\r\n");
                    }
                    if (recipientBankID == 1)
                    {
                        bankPort1.Write(bankOutputBuffer, 0, 27);
                        logTextBox.Text += ("Sent result to Bank1" + "\r\n");
                    }
                    if (recipientBankID == 2)
                    {
                        bankPort2.Write(bankOutputBuffer, 0, 27);
                        logTextBox.Text += ("Sent result to Bank2" + "\r\n");
                    }
                }
            }
        }
        private void bank2Processing()
        {
            if (checkCRC(bankInputBuffer))
            {
                Int64 command = Convert.ToInt64(bankInputBuffer[24]);

                if (command == 0)
                {
                    logTextBox.Text += ("Received request from Bank2" + "\r\n");
                    bankOutputBuffer = bankInputBuffer;


                    senderBankID = Convert.ToInt64(bankInputBuffer[6]);
                    recipientBankID = Convert.ToInt64(bankInputBuffer[14]);

                    if (checkSanctions(senderBankID, recipientBankID))
                    {
                        if (senderBankID == 0)
                        {
                            bankPort0.Write(bankOutputBuffer, 0, 27);
                            logTextBox.Text += ("Sent request to Bank0" + "\r\n");
                        }
                        if (senderBankID == 1)
                        {
                            bankPort1.Write(bankOutputBuffer, 0, 27);
                            logTextBox.Text += ("Sent request to Bank1" + "\r\n");
                        }
                        if (senderBankID == 2)
                        {
                            bankPort2.Write(bankOutputBuffer, 0, 27);
                            logTextBox.Text += ("Sent request to Bank2" + "\r\n");
                        }
                    }
                    else
                    {
                        logTextBox.Text += ("Access denied!" + "\r\n");
                        for (int i = 0; i < bankOutputBuffer.Length; i++)
                        {
                            bankOutputBuffer[i] = 0;
                        }

                        bankOutputBuffer[0] = Convert.ToByte(4);
                        bankOutputBuffer[24] = Convert.ToByte(1);

                        Int64 CRC = 0;

                        for (int i = 0; i < 25; i++)
                        {
                            CRC += bankOutputBuffer[i];
                        }
                        bankOutputBuffer[25] = Convert.ToByte(CRC >> 8);
                        bankOutputBuffer[26] = Convert.ToByte(CRC);

                        bankPort2.Write(bankOutputBuffer, 0, 27);
                    }
                }
                else
                {
                    logTextBox.Text += ("Received result from Bank2" + "\r\n");
                    bankOutputBuffer[0] = bankInputBuffer[0];

                    Int64 CRC = 0;

                    for (int i = 0; i < 25; i++)
                    {
                        CRC += bankOutputBuffer[i];
                    }
                    bankOutputBuffer[25] = Convert.ToByte(CRC >> 8);
                    bankOutputBuffer[26] = Convert.ToByte(CRC);

                    if (recipientBankID == 0)
                    {
                        bankPort0.Write(bankOutputBuffer, 0, 27);
                        logTextBox.Text += ("Sent result to Bank0" + "\r\n");
                    }
                    if (recipientBankID == 1)
                    {
                        bankPort1.Write(bankOutputBuffer, 0, 27);
                        logTextBox.Text += ("Sent result to Bank1" + "\r\n");
                    }
                    if (recipientBankID == 2)
                    {
                        bankPort2.Write(bankOutputBuffer, 0, 27);
                        logTextBox.Text += ("Sent result to Bank2" + "\r\n");
                    }
                }
            }
        }


        private void bankPort0_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            bankPort0.Read(bankInputBuffer, 0, 27);

            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate
            {
                bank0Processing();
            }));
            else
            {
                bank0Processing();
            }
        }
        private void bankPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            bankPort1.Read(bankInputBuffer, 0, 27);

            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate
            {
                bank1Processing();
            }));
            else
            {
                bank1Processing();
            }

        }
        private void bankPort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            bankPort2.Read(bankInputBuffer, 0, 27);

            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate
            {
                bank2Processing();
            }));
            else
            {
                bank2Processing();
            }
        }


        private bool checkCRC(Byte[] data)
        {
            bool result;

            Int64 CRC = 0;
            int CRCTemp = 0;

            for (int i = 0; i < 25; i++)
            {
                CRC += data[i];
            }

            CRCTemp = data[25] << 8;
            CRCTemp |= data[26];
            if (CRC == CRCTemp)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        private void clearLogButton_Click(object sender, EventArgs e)
        {
            logTextBox.Text = "";
        }
    }
}


