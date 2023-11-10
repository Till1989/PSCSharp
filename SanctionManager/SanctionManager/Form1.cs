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

namespace SanctionManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String BankProvider = "";
        String SanctionsProvider = "";


        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadData();
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

        private void reloadDataButton_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void SenderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadAvailability();
        }

        private void RecipientComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadAvailability();
        }

        private void availabilityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetAvailability();
        }
        private void ReloadData()
        {
            SenderComboBox.Items.Clear();
            RecipientComboBox.Items.Clear();

            OleDbConnection dbConnection = new OleDbConnection(SetProvider("Banks.accdb"));
            OleDbCommand command;

            dbConnection.Open();
            command = new OleDbCommand("SELECT BankName, BankID FROM BanksRegData", dbConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SenderComboBox.Items.Add(reader[0].ToString() + "_" + reader[1].ToString());
                RecipientComboBox.Items.Add(reader[0].ToString() + "_" + reader[1].ToString());
            }
            dbConnection.Close();
        }
        private void ReadAvailability()
        {
            if (SenderComboBox.Text!="" && RecipientComboBox.Text!="")
            {
                String sender = "";
                String recipient = "";
                String[] temp = new String[2];
                String result = "";

                temp = SenderComboBox.Text.Split('_');
                sender = temp[1];
                temp = RecipientComboBox.Text.Split('_');
                recipient = temp[1];

                OleDbConnection dbConnection = new OleDbConnection(SetProvider("Sanctions.accdb"));
                OleDbCommand command;

                dbConnection.Open();
                command = new OleDbCommand("SELECT Avaliability FROM Sanctions WHERE Sender=" + sender + " AND Recipient=" + recipient, dbConnection);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = reader[0].ToString();
                }
                if(result == "True")
                {
                    availabilityCheckBox.Checked = true;
                }
                if(result == "False")
                {
                    availabilityCheckBox.Checked = false;
                }
                dbConnection.Close();

            }
        }
        private void SetAvailability()
        {
            if (SenderComboBox.Text != "" && RecipientComboBox.Text != "")
            {
                Int64 sender = 0;
                Int64 recipient = 0;
                String[] temp = new String[2];
                String availability = "";
              

                temp = SenderComboBox.Text.Split('_');
                sender = Convert.ToInt64(temp[1]);
                temp = RecipientComboBox.Text.Split('_');
                recipient = Convert.ToInt64(temp[1]);
                availability = Convert.ToString(availabilityCheckBox.Checked);

                OleDbConnection dbConnection = new OleDbConnection(SetProvider("Sanctions.accdb"));
                OleDbCommand command;

                dbConnection.Open();
                command = new OleDbCommand("UPDATE Sanctions SET Avaliability="+availability+" WHERE Sender="+sender+" AND Recipient="+recipient, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();

            }
        }
    }
}
