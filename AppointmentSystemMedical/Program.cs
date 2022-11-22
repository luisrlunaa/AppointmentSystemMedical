using AppointmentSystemMedical.CapaPresentacion;
using AppointmentSystemMedical.Infrastucture.Contexts;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AppointmentSystemMedical
{
    static class Program
    {
        public static string connectionString;

        [STAThread]
        static void Main()
        {
            connectionString = ConfigurationManager.ConnectionStrings["AppointmentSystemMedicalEntities"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            DataManager.connectionStr = connection;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPrincipal());
        }
    }
}
