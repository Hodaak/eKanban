using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eKanban_Andon
{
    public partial class WorkstationSelectionForm : Form
    {
        private string ConnectionString;
        public WorkstationSelectionForm()
        {
            InitializeComponent();
            ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"]?.ConnectionString;
            RetrieveWorkstationData();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            // handle business logic here
            List<WorkStation> availableWorkstations = new List<WorkStation>();
            WorkStation workstation = workstationCombo.SelectedItem as WorkStation;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand("GetAvailableWorkstationsForAndon", connection);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "result_name");

                        DataTable dt = ds.Tables["result_name"];

                        foreach (DataRow row in dt.Rows)
                        {
                            //manipulate your data
                            var ws = new WorkStation() { ID = Int32.Parse(row[0].ToString()), Name = row[1].ToString() };
                            availableWorkstations.Add(ws);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            if (availableWorkstations.Find(x => x.Name == workstation.Name) == null)
            {
                MessageBox.Show("Please select different workstation from options", "Can't assign selected workstation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AssignAndonToWorkstation", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                sql_cmnd.Parameters.AddWithValue("@WorkstationID", SqlDbType.Int).Value = workstation.ID;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }

            // check availability of worksation again
            this.Hide();
            Form1 form = new Form1(workstation.ID, workstation.Name);
            form.Owner = this;
            form.ShowDialog();
        }

        /*
       * FUNCTION : RetrieveEmployeeWorkstationData
       * DESCRIPTION : This method is to retrive available employee and workstations by calling GetAvailableEmployee()
       *               procedure from eKanban database
       * PARAMETERS : void
       * RETURNS : void
       */
        private void RetrieveWorkstationData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand("GetAvailableWorkstationsForAndon", connection);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "result_name");

                        DataTable dt = ds.Tables["result_name"];

                        foreach (DataRow row in dt.Rows)
                        {
                            //manipulate your data
                            var ws = new WorkStation() { ID = Int32.Parse(row[0].ToString()), Name = row[1].ToString() };
                            workstationCombo.Items.Add(ws);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        private void workstationComboBox__MouseClick(object sender, MouseEventArgs e)
        {
            workstationCombo.Items.Clear();
            RetrieveWorkstationData();
        }
    }
}
