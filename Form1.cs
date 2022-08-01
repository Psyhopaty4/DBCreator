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

namespace DBCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        BindingSource bind = new BindingSource();
        DataSet dataSet = new DataSet();
        SqlDataAdapter sqlData = new SqlDataAdapter();
        string connectionString = @"Data Source=DESKTOP-G26Q8FF;Initial Catalog=testing;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows != null) {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                Form2 form2 = new Form2();
                form2.TableName = dataGridView1.Rows[selectedrowindex].Cells[0].Value.ToString();
                form2.Show();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT table_name FROM information_schema.tables";
            sqlData = new SqlDataAdapter(sqlCommand);
            dataSet.Tables.Clear();
            sqlData.Fill(dataSet);
            bind.DataSource = dataSet.Tables[0];
            dataGridView1.DataSource = bind;
            sqlConnection.Close();
        }
    }
}
