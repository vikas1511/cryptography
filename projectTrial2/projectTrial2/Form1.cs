using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectTrial2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public enum State
        {
            Hiding,         //  Defines what state the 
            Zero_Filling    //  application is currently in.
        };

        OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
        Bitmap bmap;
        String sText;
        
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            OpenFileDialog1.Title = "Choose Image to Decrypt";
            OpenFileDialog1.Filter = "Image Files(*.jpeg; *.bmp; *.jpg; *.png;) | *.jpeg; *.bmp; *.jpg; *.png";
            if(OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmap = new Bitmap(OpenFileDialog1.FileName);
                textBox1.Text = OpenFileDialog1.FileName;
                pictureBox1.Image = bmap;
                pictureBox1.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == String.Empty)
            {
                MessageBox.Show("Select an Image to Decrypt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sText = decrypt(bmap);
            try
            {
                sText = Crypto.DecryptStringAES(sText, textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Wrong Passowrd", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
            richTextBox1.Text = sText;
            richTextBox1.Show();
            MessageBox.Show("Decryption Completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string decrypt(Bitmap bmp)
        {
            int colorUnitIndex = 0, charValue = 0;  
            string eText = String.Empty; // to store secret ext from ing

            for(int i=0;i< bmp.Height; i++)  // iterate rows
            {
                for(int j=0;j< bmp.Width; j++)  // iterate in each row
                {
                    Color pixel = bmp.GetPixel(j, i);

                    for(int n = 0; n < 3; n++)
                    {
                        switch(colorUnitIndex % 3)
                        {
                            case 0:
                                {
                                    charValue = charValue * 2 + pixel.R % 2; // append the bit value to charValue
                                }
                                break;
                            case 1:
                                {
                                    charValue = charValue * 2 + pixel.G % 2;
                                }
                                break;
                            case 2:
                                {
                                    charValue = charValue * 2 + pixel.B % 2;
                                }
                                break;
                        }
                        colorUnitIndex++;

                        if(colorUnitIndex % 8 == 0)
                        {
                            charValue = revBits(charValue);

                            if (charValue == 0)
                            {
                                return eText;
                            }

                            char c = (char)charValue; // conversion from int to char

                            eText += c.ToString();
                        }
                    }
                }
            }
            return eText;
        }

        private int revBits(int n)
        {
            int result = 0;

            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;

                n /= 2;
            }

            return result;
        }
    }
}
