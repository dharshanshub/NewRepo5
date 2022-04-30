using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medicure_Entity;

namespace DataAccessLib
{
    public class Appointment_Log_Dal : Base_Dal
    {
        public void Book_Appointment(Appointment_Log ap)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"insert into Appointment_Log(Appointment_ID,Patient_Id,Physician_Id,illness,Date_of_visit)  values('{ap.Appointment_ID}','{ap.Patient_Id}',{ap.Physician_Id},'{ap.illness}','{ap.Date_of_visit}')";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }
        public void Delete_Appointment(int id)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"DELETE FROM Appointment_Log WHERE Appointment_ID={id};";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }
    }

}
