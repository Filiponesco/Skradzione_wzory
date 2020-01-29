using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Plagiator3000
{
    public partial class Form1 : Form, IView
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string file
        {
            set
            {
                label5.Text = value;
            }
        }
        public string direct
        {
            set
            {
                label7.Text = value;
            }
        }

        public string alg
        {
            get
            {
                return comboBox1.Text;
            }
        }
        public string err
        {
            get
            {
                return comboBox2.Text;
            }
        }

        public event Action Add_File;
        public event Action Add_Direc;
        public event Action Start_Prog;

        public void Message(string s)
        {
            MessageBox.Show(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            Add_File.Invoke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            Add_Direc.Invoke();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label5.Text == "" && label7.Text == "")
            {
                MessageBox.Show("Musisz uzupełnić wszystkie pola!");
            }
            else if (label5.Text == "")
            {
                MessageBox.Show("Musisz jeszcze wybrac oryginalny plik!");
            }
            else if (label7.Text == "")
            {
                MessageBox.Show("Musisz jeszcze wybrac folder z plikami!");
            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("Musisz jeszcze wybrać algorytm!");
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Musisz jeszcze wybrać od ilu prcent testowny plik jest plagiatem!");
            }
            else
            {
                Start_Prog.Invoke();
            }
        }
    }
}
