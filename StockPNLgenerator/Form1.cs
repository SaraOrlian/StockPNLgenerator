using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockPNLgenerator
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

        //Dr Kat'z method to view the table
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlCon = null; 
            try
                {   //get database parameters from App.config file
                    String strServer = ConfigurationManager.AppSettings["server"];
                    String strDatabase = ConfigurationManager.AppSettings["database"];
                    //open connection to database
                    String strConnect = $"Server={strServer};Database={strDatabase};Trusted_Connection=True;";
                    sqlCon = new SqlConnection(strConnect);
                    sqlCon.Open();
                    //prepare parameters for stored procedure called below
                    double minPrc = Convert.ToDouble(nudWprice.Value);
                    String symbol = tbSymbol.Text;
                    //set up call to spGetPrcForSymbol stored procedure
                    SqlCommand sqlCmd = new SqlCommand("spGetPrcForSymbol", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@Symbol", System.Data.SqlDbType.VarChar).Value = symbol;
                    sqlCmd.Parameters.Add("@MinPrc", System.Data.SqlDbType.Float).Value = minPrc;
                    //execute spGetPrcForSymbol
                    sqlCmd.ExecuteNonQuery();
                    //get data returned by spGetPrcForSymbol and display it
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    DataSet dataset = new DataSet();
                    da.Fill(dataset, "table1");
                    dgvData.AutoGenerateColumns = true;
                    dgvData.DataSource = dataset.Tables["table1"];
                    //This message box below, shows how to grab a specific day's data:
                    MessageBox.Show($"BTW on {dataset.Tables[0].Rows[1].ItemArray[0]} the price of {symbol} was {dataset.Tables[0].Rows[1].ItemArray[1]}");
                //dgvData.Refresh();            
            }            
                catch (Exception ex)
                {
                    MessageBox.Show(" " + DateTime.Now.ToLongTimeString() + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { if (sqlCon != null && sqlCon.State == System.Data.ConnectionState.Open) sqlCon.Close(); }
            }

        private void tbSymbol_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nudWprice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nudWprice_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void Sell_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}


     