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
using System.IO;
using System.Security.Cryptography;

namespace db_Goynik_D198
{
    public partial class Form1 : Form
    {
        string str;
        byte[] key;
        byte[] te;
        byte[] cipherbytes;
        SymmetricAlgorithm sa;
        string Key;

        public Form1()
        {
            InitializeComponent();
        }

        public void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //err
                if (radioButton9.Checked == true && radioButton12.Checked == true)
                {
                    MessageBox.Show("Заданный режим шифрования недопустим для этого алгоритма");
                    radioButton5.Checked = true;
                    sa.Mode = CipherMode.ECB;
                }

                //Symetric Algoritm
                if (radioButton1.Checked == true)
                {
                    sa = DES.Create();
                    sa.GenerateKey();
                    key = sa.Key;
                    Key = Encoding.Default.GetString(sa.Key);
                }
                else if (radioButton2.Checked == true)
                {
                    sa = TripleDES.Create();
                    sa.GenerateKey();
                    key = sa.Key;
                    Key = Encoding.Default.GetString(sa.Key);
                }
                else if (radioButton3.Checked == true)
                {
                    sa = Rijndael.Create();
                    sa.GenerateKey();
                    key = sa.Key;
                    Key = Encoding.Default.GetString(sa.Key);
                }
                else if (radioButton4.Checked == true)
                {
                    sa = RC2.Create();
                    sa.GenerateKey();
                    key = sa.Key;
                    Key = Encoding.Default.GetString(sa.Key);
                }

                //Mode
                if (radioButton5.Checked == true)
                {
                    sa.Mode = CipherMode.ECB;
                }
                else if (radioButton6.Checked == true)
                {
                    sa.Mode = CipherMode.CBC;
                }
                else if (radioButton7.Checked == true)
                {
                    sa.Mode = CipherMode.CFB;
                }
                else if (radioButton8.Checked == true)
                {
                    sa.Mode = CipherMode.OFB;
                }
                else if (radioButton9.Checked == true)
                {
                    sa.Mode = CipherMode.CTS;
                }


                //Padding
                if (radioButton14.Checked == true)
                {
                    sa.Padding = PaddingMode.PKCS7;
                }
                else if (radioButton13.Checked == true)
                {
                    sa.Padding = PaddingMode.Zeros;
                }
                else if (radioButton12.Checked == true)
                {
                    sa.Padding = PaddingMode.None;
                }
            }
            catch
            {
                MessageBox.Show("Неверное сочетание"); 
            }
            

            byte[] b16 = Encoding.Default.GetBytes(Key);   
            string b16S = BitConverter.ToString(b16);
            textBox5.Text = b16S;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string conn_param = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\goyni\Desktop\db_Goynik_D198";
            OleDbConnection connection = new OleDbConnection(conn_param);
            OleDbCommand command = connection.CreateCommand();

            connection.Open();

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] plainbytes = Encoding.Default.GetBytes(textBox1.Text);
            cs.Write(plainbytes, 0, plainbytes.Length);
            cs.Close();
            cipherbytes = ms.ToArray();
            ms.Close();
            string str = Encoding.Default.GetString(cipherbytes);

            command.CommandText = "select max(id) + 1 from DaaBase";
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            int id = Convert.ToInt32(reader.GetValue(0));
            reader.Close();



            command.CommandText = "INSERT INTO DaaBase VALUES('" + textBox1.Text + "'," + id + ",'" + str +"');";
            command.ExecuteNonQuery();

            command.CommandText = "select reh from DaaBase where id = " + id;
            reader = command.ExecuteReader();
            reader.Read();
            string strE = reader.GetString(0);
            textBox4.Text = strE;
            reader.Close();

            byte[] b16 = Encoding.Default.GetBytes(strE);
            string b16S = BitConverter.ToString(b16);

            textBox3.Text = b16S;

            connection.Close();

            MessageBox.Show("The word has been encrypted");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string conn_param = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\goyni\Desktop\db_Goynik_D198";
            OleDbConnection connection = new OleDbConnection(conn_param);
            OleDbCommand command = connection.CreateCommand();
            OleDbDataReader reader;

            connection.Open();

            command.CommandText = "select max(id) from DaaBase";
            reader = command.ExecuteReader();
            reader.Read();
            int id = Convert.ToInt32(reader.GetValue(0));
            reader.Close();

            command.CommandText = "select reh from DaaBase where id = " + id;
            reader = command.ExecuteReader();
            reader.Read();
            string str = reader.GetString(0);
            textBox2.Text = str;
            reader.Close();

            

            /*StringBuilder sb = new StringBuilder();
            foreach (var b in cipherbytes)
            {
                sb.Append(b);
            }
            string outVAR = sb.ToString();*/

            connection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

    

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
