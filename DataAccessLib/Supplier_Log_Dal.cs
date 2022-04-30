using Medicure_Entity;
using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace DataAccessLib
{
    public class Supplier_Log_Dal :Base_Dal
    {
        public void AddLog(Supplier_Log s)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"insert into Supplier_Log (Supplier_Id,Drug_id,Qty,Date) values ({s.Supplier_Id},{s.Drug_id},{s.Qty},'{s.Date}')";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();


        }
        public List<Supplier_Log> Supplier_Log_Details(int id)
        {
            List<Supplier_Log> details = new List<Supplier_Log>();
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"select Supplier_Id,Physician_ID,Drug_id,Qty,Date from Supplier_Log where Supplier_Id={id}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Supplier_Log s = new Supplier_Log();
                s.Supplier_Id = dr.GetInt32(0);
                s.Physician_ID = dr.GetInt32(1);
                s.Drug_id = dr.GetInt32(2);
                s.Qty = dr.GetInt32(3);
                details.Add(s);
            }
            cn.Close();
            cn.Dispose();
            return details;
        }
        public List<Supplier_Log> Physician_Log_Details(int id)
        {
            List<Supplier_Log> details = new List<Supplier_Log>();
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"select Supplier_Id,Physician_ID,Drug_id,Qty,Date from Supplier_Log where Physician_ID={id}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Supplier_Log s = new Supplier_Log();
                s.Supplier_Id = dr.GetInt32(0);
                s.Physician_ID = dr.GetInt32(1);
                s.Drug_id = dr.GetInt32(2);
                s.Qty = dr.GetInt32(3);
                details.Add(s);
            }
            cn.Close();
            cn.Dispose();
            return details;
        }
    }
}
