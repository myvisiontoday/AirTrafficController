using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pilot
{
    public partial class DialogForm : Form
    {
        public int fuel;
        public string status;
        public DialogForm()
        {
            InitializeComponent();
            
            radioButton1.Checked = true;
            this.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num = -1;
            if (radioButton1.Checked == true)
            {
                if (int.TryParse(textBox1.Text, out num) && num >= 0 && num <= 100) GetFuel();
                else MessageBox.Show("Please enter fuel level between 0 and 100");
            }
            if (fuel != 0)
            {

                this.Close();
            }
            

        }
        public void GetFuel()
        {
            if (Convert.ToInt32(textBox1.Text) <= 10)
            {
                MessageBox.Show("Enter value above 10");
                fuel = 0;
            }
            else if (Convert.ToInt32(textBox1.Text) > 100)
            {
                MessageBox.Show("Enter value below or equal to 100");
                fuel = 0;
            }
            else
            {

                fuel = Convert.ToInt32(textBox1.Text);

            }

        }
        //MARTA: I'm changing the names of the status for this cases to later have a clear difference between awaiting approval, landing, docked etc
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label1.Visible = true;
            status = "In the air";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            label1.Visible = false;
            status = "On the ground";
            fuel = 100;
        }

        private void DialogForm_Load(object sender, EventArgs e)
        {

        }
    }
}
