using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace disciplina
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString);
            
            string ServerName = csBuilder.DataSource;
            
            string DBName = csBuilder.InitialCatalog;
            ConnectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Integrated Security=True";
            conn1(ConnectionString, select_otdel, dataGridView1);
            conn1(ConnectionString, select_dol, dataGridView2);
            conn1(ConnectionString, select_w, dataGridView3);
            conn1(ConnectionString, select_w, dataGridView4);
            conn1(ConnectionString, select_rab, dataGridView5);
            conn1(ConnectionString, select_p, dataGridView6);
            conn1(ConnectionString, select_p, dataGridView7);
            conn2(ConnectionString, select_dol, comboBox1, "Наименованиедолжности", "номердолжности");
            conn2(ConnectionString,select_dol,comboBox2, "Наименованиедолжности", "номердолжности");
            conn2(ConnectionString, select_rab, comboBox7, "Наименование", "Номерработы");
            conn2(ConnectionString, select_w, comboBox5, "Фиоработника", "Номерработника");
            conn2(ConnectionString, select_w, comboBox4, "Фиоработника", "Номерработника");
        }

        public string ConnectionString;
        public string select_otdel = "Select n_o as Номеротдела, name_o as Наименованиеотдела, opiis_o as Описание FROM otdel";
        public string select_dol = "select dolgnost.n_d as номердолжности, dolgnost.name_d as Наименованиедолжности,dolgnost.opis_d as описание,dolgnost.n_o as номеротдела  from dolgnost";
        public string select_w = "SELECT rabotnik.n_r as Номерработника, rabotnik.fio_r as Фиоработника, rabotnik.spec as Специализация,rabotnik.data_p as датаприема, rabotnik.data_u as датаувольнения, dolgnost.n_d as номердолжности FROM dolgnost INNER JOIN rabotnik ON dolgnost.n_d = rabotnik.n_d";
        public string select_rab = "SELECT rabota.n_rab AS Номерработы, rabota.name_rab AS Наименование, rabota.opis_rab AS Описание FROM rabota";
        public string select_p = "select  poruchenie.id_por as id_поручения, poruchenie.name_por as название, poruchenie.opis_p as описание ,poruchenie.srok_i as срок_исполнения,poruchenie.data_n as дата_начала, poruchenie.data_o as дата_окончания, poruchenie.n_rab as номер_работы,poruchenie.n_por as номер_поручения, poruchenie.n_r as  номер_работника from poruchenie";

        public void conn1(string CS, string cmdT, DataGridView dgv)
        {
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            dgv.DataSource = ds.Tables["Table"].DefaultView;

        }

        public void conn2(string CS, string cmdT, ComboBox CB, string field1, string field2)
        {
            //создание экземпляра адаптера
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            //создание объекта DataSet (набор данных)
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            // привязка ComboBox к таблице БД
            CB.DataSource = ds.Tables["Table"];

            CB.DisplayMember = field1; //установка отображаемого в списке поля
            CB.ValueMember = field2; //установка ключевого поля
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ins_otdel]";
            cmd.Parameters.Add("@name_o",SqlDbType.NVarChar, 50);
            cmd.Parameters["@name_o"].Value = textBox2.Text;
            cmd.Parameters.Add("@opiis_o",SqlDbType.NVarChar,150);
            cmd.Parameters["@opiis_o"].Value = textBox3.Text;
            cmd.ExecuteScalar();
            MessageBox.Show("Запись успешно добавлена");
            conn1(ConnectionString, select_otdel, dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ins_dolgnost]";
            cmd.Parameters.Add("@name_d", SqlDbType.NVarChar, 50);
            cmd.Parameters["@name_d"].Value = textBox5.Text;
            cmd.Parameters.Add("@opis_d", SqlDbType.NVarChar, 150);
            cmd.Parameters["@opis_d"].Value = textBox4.Text;
            cmd.Parameters.Add("@n_o",SqlDbType.Int,4);
            cmd.Parameters["@n_o"].Value = comboBox1.SelectedValue;
            cmd.ExecuteScalar();
            MessageBox.Show("Запись успешно добавлена");
            conn1(ConnectionString, select_dol, dataGridView2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ins_rabotnik]";
            cmd.Parameters.Add("@fio_r", SqlDbType.NVarChar, 50);
            cmd.Parameters["@fio_r"].Value = textBox8.Text;
            cmd.Parameters.Add("@spec", SqlDbType.NVarChar, 50);
            cmd.Parameters["@spec"].Value = textBox7.Text;
            cmd.Parameters.Add("@n_d", SqlDbType.Int,4);
            cmd.Parameters["@n_d"].Value = comboBox2.SelectedValue;
            cmd.Parameters.Add("@data_p", SqlDbType.Date);
            cmd.Parameters["@data_p"].Value = dateTimePicker1.Value;
            cmd.ExecuteScalar();
            MessageBox.Show("Запись успешно добавлена");
            conn1(ConnectionString, select_w, dataGridView3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ins_rabota]";
            cmd.Parameters.Add("@name_rab", SqlDbType.NVarChar, 50);
            cmd.Parameters["@name_rab"].Value = textBox11.Text;
            cmd.Parameters.Add("@opis_rab", SqlDbType.NVarChar, 150);
            cmd.Parameters["@opis_rab"].Value = textBox10.Text;
            cmd.ExecuteScalar();
            MessageBox.Show("Запись успешно добавлена");
            conn1(ConnectionString, select_rab, dataGridView5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ins_poruchenie]";
            cmd.Parameters.Add("@name_por", SqlDbType.NVarChar, 50);
            cmd.Parameters["@name_por"].Value = textBox14.Text;
            cmd.Parameters.Add("@opis_p", SqlDbType.NVarChar, 150);
            cmd.Parameters["@opis_p"].Value = textBox1.Text;
            cmd.Parameters.Add("@srok_i", SqlDbType.Date,70);
            cmd.Parameters["@srok_i"].Value = dateTimePicker5.Value;
            cmd.Parameters.Add("@data_n", SqlDbType.Date,70);
            cmd.Parameters["@data_n"].Value = dateTimePicker3.Value;
            cmd.Parameters.Add("@n_rab", SqlDbType.Int, 4);
            cmd.Parameters["@n_rab"].Value = comboBox7.SelectedValue;
            cmd.Parameters.Add("@n_por", SqlDbType.Int, 4);
            cmd.Parameters["@n_por"].Value = textBox16.Text;
            cmd.Parameters.Add("@n_r", SqlDbType.Int, 4);
            cmd.Parameters["@n_r"].Value = comboBox4.SelectedValue;
            cmd.ExecuteScalar();
            MessageBox.Show("Запись успешно добавлена");
           conn1(ConnectionString, select_p, dataGridView6);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[uvol]";
            cmd.Parameters.Add("@data_u", SqlDbType.Date, 150);
            cmd.Parameters["@data_u"].Value = dateTimePicker2.Value;
            cmd.Parameters.Add("@n_r",SqlDbType.Int,4);
            cmd.Parameters["@n_r"].Value = comboBox5.SelectedValue;
            cmd.ExecuteScalar();
            MessageBox.Show("Работник уволен успешно");
            conn1(ConnectionString, select_w, dataGridView4);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[udp_poruchenie]";
            cmd.Parameters.Add("@id_por", SqlDbType.Int, 4);
            cmd.Parameters["@id_por"].Value = dataGridView7[0, dataGridView7.CurrentRow.Index].Value;
            cmd.Parameters.Add("@data_o", SqlDbType.Date);
            cmd.Parameters["@data_o"].Value = dateTimePicker4.Value;
            cmd.Parameters.Add("@opis_p", SqlDbType.NVarChar, 150);
            cmd.Parameters["@opis_p"].Value = comboBox3.Text;
            cmd.ExecuteScalar();
            MessageBox.Show("Запись успешно добавлено");
            conn1(ConnectionString, select_p, dataGridView7);

            if (comboBox3.SelectedIndex == 3)
            {
                SqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "[ins_poruchenie]";
                cmd1.Parameters.Add("@n_por", SqlDbType.Int, 4);
                cmd1.Parameters["@n_por"].Value = dataGridView7[1, dataGridView7.CurrentRow.Index].Value;
                cmd1.Parameters.Add("@name_por", SqlDbType.NVarChar, 50);
                cmd1.Parameters["@name_por"].Value = dataGridView7[2, dataGridView7.CurrentRow.Index].Value;
                cmd1.Parameters.Add("@opis_p", SqlDbType.NVarChar, 150);
                cmd1.Parameters["@opis_p"].Value = "Выполняется";
                cmd1.Parameters.Add("@data_n", SqlDbType.Date, 70);
                cmd1.Parameters["@data_n"].Value = dateTimePicker3.Value;
                /*cmd.Parameters.Add("@data_o",SqlDbType.Date,70);
                cmd.Parameters["@data_o"].Value = dateTimePicker4.Value;*/
                cmd1.Parameters.Add("@srok_i", SqlDbType.Date, 70);
                cmd1.Parameters["@srok_i"].Value = dateTimePicker5.Value;
                cmd1.Parameters.Add("@id_rab", SqlDbType.Int, 4);
                cmd1.Parameters["@id_rab"].Value = dataGridView7[9, dataGridView7.CurrentRow.Index].Value;
                cmd1.Parameters.Add("@n_r", SqlDbType.Int, 4);
                cmd1.Parameters["@n_r"].Value = dataGridView7[8, dataGridView7.CurrentRow.Index].Value;
                cmd1.ExecuteScalar();
                MessageBox.Show("Запись успешно добавлено");
                conn1(ConnectionString, select_p, dataGridView7);
            }

            if (comboBox3.SelectedIndex == 4)
            {
                SqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "[ins_poruchenie]";
                cmd1.Parameters.Add("@n_por", SqlDbType.Int, 4);
                cmd1.Parameters["@n_por"].Value = dataGridView7[1, dataGridView7.CurrentRow.Index].Value;
                cmd1.Parameters.Add("@name_por", SqlDbType.NVarChar, 200);
                cmd1.Parameters["@name_por"].Value = dataGridView7[2, dataGridView7.CurrentRow.Index].Value;
                cmd1.Parameters.Add("@opis_p", SqlDbType.NVarChar, 150);
                cmd1.Parameters["@opis_p"].Value = "Выполняется";
                cmd1.Parameters.Add("@data_n", SqlDbType.Date, 70);
                cmd1.Parameters["@data_n"].Value = dateTimePicker3.Value;
                /*cmd.Parameters.Add("@data_o",SqlDbType.Date,70);
                cmd.Parameters["@data_o"].Value = dateTimePicker4.Value;*/
                cmd1.Parameters.Add("@srok_i", SqlDbType.Date, 70);
                cmd1.Parameters["@srok_i"].Value = dataGridView7[7, dataGridView7.CurrentRow.Index].Value;
                cmd1.Parameters.Add("@n_rab", SqlDbType.Int, 4);
                cmd1.Parameters["@n_rab"].Value = dataGridView7[9, dataGridView7.CurrentRow.Index].Value;
                cmd1.Parameters.Add("@n_r", SqlDbType.Int, 4);
                cmd1.Parameters["@n_r"].Value = comboBox4.SelectedValue;
                cmd1.ExecuteScalar();
                MessageBox.Show("Запись изменена успешно");
                conn1(ConnectionString, select_p, dataGridView7);
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string seach = select_p;
            if (textBox9.Text !="")
            {
                seach = select_p + " where n_por like "+ textBox9.Text;
            }
            else
            {
                seach = select_p;
            }
            conn1(ConnectionString, select_p, dataGridView7);
        }
    }
}
