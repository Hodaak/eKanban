/*
* FILE			: Program.cs
* PROJECT		: PROG3070 - Milestone-2
* PROGRAMMER	: Enes Demirsoz, Jessica Sim, Hoda Akrami
* FIRST VERSION : 2022-11-26
* DESCRIPTION	: This is the runner application for eKanban system.
*/
using System.Data;
using System;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;

public class Program
{
    private static System.Timers.Timer aTimer;
    private static long count = 0;
    private static long timeDuration = 0;
    private static int timeMin = 0;
    private static int timeScale = 0;
    private static string ConnectionString;

    public static void Main()
    {
        ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"]?.ConnectionString;
        SetConfigValues(); // get config values here

        if (timeMin <= 0 || timeScale <= 0)
        {
            Console.WriteLine("\nPlease provide a value larger than zero for RunnerTimeMinute and TimeScale in config table\n");
            return;
        }

        timeDuration = timeMin * 60;

        Console.WriteLine("\nPress the Enter key to start the runner application...\n");
        Console.ReadLine();

        SetTimer(timeScale);

        Console.WriteLine("\nPress the Enter key to exit the runner application...\n");
        Console.ReadLine();
        aTimer.Stop();
        aTimer.Dispose();

        Console.WriteLine("Terminating the runner application...");
    }

    /*
    * FUNCTION : SetTimer
    * DESCRIPTION : This method is to set the timer and start it
    * PARAMETERS : int timeInterval
    * RETURNS : void
    */
    private static void SetTimer(int timeInterval)
    {
        // Create a timer with a specified interval.
        aTimer = new System.Timers.Timer(1000 / timeInterval);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
        Console.WriteLine("Runner application has started to run...");
    }

    /*
    * FUNCTION : OnTimedEvent
    * DESCRIPTION : This method is being executed for every interval time
    * PARAMETERS : Object soure, ElapsedEventArgs e
    * RETURNS : void
    */
    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        count++;

        if (count >= timeDuration)
        {
            Console.WriteLine("Runner checked workstations to replenish parts ...");
            count = 0;
            ReplenishParts();
        }
    }

    /*
    * FUNCTION : SetConfigValues()
    * DESCRIPTION : This method is to retrieve config values from config table and set it in the program
    * PARAMETERS : void
    * RETURNS : void
    */
    private static void SetConfigValues()
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            SqlCommand sql_cmnd = new SqlCommand("GetConfigInfoForRunner", connection);
            sql_cmnd.CommandType = CommandType.StoredProcedure;

            sql_cmnd.Parameters.Add("@TimeScale", SqlDbType.Int, 100);
            sql_cmnd.Parameters["@TimeScale"].Direction = ParameterDirection.Output;
            sql_cmnd.Parameters.Add("@RunnerTime", SqlDbType.Int, 100);
            sql_cmnd.Parameters["@RunnerTime"].Direction = ParameterDirection.Output;

            sql_cmnd.ExecuteNonQuery();

            timeScale = Convert.ToInt32(sql_cmnd.Parameters["@TimeScale"].Value);
            timeMin = Convert.ToInt32(sql_cmnd.Parameters["@RunnerTime"].Value);
            connection.Close();
        }
    }

    /*
    * FUNCTION : ReplenishParts
    * DESCRIPTION : This method is to call the stored procudere for replenishing parts
    * PARAMETERS : void
    * RETURNS : void
    */
    private static void ReplenishParts()
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            SqlCommand sql_cmnd = new SqlCommand("ReplenishParts", connection);
            sql_cmnd.CommandType = CommandType.StoredProcedure;
            sql_cmnd.ExecuteNonQuery();
            connection.Close();
        }
    }
}