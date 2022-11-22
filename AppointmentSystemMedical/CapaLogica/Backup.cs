using AppointmentSystemMedical.Properties;
using System;
using System.Data.SqlClient;

namespace AppointmentSystemMedical.CapaLogica
{
    class Backup
    {
        public static bool RealizarBackup(string ubicacion)
        {
            string nombre = "AppointmentSystemMedical_" + DateTime.Now.Year.ToString() + "_"
                + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_"
                + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_"
                + DateTime.Now.Second.ToString() + ".bak";

            var stn = new Settings();
            var con = new SqlConnection(stn.AppointmentSystemMedicalEntities);
            var cmd = new SqlCommand("BACKUP DATABASE AppointmentSystemMedical TO DISK='" + ubicacion + "/" + nombre + "'", con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                con.Close();
                return false;
            }
        }

        public static bool RestaurarBackup(string archivo)
        {
            var stn = new Settings();
            var con = new SqlConnection(stn.AppointmentSystemMedicalEntities);
            var cmd = new SqlCommand("RESTORE DATABASE AppointmentSystemMedical FROM DISK='" + archivo + "'", con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                con.Close();
                return false;
            }
        }
    }
}
