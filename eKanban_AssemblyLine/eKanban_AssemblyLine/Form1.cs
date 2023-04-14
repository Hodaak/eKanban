/*
* FILE			: Form1.cs
* PROJECT		: PROG3070 - Project
* PROGRAMMER	: Enes Demirsoz, Jessica Sim, Hoda Akrami
* FIRST VERSION : 2022-11-26
* DESCRIPTION	: This is the Assembly Line Kanban application
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace eKanban_AssemblyLine
{
    public partial class Form1 : Form
    {
        private string ConnectionString;
        public List<YieldForEachWorkstationStructure> WorkstationYields = new List<YieldForEachWorkstationStructure>();
        private bool isTimerActive = false;
        private long count = 0;
        private int interval = 0;

        public Form1()
        {
            InitializeComponent();
            ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"]?.ConnectionString;
            GetConfigurationValues();

            isTimerActive = true;
            timer1.Interval= 1000/interval;
            timer1.Start();
            RetrieveDataAndDisplay();
        }

        /*
        * FUNCTION : GetConfigurationValues
        * DESCRIPTION : This method is to get configuration values from configuration table by calling GetDefaultTimeEfficiencyForWorkstation()
        *               procedure from eKanban database
        * PARAMETERS : no parameters
        * RETURNS : void
        */
        private void GetConfigurationValues()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("GetDefaultTimeEfficiencyForWorkstation", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                sql_cmnd.Parameters.Add("@TimeScale", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@TimeScale"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@Base", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@Base"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@BaseDifference", SqlDbType.Float, 100);
                sql_cmnd.Parameters["@BaseDifference"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@NewEmployee_Efficiency", SqlDbType.NVarChar, 5);
                sql_cmnd.Parameters["@NewEmployee_Efficiency"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@VeryExperienced_Efficiency", SqlDbType.NVarChar, 5);
                sql_cmnd.Parameters["@VeryExperienced_Efficiency"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@New_Productivity", SqlDbType.NVarChar, 10);
                sql_cmnd.Parameters["@New_Productivity"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@Experienced_Productivity", SqlDbType.NVarChar, 10);
                sql_cmnd.Parameters["@Experienced_Productivity"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@VeryExperienced_Productivity", SqlDbType.NVarChar, 10);
                sql_cmnd.Parameters["@VeryExperienced_Productivity"].Direction = ParameterDirection.Output;

                sql_cmnd.ExecuteNonQuery();

                var timeScaleString = sql_cmnd.Parameters["@TimeScale"].Value.ToString();
                interval = Convert.ToInt32(timeScaleString);

                connection.Close();
            }
        }


        /*
        * FUNCTION : RetrieveDataAndDisplay
        * DESCRIPTION : This method is to retrieve data from database to show in Assembly Kanban application by calling stored procedures and functions
        *               procedure from eKanban database
        * PARAMETERS : no parameters
        * RETURNS : void
        */
        public void RetrieveDataAndDisplay()
        {
            //call GetAvailableWorkstations stored procedure and get running workstations' ids
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand("GetRunningWorkstations", connection);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "result_name");

                        DataTable dt = ds.Tables["result_name"];

                        for (int i=0; i<dt.Rows.Count; i++)
                        {
                            if(WorkstationYields != null && WorkstationYields.FirstOrDefault(x=>x.WorkstationName.Text == $"{dt.Rows[i][1]}") == null)
                            {
                                WorkstationYields.Add(new YieldForEachWorkstationStructure()
                                {
                                    ID = Int32.Parse(dt.Rows[i][0].ToString()),
                                    WorkstationName = new Label() { Text =  $"{dt.Rows[i][1]}" }
                                });

                                tableLayoutPanel1.RowStyles.Add(new RowStyle());
                                tableLayoutPanel1.Controls.Add(WorkstationYields[i].WorkstationName, 0, i);
                                tableLayoutPanel1.Controls.Add(WorkstationYields[i].Yield, 1, i);
                                WorkstationYields[i].Row = i;
                                tableLayoutPanel1.Update();
                            }
                        }

                        if (dt.Rows.Count != WorkstationYields.Count)
                        {
                            IEnumerable<DataRow> collection = dt.Rows.Cast<DataRow>();
                            for (int i = 0; i < WorkstationYields.Count; i++)
                            {
                                if (!collection.Any(x => x[1].ToString() == WorkstationYields[i].WorkstationName.Text))
                                {
                                    tableLayoutPanel1.Controls.Remove(WorkstationYields[i].WorkstationName);
                                    tableLayoutPanel1.Controls.Remove(WorkstationYields[i].Yield);
                                    tableLayoutPanel1.RowStyles.RemoveAt(WorkstationYields[i].Row);
                                    WorkstationYields.Remove(WorkstationYields[i]);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            // pass running workstations' ids to CalculateYieldForEachWorkstation function and get yield for each workstation
            foreach(var workstation in WorkstationYields)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand($"SELECT dbo.CalculateYieldForEachWorkstation({Convert.ToInt64(workstation.ID)})", conn))
                    {
                        conn.Open();
                        var yield = cmd.ExecuteScalar().ToString();

                        if (yield == "-1" && workstation.Yield.Text == "")
                        {
                            workstation.Yield.Text = "N/A";
                        }
                        else
                        {
                            if (yield != "" && yield != "-1")
                            {
                                workstation.Yield.Text = (String.Format("{0:F2} %", Convert.ToDouble(yield)));
                            }
                        }

                        
                        
                        conn.Close();
                    }
                }
            }

            // call CalculateYieldForAllRunningWorkstation function to get yield for all workstations
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.CalculateYieldForAllRunningWorkstation()", conn))
                {
                    conn.Open();

                    var yield = cmd.ExecuteScalar().ToString();
                    if (yield == "")
                    {
                        totalYieldForAllWorkstations.Text = "N/A";
                    }
                    else
                    {
                        totalYieldForAllWorkstations.Text = String.Format("{0:F2} %", Convert.ToDouble(yield));
                    }

                    conn.Close();
                }
            }

            //call RetrieveDataForAssemblyKanban stored procedure then update UI
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("RetrieveDataForAssemblyKanban", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;


                    sql_cmnd.Parameters.Add("@OrderAmount", SqlDbType.Float);
                    sql_cmnd.Parameters["@OrderAmount"].Direction = ParameterDirection.Output;
                    sql_cmnd.Parameters.Add("@TotalNumberOfProductsProduced", SqlDbType.Float);
                    sql_cmnd.Parameters["@TotalNumberOfProductsProduced"].Direction = ParameterDirection.Output;
                    sql_cmnd.Parameters.Add("@NumberOfWorkstationsRunning", SqlDbType.Int, 100);
                    sql_cmnd.Parameters["@NumberOfWorkstationsRunning"].Direction = ParameterDirection.Output;
                    sql_cmnd.Parameters.Add("@ProcessAmount", SqlDbType.Float);
                    sql_cmnd.Parameters["@ProcessAmount"].Direction = ParameterDirection.Output;

                    sql_cmnd.ExecuteNonQuery();

                    orderAmount.Text = (sql_cmnd.Parameters["@OrderAmount"].Value.ToString() == "0")? "N/A" : $"{sql_cmnd.Parameters["@OrderAmount"].Value}" ;
                    numberProduced.Text = (sql_cmnd.Parameters["@TotalNumberOfProductsProduced"].Value.ToString() == "" ||
                        sql_cmnd.Parameters["@TotalNumberOfProductsProduced"].Value.ToString() == "0") ? "0" : $"{sql_cmnd.Parameters["@TotalNumberOfProductsProduced"].Value}";
                    numberOfRunningWorkstations.Text = (sql_cmnd.Parameters["@NumberOfWorkstationsRunning"].Value.ToString() == "0") ? "N/A" : $"{sql_cmnd.Parameters["@NumberOfWorkstationsRunning"].Value}";
                    processAmount.Text = (sql_cmnd.Parameters["@ProcessAmount"].Value.ToString() == "") ?  "N/A" : String.Format("{0:F2} %", Convert.ToDouble(sql_cmnd.Parameters["@ProcessAmount"].Value));


                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            tableLayoutPanel1.RowCount = WorkstationYields.Count;
        }

        /*
        * FUNCTION : timer1_Tick
        * DESCRIPTION : This method will be called every second and update UI based on updated information from database
        * PARAMETERS : no parameters
        * RETURNS : void
        */
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isTimerActive)
            {
                count++;

                if (count >= 15)
                {
                    count = 0;
                    RetrieveDataAndDisplay();
                }
            }
        }
    }

    
}
