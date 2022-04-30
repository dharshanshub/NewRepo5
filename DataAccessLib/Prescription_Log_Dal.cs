using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medicure_Entity;

namespace DataAccessLib
{
    public class Prescription_Log_Dal :Base_Dal
    {
        
        public void newPrescription(Prescription_Log pl)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"insert into Prescription_Log values({pl.Appointment_ID},{pl.Drug_Id},{pl.Dosage});";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
            Drug_Dal d = new Drug_Dal();
            d.UpdateStock(pl.Drug_Id, pl.Dosage);

            //Set addeed Prescription_Status to true 
            sql = $" UPDATE  Appointment_Log SET Prescription_Status = 1 where Appointment_ID={pl.Appointment_ID};";
            cn = new SqlConnection(CnString);
            cmd = new SqlCommand(sql, cn);
            cn.Open();
            i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }

    }
}
