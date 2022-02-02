using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Goynik_PR10_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[] cipherbytes;

        private void GenerateNewRSAParams()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            StreamWriter writer = new StreamWriter("D:/work/Стариков_ПР10_2/PublicPrivateKey.xml");
            string publicPrivateKeyXML = rsa.ToXmlString(true);
            writer.Write(publicPrivateKeyXML);
            writer.Close();

            writer = new StreamWriter("D:/work/Стариков_ПР10_2/PublicOnlyKey.xml");
            string publicOnlyKeyXML = rsa.ToXmlString(false);
            writer.Write(publicOnlyKeyXML);
            writer.Close();
            textBox1.Text = publicPrivateKeyXML;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateNewRSAParams();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            StreamReader reader = new StreamReader("D:/work/Стариков_ПР10_2/PublicPrivateKey.xml");
            string publicPrivateKeyXML = reader.ReadToEnd();
            rsa.FromXmlString(publicPrivateKeyXML);
            byte[] plainbytes = Encoding.UTF8.GetBytes(textBox2.Text);
            reader.Close();
            cipherbytes = rsa.Encrypt(plainbytes, false);
            string str = Encoding.Default.GetString(cipherbytes);
            textBox3.Text = str;
            byte[] b16 = Encoding.Default.GetBytes(str);
            string str16 = BitConverter.ToString(b16).Replace('-', ' ');
            textBox4.Text = str16;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            StreamReader reader = new StreamReader("D:/work/Стариков_ПР10_2/PublicPrivateKey.xml");
            string publicPrivateKeyXML = reader.ReadToEnd();
            rsa.FromXmlString(publicPrivateKeyXML);
            reader.Close();
            byte[] plainbytes = rsa.Decrypt(cipherbytes, false);
            string str = Encoding.Default.GetString(plainbytes);
            textBox5.Text = str;
        }
    }
}
