using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARMRBT
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        public Database database;

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Заполните поля!");
                return;
            }

            database = new Database("localhost", textBox1.Text, textBox2.Text);

            if (database.OpenConnect())
                (new Menu(this)).Show();
        }
    }
}
