using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace disciplina
{
    public partial class Avtorization : Form
    {
        public Avtorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "2134") && (comboBox1.SelectedIndex == 0))
            {
                FRukovod frm = new FRukovod();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пароль неверный! Повторите попытку еще раз");
            }

            if ((textBox1.Text == "2645") && (comboBox1.SelectedIndex == 1))
            {
                FMain frm = new FMain();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пароль неверный! Повторите попытку еще раз");
            }
        }
    }
}
