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
    public partial class FRukovod : Form
    {
        public FRukovod()
        {
            InitializeComponent();
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString);
            string ServerName = csBuilder.DataSource;
            string DBName = csBuilder.InitialCatalog;
            ConnectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Integrated Security=True";
            conn2(ConnectionString, select_otdel, comboBox1, "Название_отдела", "ID_Отдела");
            /*conn2(ConnectionString, select_w, comboBox4, "ФИО_Работника", "Номер_работника");
            conn2(ConnectionString, select_dol, comboBox3, "Наименование_должности", "Номер_должности");
            conn2(ConnectionString, select_otdel, comboBox2, "Название_отдела", "Номер_отдела");*/
        }

        public void conn2(string CS, string cmdT, ComboBox CB, string field1, string field2)
        {
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            CB.DataSource = ds.Tables["Table"];
            CB.DisplayMember = field1;
            CB.ValueMember = field2;

        }

        public string ConnectionString;
        public string select_otdel = "SELECT n_o AS ID_Отдела, name_o AS Название_отдела, opiis_o AS Описание_отдела FROM otdel";
        public string select_w = "SELECT poruchenie.n_por as №Задания, rabota.name_rab as Наименование_работы, poruchenie.srok_i as Срок_исполнения, otdel.n_o, dolgnost.n_d, rabotnik.n_r FROM rabota INNER JOIN poruchenie ON rabota.n_rab = poruchenie.n_rab INNER JOIN otdel INNER JOIN dolgnost ON otdel.n_o = dolgnost.n_o INNER JOIN rabotnik ON dolgnost.n_d = rabotnik.n_d 0where rabotnik.n_r= @IdI and otdel.n_o= @id_otd and dolgnost.n_d= @iddol and poruchenie.srok_i between @date1 and @date2";
        public string select_dol = "SELECT dolgnost.n_d as Номер_должности, dolgnost.name_d as Наименование_должности, dolgnost.opis_d as Описание_должности FROM dolgnost";

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[sel_OS]";
            cmd.Parameters.Add("@data1", SqlDbType.Date, 70);
            cmd.Parameters["@data1"].Value = dateTimePicker1.Value;
            cmd.Parameters.Add("@data2", SqlDbType.Date, 70);
            cmd.Parameters["@data2"].Value = dateTimePicker2.Value;
            cmd.Parameters.Add("@n_o", SqlDbType.Int, 4);
            cmd.Parameters["@n_o"].Value = comboBox1.SelectedValue;
            SqlDataAdapter ReportAdapter = new SqlDataAdapter();
            ReportAdapter.SelectCommand = cmd;
            DataSet dsReport = new DataSet();
            ReportAdapter.Fill(dsReport, "Report");
            dataGridView1.DataSource = dsReport.Tables["Report"].DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[sel_KI]";
            cmd.Parameters.Add("@IdI", SqlDbType.Int, 4);
            cmd.Parameters["@IdI"].Value = comboBox4.SelectedValue;
            cmd.Parameters.Add("@data1", SqlDbType.Date, 70);
            cmd.Parameters["@data1"].Value = dateTimePicker4.Value;
            cmd.Parameters.Add("@date2", SqlDbType.Date, 70);
            cmd.Parameters["@data2"].Value = dateTimePicker3.Value;
            cmd.Parameters.Add("@iddol", SqlDbType.Int, 4);
            cmd.Parameters["@iddol"].Value = comboBox3.SelectedValue;
            cmd.Parameters.Add("@id_otd", SqlDbType.Int, 4);
            cmd.Parameters["@id_otd"].Value = comboBox2.SelectedValue;
            SqlDataAdapter ReportAdapter = new SqlDataAdapter();
            ReportAdapter.SelectCommand = cmd;
            DataSet dsReport = new DataSet();
            ReportAdapter.Fill(dsReport, "Report");
            dataGridView2.DataSource = dsReport.Tables["Report"].DefaultView;*/
        }
    }
}
