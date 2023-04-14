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
    public partial class Form1 : Form
    {
        private readonly string ConnectionString;
        private readonly int workstationId;
        private string workstationName;
        private bool isTimerActive = false;
        private long count = 0;
        private int interval = 0;
        private int dataRetrievalTimeInterval = 15;

        private int harnessValue = 0;
        private int reflectorValue = 0;
        private int housingValue = 0;
        private int lensValue = 0;
        private int bulbValue = 0;
        private int bezelValue = 0;

        private int defaultHarness = 0;
        private int defaultReflector = 0;
        private int defaultHousing = 0;
        private int defaultLens = 0;
        private int defaultBulb = 0;
        private int defaultBezel = 0;

        public Form1(int workstationId, string workstationName)
        {
            InitializeComponent();

            this.workstationName = workstationName;
            labelTitle.Text = "\'" + this.workstationName + "\' Andon Display";
            ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"]?.ConnectionString;

            RetrieveDefaultBinCapacities();

            harnessBar.Maximum = defaultHarness;
            reflectorBar.Maximum = defaultReflector;
            housingBar.Maximum = defaultHousing;
            lensBar.Maximum = defaultLens;
            bulbBar.Maximum = defaultBulb;
            bezelBar.Maximum = defaultBezel;
            
            this.workstationId = workstationId;
            signal.Checked = false;
            yieldValue.Text = "N/A";

            RetrievePartsDataAndDisplay();
            GetConfigurationValues();

            isTimerActive = true;
            timer1.Interval = 1000 / interval;
            timer1.Start();
        }

        private void RetrieveDefaultBinCapacities()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand sql_cmnd = new SqlCommand("GetDefaultBinCapacities", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                sql_cmnd.Parameters.Add("@HarnessCapacity", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@HarnessCapacity"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@ReflectorCapacity", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@ReflectorCapacity"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@HousingCapacity", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@HousingCapacity"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@LensCapacity", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@LensCapacity"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@BulbCapacity", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@BulbCapacity"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@BezelCapacity", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@BezelCapacity"].Direction = ParameterDirection.Output;

                sql_cmnd.ExecuteNonQuery();

                defaultHarness = Int32.Parse(sql_cmnd.Parameters["@HarnessCapacity"].Value.ToString());
                defaultReflector = Int32.Parse(sql_cmnd.Parameters["@ReflectorCapacity"].Value.ToString());
                defaultHousing = Int32.Parse(sql_cmnd.Parameters["@HousingCapacity"].Value.ToString());
                defaultLens = Int32.Parse(sql_cmnd.Parameters["@LensCapacity"].Value.ToString());
                defaultBulb = Int32.Parse(sql_cmnd.Parameters["@BulbCapacity"].Value.ToString());
                defaultBezel = Int32.Parse(sql_cmnd.Parameters["@BezelCapacity"].Value.ToString());

                connection.Close();
            }
        }
        /*
        * FUNCTION      : RetrievePartsDataAndDisplay
        * DESCRIPTION   : This method is to get current number of parts left in each bin and display
        * PARAMETERS    : no parameters
        * RETURNS       : void
        */
        private void RetrievePartsDataAndDisplay()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand sql_cmnd = new SqlCommand("GetPartsCount", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                sql_cmnd.Parameters.AddWithValue("@WorkstationID", SqlDbType.Int).Value = workstationId;

                sql_cmnd.Parameters.Add("@NumberOfHarnessLeft", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@NumberOfHarnessLeft"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@NumberOfReflectorLeft", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@NumberOfReflectorLeft"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@NumberOfHousingLeft", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@NumberOfHousingLeft"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@NumberOfLensLeft", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@NumberOfLensLeft"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@NumberOfBulbLeft", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@NumberOfBulbLeft"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@NumberOfBezelLeft", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@NumberOfBezelLeft"].Direction = ParameterDirection.Output;

                sql_cmnd.Parameters.Add("@IsNeedToReplenish", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@IsNeedToReplenish"].Direction = ParameterDirection.Output;

                sql_cmnd.ExecuteNonQuery();


                var retrievedValue = Int32.Parse(sql_cmnd.Parameters["@NumberOfHarnessLeft"].Value.ToString());
                if (retrievedValue >= defaultHarness)
                {
                    harnessBar.Maximum = retrievedValue;
                }
                harnessValue = retrievedValue;
                harnessBar.Value = harnessValue;
                harnessCurrentValue.Text = harnessValue.ToString();

                retrievedValue = Int32.Parse(sql_cmnd.Parameters["@NumberOfReflectorLeft"].Value.ToString());
                if (retrievedValue >= defaultReflector)
                {
                    reflectorBar.Maximum = retrievedValue;
                }
                reflectorValue = retrievedValue;
                reflectorBar.Value = reflectorValue;
                reflectorCurrentValue.Text = reflectorValue.ToString();

                retrievedValue = Int32.Parse(sql_cmnd.Parameters["@NumberOfHousingLeft"].Value.ToString());
                if (retrievedValue >= defaultHousing)
                {
                    housingBar.Maximum = retrievedValue;
                }
                housingBar.Refresh();
                housingValue = retrievedValue;
                housingBar.Value = housingValue;
                housingCurrentValue.Text = housingValue.ToString();

                retrievedValue = Int32.Parse(sql_cmnd.Parameters["@NumberOfLensLeft"].Value.ToString());
                if (retrievedValue >= defaultLens)
                {
                    lensBar.Maximum = retrievedValue;
                }
                lensValue = retrievedValue;
                lensBar.Value = lensValue;
                lensCurrentValue.Text = lensValue.ToString();

                retrievedValue = Int32.Parse(sql_cmnd.Parameters["@NumberOfBulbLeft"].Value.ToString());
                if (retrievedValue >= defaultBulb)
                {
                   bulbBar.Maximum = retrievedValue;
                }
                bulbValue = retrievedValue;
                bulbBar.Value = bulbValue;
                bulbCurrentValue.Text = bulbValue.ToString();

                retrievedValue = Int32.Parse(sql_cmnd.Parameters["@NumberOfBezelLeft"].Value.ToString());
                if (retrievedValue >= defaultBezel)
                {
                   bezelBar.Maximum = retrievedValue;
                }
                bezelValue = retrievedValue;
                bezelBar.Value = bezelValue;
                bazelCurrentValue.Text = bezelValue.ToString();

                int isSignalNeedToBeChecked = Int32.Parse(sql_cmnd.Parameters["@IsNeedToReplenish"].Value.ToString());
                signal.Checked = isSignalNeedToBeChecked == 1;

                // get yield
                SqlCommand command = new SqlCommand($"SELECT dbo.CalculateYieldForEachWorkstation({workstationId})", connection);
                var yield = command.ExecuteScalar().ToString();
                if (yield == "") {
                    yieldValue.Text = "N/A";
                } else
                {
                    yieldValue.Text = String.Format("{0:F2} %", Convert.ToDouble(yield.ToString()));
                }

                // num products
                command.CommandText = $"SELECT dbo.GetNumberOfProductProducedForWorkstation({workstationId})";
                var numProducts = Convert.ToInt32(command.ExecuteScalar());
                labelProductsValue.Text = numProducts.ToString();
                connection.Close();
            }
        }

        /*
        * FUNCTION      : GetConfigurationValues
        * DESCRIPTION   : This method is to get configuration values from configuration table by calling GetDefaultTimeEfficiencyForWorkstation()
        *                 procedure from eKanban database
        * PARAMETERS    : no parameters
        * RETURNS       : void
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

        private void HandleAndonRelease() 
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("ReleaseWorkstationForAndon", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                sql_cmnd.Parameters.AddWithValue("@WorkstationID", SqlDbType.Int).Value = workstationId;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }
            timer1.Stop();
            isTimerActive = false;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            HandleAndonRelease();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isTimerActive)
            {
                count++;

                if (count >= dataRetrievalTimeInterval)
                {
                    count = 0;
                    RetrievePartsDataAndDisplay();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HandleAndonRelease();
            this.Owner.Dispose();
            this.Close();
        }
    }
}
