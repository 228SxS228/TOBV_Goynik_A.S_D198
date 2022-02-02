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

namespace Goynik_D198_PR11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DSAParameters dsaparams;
        byte[] signaturebytes;

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] messagebytes = Encoding.UTF8.GetBytes(textBox1.Text);
            SHA1 shal = new SHA1CryptoServiceProvider();
            byte[] hashbytes = shal.ComputeHash(messagebytes);
            string strHash = BitConverter.ToString(messagebytes).Replace('-', ' ');
            textBox2.Text = strHash;
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
            signaturebytes = dsa.SignHash(hashbytes, "1.3.14.3.2.26");
            dsaparams = dsa.ExportParameters(false);
            string strDSA = BitConverter.ToString(signaturebytes).Replace('-', ' ');
            textBox3.Text = strDSA;

            string strDSAParams = BitConverter.ToString(dsaparams.P).Replace('-', ' ');
            textBox4.Text = strDSAParams;
            string strDSAParams2 = BitConverter.ToString(dsaparams.Q).Replace('-', ' ');
            textBox5.Text = strDSAParams2;
            string strDSAParams3 = BitConverter.ToString(dsaparams.Y).Replace('-', ' ');
            textBox6.Text = strDSAParams3;
            string strDSAParams4 = BitConverter.ToString(dsaparams.G).Replace('-', ' ');
            textBox7.Text = strDSAParams4;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] messagebytes = Encoding.UTF8.GetBytes(textBox1.Text);
            SHA1 shal = new SHA1CryptoServiceProvider();
            byte[] hashbytes = shal.ComputeHash(messagebytes);
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
            dsa.ImportParameters(dsaparams);
            bool match = dsa.VerifyHash(hashbytes, "1.3.14.3.2.26", signaturebytes);

            if (match == true)
            {
                MessageBox.Show("VerifySignature returned TRUE");
            }
            else
            {
                MessageBox.Show("VerifySignature returned FALSE");
            }
        }
    }
}
