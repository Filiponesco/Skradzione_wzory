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

        public string fil
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

        public event Action Add_Fil;
        public event Action Add_Direc;
        public event Action Start_Prog;

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Fil.Invoke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_Direc.Invoke();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Start_Prog.Invoke();
        }
    }
}
