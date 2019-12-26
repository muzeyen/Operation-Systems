using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BM307_Homework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_sorting_Click(object sender, EventArgs e)
        {
                string text = textBox1.Text;
                int int_text = Convert.ToInt32(text);
                if (int_text > 0 && int_text <= 900)
                {
                    Form2 pagetwo = new Form2(int_text);
                    this.Hide();
                    pagetwo.Show();
                }
                else
                   MessageBox.Show("You can write numbers between 1 and 900", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }



    } 
}
