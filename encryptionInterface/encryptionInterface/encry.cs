using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace encryptionInterface
{
    public partial class encry : Form
    {
        public encry()
        {
            InitializeComponent();
            label1.Text = "User";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                Hide();
                txtency tc = new txtency();
                tc.ShowDialog();
                Close();
            }
            if (radioButton2.Checked == true)
            {
                Hide();
                imgency ic = new imgency();
                ic.ShowDialog();
                Close();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                Hide();
                txtdecy tc = new txtdecy();
                tc.ShowDialog();
                Close();
            }
            if (radioButton2.Checked == true)
            {
                Hide();
                imgdecy ic = new imgdecy();
                ic.ShowDialog();
                Close();
            }

        }

        
    }
}
