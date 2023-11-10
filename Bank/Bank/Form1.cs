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
using System.Data.OleDb;

namespace Bank
{
    public partial class Form1 : Form
    {
        String name;
        String regCountry;
        Int32 bankID;
        Card[] cards=new Card[10];
        Balance[] balances=new Balance[10];
        Customer[] customers=new Customer[10];




        Int64 senderCardNumber = 0;
        Int64 recipientCardNumber = 0;
        Int64 amount = 0;
        Byte checkBalanceResult = 0;


        Byte[] terminalInputBuffer = new Byte[27];
        Byte[] terminalOutputBuffer = new Byte[1];

        Byte[] paySystemInputBuffer = new Byte[27];
        Byte[] paySystemOutputBuffer = new Byte[27];





        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createBank();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            OleDbConnection dbConnection = new OleDbConnection(SetProvider("Banks.accdb"));
            OleDbCommand command;

            dbConnection.Open();
            command = new OleDbCommand("UPDATE BanksRegData SET IsCreated=False WHERE BankID=" + Convert.ToString(bankID), dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();


            terminalPort.Close();
            while (terminalPort.IsOpen == true) ;
            paySystem0Port.Close();
            while (paySystem0Port.IsOpen == true) ;
            paySystem1Port.Close();
            while (paySystem1Port.IsOpen == true) ;
            paySystem2Port.Close();
            while (paySystem2Port.IsOpen == true) ;
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
        private bool checkIsCreated(string ID)
        {
            bool result = false;
            OleDbConnection dbConnection = new OleDbConnection(SetProvider("Banks.accdb"));
            OleDbCommand command;

            dbConnection.Open();
            command = new OleDbCommand("SELECT IsCreated FROM BanksRegData WHERE BankID=" + ID, dbConnection);
            String tmp = "";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tmp = reader[0].ToString();
            }
            dbConnection.Close();
            if(tmp=="True")
            {
                result = true;
            }
            if(tmp=="False")
            {
                result = false;
            }


            return result;
        }
        private void createBank()
        {
            OleDbConnection dbConnection = new OleDbConnection(SetProvider("Banks.accdb"));
            OleDbCommand command;

            dbConnection.Open();
            command = new OleDbCommand("SELECT BankID FROM BanksRegData", dbConnection);
            String[] banksIDTemp = new String[100];
            int tmp = 0;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                banksIDTemp[tmp] = reader[0].ToString();
                tmp++;
            }
            dbConnection.Close();



            for(int i=0;i<banksIDTemp.Length;i++)
            {
                if(banksIDTemp[i]!=null)
                {
                    if(!checkIsCreated(banksIDTemp[i]))
                    {
                        dbConnection.Open();
                        command = new OleDbCommand("SELECT * FROM BanksRegData WHERE BankID=" + banksIDTemp[i], dbConnection);
                        String data = "";
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            data = reader[0].ToString() + "_" + reader[1].ToString() + "_" + reader[2].ToString() + "_" + reader[4].ToString() + "_" + reader[5].ToString() + "_" + reader[6].ToString() + "_" + reader[7].ToString();
                        }
                        dbConnection.Close();

                        String[] bankRegData = data.Split('_');


                        name = bankRegData[2];
                        regCountry = bankRegData[0];
                        bankID = Convert.ToInt32(bankRegData[1]);
                        if(bankRegData[6]!="")
                        {
                            terminalPort.PortName = bankRegData[6];
                            terminalPort.Open();
                        }
                        if (bankRegData[3] != "")
                        {
                            paySystem0Port.PortName = bankRegData[3];
                            paySystem0Port.Open();
                        }
                        if (bankRegData[4] != "")
                        {
                            paySystem1Port.PortName = bankRegData[4];
                            paySystem1Port.Open();
                        }
                        if (bankRegData[5] != "")
                        {
                            paySystem2Port.PortName = bankRegData[5];
                            paySystem2Port.Open();
                        }

                        bankNameLabel.Text = "Name:" + name;
                        bankRegCountryLabel.Text = "RegCountry:" + regCountry;
                        bankIDLabel.Text = "ID:" + Convert.ToString(bankID);
                        this.Text = Convert.ToString(name);


                        switch (bankID)
                        {
                            case 0:
                                customers[searchCustomersFirstNullIndex(customers)] = new Business("fghffghnfh", 0, 12423423);
                                cards[0] = new Card(824846385153, 444, "4444", 121, 0);
                                balances[0] = new Balance(Convert.ToString(cards[0].number) + "_" + Convert.ToString(cards[0].cvv) + "_" + cards[0].expDate, 10000);
                                break;
                            case 1:
                                customers[searchCustomersFirstNullIndex(customers)] = new Person("Efgvf Ozvfv", 0, "lj976896", 6856587);
                                cards[0] = new Card(688675094784, 111, "1111", 40, 0);
                                balances[0] = new Balance(Convert.ToString(cards[0].number) + "_" + Convert.ToString(cards[0].cvv) + "_" + cards[0].expDate, 10000);
                                break;
                            case 2:
                                customers[searchCustomersFirstNullIndex(customers)] = new Person("7ше расотпр", 1, "ttttkhj", 5675);
                                cards[0] = new Card(499221201410, 222, "2222", 2, 0);
                                balances[0] = new Balance(Convert.ToString(cards[0].number) + "_" + Convert.ToString(cards[0].cvv) + "_" + cards[0].expDate, 10);
                                break;
                            default: break;
                        }
                        i = banksIDTemp.Length;
                    }

                }
                else
                {
                    this.Close();
                }
                
            }

