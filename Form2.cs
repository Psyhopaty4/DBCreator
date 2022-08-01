using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBCreator
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        BindingSource bind = new BindingSource();
        DataSet dataSet = new DataSet();
        SqlDataAdapter sqlData = new SqlDataAdapter();
        SqlConnection sqlConnection = new SqlConnection();
        string connectionString = @"Data Source=DESKTOP-G26Q8FF;Initial Catalog=testing;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string queryAdd = ""; 
        string tableName;
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        } 

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = TableName;
            try
            {
                //connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                executeQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void generateQuery()
        {
            int i = 1;
            queryAdd = $"INSERT INTO {tableName} (";

            while (i < dataGridView1.Columns.Count)
            {
                queryAdd += $"[{dataGridView1.Columns[i].HeaderText}], ";
                button2.Text += dataGridView1.Columns[i].HeaderText.ToString();
                i++;
            }

            queryAdd = queryAdd.Remove(queryAdd.Length - 2, 2);
            queryAdd += ") VALUES (";
        }

        private void executeQuery()
        {
            sqlConnection = new SqlConnection(@connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                generateQuery();
                for (int j = 1; j < dataGridView1.Columns.Count; j++)
                { 
                    queryAdd += $"@Param{i}" + $"{j}" + ", ";
                }
                queryAdd = queryAdd.Remove(queryAdd.Length - 2, 2);
                queryAdd += ")";
                label1.Text = queryAdd;
                sqlCommand = new SqlCommand(queryAdd, sqlConnection);
                for (int j = 1; j < dataGridView1.Columns.Count; j++)
                {
                    sqlCommand.Parameters.AddWithValue($"Param{i}" + $"{j}", dataGridView1.Rows[i].Cells[j].Value);
                }
                sqlCommand.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }

        private void connect()
        {
            sqlConnection = new SqlConnection(@connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"SELECT * FROM {TableName}";
            sqlData = new SqlDataAdapter(sqlCommand);
            dataSet.Tables.Clear();
            sqlData.Fill(dataSet);
            bind.DataSource = dataSet.Tables[0];
            dataGridView1.DataSource = bind;
            sqlConnection.Close();
        }
    }
}
