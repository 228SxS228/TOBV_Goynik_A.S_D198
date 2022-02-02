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

namespace Goynik_D198_PR10_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        RSAParameters rsaParamsExcludePrivate;
        RSAParameters rsaParamsIncludePrivate;
        byte[] cipherbytes;
        byte[] plainbytes;

        private void GenerateNewRSAParams()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsaParamsIncludePrivate = rsa.ExportParameters(true);
            rsaParamsExcludePrivate = rsa.ExportParameters(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateNewRSAParams();
            string strRSA = BitConverter.ToString(rsaParamsIncludePrivate.P).Replace('-', ' ');
            textBox1.Text = strRSA;
            string strRSA2 = BitConverter.ToString(rsaParamsIncludePrivate.Q).Replace('-', ' ');
            textBox2.Text = strRSA2;
            string strRSA3 = BitConverter.ToString(rsaParamsIncludePrivate.Exponent).Replace('-', ' ');
            textBox3.Text = strRSA3;
            string strRSA4 = BitConverter.ToString(rsaParamsIncludePrivate.D);
            textBox4.Text = strRSA4;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParamsExcludePrivate);
            byte[] plainbytes = Encoding.UTF8.GetBytes(textBox5.Text);
            cipherbytes = rsa.Encrypt(plainbytes, false);
            string strE = Encoding.Default.GetString(cipherbytes);
            string strE16 = BitConverter.ToString(cipherbytes).Replace('-', ' ');
            textBox6.Text = strE;
            textBox7.Text = strE16;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParamsIncludePrivate);
            plainbytes = rsa.Decrypt(cipherbytes, false);
            string strD = Encoding.Default.GetString(plainbytes);
            textBox8.Text = strD;
        }
    }
}