            dbConnection.Open();
            command = new OleDbCommand("UPDATE BanksRegData SET IsCreated=True WHERE BankID=" + Convert.ToString(bankID), dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();

            timer1.Enabled = true;

        }


        private int searchCustomersFirstNullIndex(Customer[] customers)
        {
            Int32 index = 0;
            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i] == null)
                {
                    index = i;
                    i = customers.Length;
                }
            }
            return index;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            refreshInfo();
        }
        public void refreshInfo()
        {
            infoTextBox.Text = "";
            int index = 0;
            for(int i=0;i<balances.Length;i++)
            {
                if(balances[i]==null)
                {
                    index = i;
                    i = balances.Length;
                }
            }
            for(int i=0;i<index;i++)
            {
                infoTextBox.Text += balances[i].data + "::::" + Convert.ToString(balances[i].value) + "\r\n";
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
        private Byte checkBalance(Int64 cardNumber, Int64 amount)
        {

            for (int i = 0; i < balances.Length; i++)
            {

                String[] temp = new String[3];
                temp = balances[i].data.Split('_');

                Int64 tmp = 0;
                tmp = Convert.ToInt64(temp[0]);

                if (tmp == cardNumber)
                {
                    if (balances[i].value >= amount)
                    {
                        checkBalanceResult = 1;
                    }
                    else
                    {
                        checkBalanceResult = 2;
                    }
                    i = balances.Length;
                }
            }
            if (checkBalanceResult == 1)
            {
                logTextBox.Text += ("Balance OK." + "\r\n");
            }
            else
            {
                logTextBox.Text += ("Balance not OK." + "\r\n");
            }

            return checkBalanceResult;
        }
        private void balanceEdit(Int64 cardNumber, int command, Int64 amount)
        {
            if(command==0)
            {
                for (int i = 0; i < balances.Length; i++)
                {

                    String[] temp = new String[3];
                    temp = balances[i].data.Split('_');

                    Int64 tmp = 0;
                    tmp = Convert.ToInt64(temp[0]);

                    if (tmp == cardNumber)
                    {
                        balances[i].value -= amount;

                        i = balances.Length;
                    }
                }
            }
            if(command==1)
            {
                for (int i = 0; i < balances.Length; i++)
                {

                    String[] temp = new String[3];
                    temp = balances[i].data.Split('_');

                    Int64 tmp = 0;
                    tmp = Convert.ToInt64(temp[0]);

                    if (tmp == cardNumber)
                    {
                        balances[i].value += amount;

                        i = balances.Length;
                    }
                }
            }

        }




        private void terminalPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            terminalPort.Read(terminalInputBuffer, 0, 27);

            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate
            {
                inputIndicator.BackColor = System.Drawing.Color.Lime;
                inputBlinkTimer.Enabled = true;
                terminalReceivedProcessing();

            }));
            else
            {
                inputIndicator.BackColor = System.Drawing.Color.Lime;
                inputBlinkTimer.Enabled = true;
                terminalReceivedProcessing();
            }

        }
        public void returnResultToTerminal(Byte data)
        {
            Byte[] buffer = new Byte[1];
            buffer[0] = data;
            terminalPort.Write(buffer, 0, 1);
        }
        private void paySystem0Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            paySystem0Port.Read(paySystemInputBuffer, 0, 27);

            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate
            {
                paymentSystem0Processing();
            }));
            else
            {
                paymentSystem0Processing();
            }
        }
        private void paySystem1Port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            paySystem1Port.Read(paySystemInputBuffer, 0, 27);

            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate
            {
                paymentSystem1Processing();
            }));
            else
            {
                paymentSystem1Processing();
            }
        }
        private void paySystem2Port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            paySystem2Port.Read(paySystemInputBuffer, 0, 27);

            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate
            {
                paymentSystem2Processing();
            }));
            else
            {
                paymentSystem2Processing();
            }
        }



        private void terminalReceivedProcessing()
        {
            if (checkCRC(terminalInputBuffer))
            {
                logTextBox.Text += ("Received from terminal." + "\r\n");


                Byte[] amountTemp = new Byte[8];
                amountTemp[0] = terminalInputBuffer[23];
                amountTemp[1] = terminalInputBuffer[22];
                amountTemp[2] = terminalInputBuffer[21];
                amountTemp[3] = terminalInputBuffer[20];
                amountTemp[4] = terminalInputBuffer[19];
                amountTemp[5] = terminalInputBuffer[18];
                amountTemp[6] = terminalInputBuffer[17];
                amountTemp[7] = terminalInputBuffer[16];
                while (amountTemp[0] == 11)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 7)
                        {
                            amountTemp[i] = amountTemp[i + 1];
                        }
                        else
                        {
                            amountTemp[i] = 0;
                        }
                    }
                }

                for (Int16 i = 7; i > -1; i--)
                {
                    if (amountTemp[i] == 0)
                    {
                        amountTemp[i] = 11;
                    }
                    else
                    {
                        i = -1;
                    }
                }
                amount = 0;
                if (amountTemp[7] != 11)
                {
                    amount += Convert.ToInt64(amountTemp[7]) * 10000000;
                }
                if (amountTemp[6] != 11)
                {
                    amount += Convert.ToInt64(amountTemp[6]) * 1000000;
                }
                if (amountTemp[5] != 11)
                {
                    amount += Convert.ToInt64(amountTemp[5]) * 100000;
                }
                if (amountTemp[4] != 11)
                {
                    amount += Convert.ToInt64(amountTemp[4]) * 10000;
                }
                if (amountTemp[3] != 11)
                {
                    amount += Convert.ToInt64(amountTemp[3]) * 1000;
                }
                if (amountTemp[2] != 11)
                {
                    amount += Convert.ToInt64(amountTemp[2]) * 100;
                }
                if (amountTemp[1] != 11)
                {
                    amount += Convert.ToInt64(amountTemp[1]) * 10;
                }
                if (amountTemp[0] != 11)
                {
                    amount += Convert.ToInt64(amountTemp[0]) * 1;
                }

                Byte[] temp = new Byte[8];
                for (int i = 0; i < 8; i++)
                {
                    temp[i] = terminalInputBuffer[15 - i];
                }
                recipientCardNumber = BitConverter.ToInt64(temp, 0);

                for (int i = 0; i < 27; i++)
                {
                    paySystemOutputBuffer[i] = terminalInputBuffer[i];
                }

                paySystemOutputBuffer[24] = 0;
                Int64 CRCTemp = 0;
                for (int i = 0; i < 25; i++)
                {
                    CRCTemp += paySystemOutputBuffer[i];
                }
                paySystemOutputBuffer[25] = Convert.ToByte(CRCTemp >> 8);
                paySystemOutputBuffer[26] = Convert.ToByte(CRCTemp & 255);

                Int64 paySystemID = Convert.ToInt64(terminalInputBuffer[7]);

                if (paySystemID == 0)
                {
                    paySystem0Port.Write(paySystemOutputBuffer, 0, 27);
                    logTextBox.Text += ("Sent to PaymentSystem0." + "\r\n");
                }
                if (paySystemID == 1)
                {
                    paySystem1Port.Write(paySystemOutputBuffer, 0, 27);
                    logTextBox.Text += ("Sent to PaymentSystem1." + "\r\n");
                }
                if (paySystemID == 2)
                {
                    paySystem2Port.Write(paySystemOutputBuffer, 0, 27);
                    logTextBox.Text += ("Sent to PaymentSystem2." + "\r\n");
                }
            }
            else
            {
                terminalOutputBuffer[0] = Convert.ToByte(3);
                terminalPort.Write(terminalOutputBuffer, 0, 1);
            }
        }
        private void paymentSystem0Processing()
        {
            inputIndicator.BackColor = System.Drawing.Color.Lime;
            inputBlinkTimer.Enabled = true;

            if (checkCRC(paySystemInputBuffer))
            {
                int command = Convert.ToInt32(paySystemInputBuffer[24]);
                if (command == 0)
                {
                    logTextBox.Text += ("Received request from PaymentSystem0." + "\r\n");
                    Byte[] temp = new Byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        temp[i] = paySystemInputBuffer[7 - i];
                    }
                    senderCardNumber = BitConverter.ToInt64(temp, 0);



                    Byte[] amountTemp = new Byte[8];
                    amountTemp[0] = paySystemInputBuffer[23];
                    amountTemp[1] = paySystemInputBuffer[22];
                    amountTemp[2] = paySystemInputBuffer[21];
                    amountTemp[3] = paySystemInputBuffer[20];
                    amountTemp[4] = paySystemInputBuffer[19];
                    amountTemp[5] = paySystemInputBuffer[18];
                    amountTemp[6] = paySystemInputBuffer[17];
                    amountTemp[7] = paySystemInputBuffer[16];
                    while (amountTemp[0] == 11)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (i < 7)
                            {
                                amountTemp[i] = amountTemp[i + 1];
                            }
                            else
                            {
                                amountTemp[i] = 0;
                            }
                        }
                    }

                    for (Int16 i = 7; i > -1; i--)
                    {
                        if (amountTemp[i] == 0)
                        {
                            amountTemp[i] = 11;
                        }
                        else
                        {
                            i = -1;
                        }
                    }
                    amount = 0;
                    if (amountTemp[7] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[7]) * 10000000;
                    }
                    if (amountTemp[6] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[6]) * 1000000;
                    }
                    if (amountTemp[5] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[5]) * 100000;
                    }
                    if (amountTemp[4] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[4]) * 10000;
                    }
                    if (amountTemp[3] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[3]) * 1000;
                    }
                    if (amountTemp[2] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[2]) * 100;
                    }
                    if (amountTemp[1] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[1]) * 10;
                    }
                    if (amountTemp[0] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[0]) * 1;
                    }

                    for (int i = 0; i < 27; i++)
                    {
                        paySystemOutputBuffer[i] = 0;
                    }

                    int balanceResult = 0;

                    balanceResult = checkBalance(senderCardNumber, amount);

                    if (balanceResult == 1)
                    {
                        balanceEdit(senderCardNumber, 0, amount);
                    }


                    paySystemOutputBuffer[0] = Convert.ToByte(balanceResult);



                    paySystemOutputBuffer[24] = Convert.ToByte(1);
                    int CRCTemp = 0;
                    for (int i = 0; i < 25; i++)
                    {
                        CRCTemp += paySystemOutputBuffer[i];
                    }
                    paySystemOutputBuffer[25] = Convert.ToByte(CRCTemp >> 8);
                    paySystemOutputBuffer[26] = Convert.ToByte(CRCTemp & 255);

                    outputIndicator.BackColor = System.Drawing.Color.Lime;
                    outputBlinkTimer.Enabled = true;

                    paySystem0Port.Write(paySystemOutputBuffer, 0, 27);
                    logTextBox.Text += ("Sent result to PaySystem0." + "\r\n");
                }
                else
                {
                    outputIndicator.BackColor = System.Drawing.Color.Lime;
                    outputBlinkTimer.Enabled = true;

                    logTextBox.Text += ("Received result from PaymentSystem0." + "\r\n");

                    if (paySystemInputBuffer[0] == 1)
                    {
                        balanceEdit(recipientCardNumber, 1, amount);
                    }

                    logTextBox.Text += ("Sent result to terminal." + "\r\n");

                    returnResultToTerminal(paySystemInputBuffer[0]);
                }
            }
        }
        private void paymentSystem1Processing()
        {
            inputIndicator.BackColor = System.Drawing.Color.Lime;
            inputBlinkTimer.Enabled = true;

            if (checkCRC(paySystemInputBuffer))
            {
                int command = Convert.ToInt32(paySystemInputBuffer[24]);
                if (command == 0)
                {
                    logTextBox.Text += ("Received request from PaymentSystem1." + "\r\n");
                    Byte[] temp = new Byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        temp[i] = paySystemInputBuffer[7 - i];
                    }
                    senderCardNumber = BitConverter.ToInt64(temp, 0);



                    Byte[] amountTemp = new Byte[8];
                    amountTemp[0] = paySystemInputBuffer[23];
                    amountTemp[1] = paySystemInputBuffer[22];
                    amountTemp[2] = paySystemInputBuffer[21];
                    amountTemp[3] = paySystemInputBuffer[20];
                    amountTemp[4] = paySystemInputBuffer[19];
                    amountTemp[5] = paySystemInputBuffer[18];
                    amountTemp[6] = paySystemInputBuffer[17];
                    amountTemp[7] = paySystemInputBuffer[16];
                    while (amountTemp[0] == 11)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (i < 7)
                            {
                                amountTemp[i] = amountTemp[i + 1];
                            }
                            else
                            {
                                amountTemp[i] = 0;
                            }
                        }
                    }

                    for (Int16 i = 7; i > -1; i--)
                    {
                        if (amountTemp[i] == 0)
                        {
                            amountTemp[i] = 11;
                        }
                        else
                        {
                            i = -1;
                        }
                    }
                    amount = 0;
                    if (amountTemp[7] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[7]) * 10000000;
                    }
                    if (amountTemp[6] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[6]) * 1000000;
                    }
                    if (amountTemp[5] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[5]) * 100000;
                    }
                    if (amountTemp[4] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[4]) * 10000;
                    }
                    if (amountTemp[3] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[3]) * 1000;
                    }
                    if (amountTemp[2] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[2]) * 100;
                    }
                    if (amountTemp[1] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[1]) * 10;
                    }
                    if (amountTemp[0] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[0]) * 1;
                    }

                    for (int i = 0; i < 27; i++)
                    {
                        paySystemOutputBuffer[i] = 0;
                    }

                    int balanceResult = 0;

                    balanceResult = checkBalance(senderCardNumber, amount);

                    if (balanceResult == 1)
                    {
                        balanceEdit(senderCardNumber, 0, amount);
                    }


                    paySystemOutputBuffer[0] = Convert.ToByte(balanceResult);



                    paySystemOutputBuffer[24] = Convert.ToByte(1);
                    int CRCTemp = 0;
                    for (int i = 0; i < 25; i++)
                    {
                        CRCTemp += paySystemOutputBuffer[i];
                    }
                    paySystemOutputBuffer[25] = Convert.ToByte(CRCTemp >> 8);
                    paySystemOutputBuffer[26] = Convert.ToByte(CRCTemp & 255);

                    outputIndicator.BackColor = System.Drawing.Color.Lime;
                    outputBlinkTimer.Enabled = true;

                    paySystem1Port.Write(paySystemOutputBuffer, 0, 27);
                    logTextBox.Text += ("Sent result to PaySystem1." + "\r\n");
                }
                else
                {
                    outputIndicator.BackColor = System.Drawing.Color.Lime;
                    outputBlinkTimer.Enabled = true;

                    logTextBox.Text += ("Received result from PaymentSystem1." + "\r\n");

                    if (paySystemInputBuffer[0] == 1)
                    {
                        balanceEdit(recipientCardNumber, 1, amount);
                    }

                    logTextBox.Text += ("Sent result to terminal." + "\r\n");

                    returnResultToTerminal(paySystemInputBuffer[0]);
                }
            }
        }
        private void paymentSystem2Processing()
        {

            inputIndicator.BackColor = System.Drawing.Color.Lime;
            inputBlinkTimer.Enabled = true;

            if (checkCRC(paySystemInputBuffer))
            {
                int command = Convert.ToInt32(paySystemInputBuffer[24]);
                if (command == 0)
                {
                    logTextBox.Text += ("Received request from PaymentSystem2." + "\r\n");
                    Byte[] temp = new Byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        temp[i] = paySystemInputBuffer[7 - i];
                    }
                    senderCardNumber = BitConverter.ToInt64(temp, 0);

                    Byte[] amountTemp = new Byte[8];
                    amountTemp[0] = paySystemInputBuffer[23];
                    amountTemp[1] = paySystemInputBuffer[22];
                    amountTemp[2] = paySystemInputBuffer[21];
                    amountTemp[3] = paySystemInputBuffer[20];
                    amountTemp[4] = paySystemInputBuffer[19];
                    amountTemp[5] = paySystemInputBuffer[18];
                    amountTemp[6] = paySystemInputBuffer[17];
                    amountTemp[7] = paySystemInputBuffer[16];
                    while (amountTemp[0] == 11)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (i < 7)
                            {
                                amountTemp[i] = amountTemp[i + 1];
                            }
                            else
                            {
                                amountTemp[i] = 0;
                            }
                        }
                    }

                    for (Int16 i = 7; i > -1; i--)
                    {
                        if (amountTemp[i] == 0)
                        {
                            amountTemp[i] = 11;
                        }
                        else
                        {
                            i = -1;
                        }
                    }
                    amount = 0;
                    if (amountTemp[7] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[7]) * 10000000;
                    }
                    if (amountTemp[6] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[6]) * 1000000;
                    }
                    if (amountTemp[5] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[5]) * 100000;
                    }
                    if (amountTemp[4] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[4]) * 10000;
                    }
                    if (amountTemp[3] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[3]) * 1000;
                    }
                    if (amountTemp[2] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[2]) * 100;
                    }
                    if (amountTemp[1] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[1]) * 10;
                    }
                    if (amountTemp[0] != 11)
                    {
                        amount += Convert.ToInt64(amountTemp[0]) * 1;
                    }


                    for (int i = 0; i < 27; i++)
                    {
                        paySystemOutputBuffer[i] = 0;
                    }
                    int balanceResult = 0;

                    balanceResult = checkBalance(senderCardNumber, amount);

                    if (balanceResult == 1)
                    {
                        balanceEdit(senderCardNumber, 0, amount);
                    }


                    paySystemOutputBuffer[0] = Convert.ToByte(balanceResult);

                    paySystemOutputBuffer[24] = Convert.ToByte(1);

                    int CRCTemp = 0;
                    for (int i = 0; i < 25; i++)
                    {
                        CRCTemp += paySystemOutputBuffer[i];
                    }
                    paySystemOutputBuffer[25] = Convert.ToByte(CRCTemp >> 8);
                    paySystemOutputBuffer[26] = Convert.ToByte(CRCTemp & 255);

                    outputIndicator.BackColor = System.Drawing.Color.Lime;
                    outputBlinkTimer.Enabled = true;

                    paySystem2Port.Write(paySystemOutputBuffer, 0, 27);
                    logTextBox.Text += ("Sent result to PaySystem2." + "\r\n");
                }
                else
                {
                    outputIndicator.BackColor = System.Drawing.Color.Lime;
                    outputBlinkTimer.Enabled = true;

                    logTextBox.Text += ("Received result from PaymentSystem2." + "\r\n");

                    if (paySystemInputBuffer[0] == 1)
                    {
                        balanceEdit(recipientCardNumber, 1, amount);
                    }

                    returnResultToTerminal(paySystemInputBuffer[0]);
                    logTextBox.Text += ("Sent result to terminal." + "\r\n");
                }
            }
        }




        

        private void inputBlinkTimer_Tick(object sender, EventArgs e)
        {
            inputBlinkTimer.Enabled = false;

            inputIndicator.BackColor = System.Drawing.Color.Transparent;
        }
        private void outputBlinkTimer_Tick(object sender, EventArgs e)
        {
            outputBlinkTimer.Enabled = false;
            outputIndicator.BackColor = System.Drawing.Color.Transparent;
        }
        private void clearLogButton_Click(object sender, EventArgs e)
        {
            logTextBox.Text = "";
        }
    }


















    public class Card
    {
        public Int64 number;
        public int cvv;
        public String expDate;
        Int32 issuerBankID;
        Int32 customerID;

        public Card(Int64 number,int cvv,String expDate,Int32 issuerBankID,Int32 customerID)
        {
            this.number = number;
            this.cvv = cvv;
            this.expDate = expDate;
            this.issuerBankID = issuerBankID;
            this.customerID = customerID;
        }

    }

    public class Balance
    {
        public String data;
        public Int64 value;
        public Balance(String data,Int64 value)
        {
            this.data = data;
            this.value = value;
        }
    }

    public class Customer
    {
        public String name;
        public Int32 ID;
        public Customer(String name,Int32 ID)
        {
            this.name = name;
            this.ID = ID;
        }
    }

    public partial class Person : Customer
    {
        String pass;
        Int32 INN;
        public Person(String name, Int32 ID,String pass, Int32 INN) : base(name, ID)
        {
            this.pass = pass;
            this.INN = INN;
        }
    }

    public partial class Business : Customer
    {
        Int32 EDRPOU;
        public Business(String name, Int32 ID,Int32 EDRPOU):base(name,ID)
        {
            this.EDRPOU = EDRPOU;
        }
    }
}
