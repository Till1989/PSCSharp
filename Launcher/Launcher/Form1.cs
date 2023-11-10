using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;

namespace Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }






        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            createBanks();
            createPaymentSystems();
            createSanctionsManager();
            this.Close();
        }



        private void createBanks()
        {
            OleDbConnection dbConnection = new OleDbConnection(SetProvider("Banks.accdb"));
            OleDbCommand command;

            dbConnection.Open();
            command = new OleDbCommand("SELECT * FROM BanksRegData WHERE IsCreated=False", dbConnection);
            String[] BanksIDTemp = new String[100];
            int tmp = 0;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                BanksIDTemp[tmp] = reader[1].ToString();
                tmp++;
            }
            dbConnection.Close();

            String pathTempString = "";
            String[] pathTempStringArray = new String[100];
            String path = "";
            pathTempString = Environment.CurrentDirectory;
            pathTempStringArray = pathTempString.Split('\\');

            for(int i=0;i<pathTempStringArray.Length-4;i++)
            {
                path += pathTempStringArray[i]+"\\";
            }
            path += "Bank\\Bank\\bin\\Debug\\Bank.exe";

            for (int i=0;i<BanksIDTemp.Length;i++)
            {
                if(BanksIDTemp[i]!=null)
                {
                    Delay(500);
                    startExecutable(path);
                }
            }
        }
        private void createPaymentSystems()
        {
            OleDbConnection dbConnection = new OleDbConnection(SetProvider("PaymentSystems.accdb"));
            OleDbCommand command;

            dbConnection.Open();
            command = new OleDbCommand("SELECT * FROM PaymentSystemsData WHERE IsCreated=False", dbConnection);
            String[] PaymentSystemsIDTemp = new String[100];
            int tmp = 0;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PaymentSystemsIDTemp[tmp] = reader[0].ToString();
                tmp++;
            }
            dbConnection.Close();

            String pathTempString = "";
            String[] pathTempStringArray = new String[100];
            String path = "";
            pathTempString = Environment.CurrentDirectory;
            pathTempStringArray = pathTempString.Split('\\');

            for (int i = 0; i < pathTempStringArray.Length - 4; i++)
            {
                path += pathTempStringArray[i] + "\\";
            }
            path += "PaymentSystem\\PaymentSystem\\bin\\Debug\\PaymentSystem.exe";

            for (int i = 0; i < PaymentSystemsIDTemp.Length; i++)
            {
                if (PaymentSystemsIDTemp[i] != null)
                {
                    Delay(500);
                    startExecutable(path);
                }
            }
        }
        private void createSanctionsManager()
        {
            String pathTempString = "";
            String[] pathTempStringArray = new String[100];
            String path = "";
            pathTempString = Environment.CurrentDirectory;
            pathTempStringArray = pathTempString.Split('\\');

            for (int i = 0; i < pathTempStringArray.Length - 4; i++)
            {
                path += pathTempStringArray[i] + "\\";
            }
            path += "SanctionManager\\SanctionManager\\bin\\Debug\\SanctionManager.exe";

            bool processIsCreated = Process.GetProcessesByName("SanctionManager").Any() ? true : false;

            if (!processIsCreated)
            {
                startExecutable(path);
            }


            
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
        private void startExecutable(string path)
        {
            Process proc = new Process();
            ProcessStartInfo procStartInfo = new ProcessStartInfo();
            procStartInfo.UseShellExecute = true;
            procStartInfo.FileName = path;
            proc.StartInfo = procStartInfo;

            try
            {
                proc.Start();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void Delay(int value)
        {
            System.Threading.Thread.Sleep(value);
        }


    }
}
