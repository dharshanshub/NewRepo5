using Medicure_Entity;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib
{
    public class Supplier_Dal :Base_Dal
    {
        public void NewSupplier(Supplier S)
        {

            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"insert into Supplier(Supplier_Id,Username,Password)  values({S.SupplierId},'{S.Username}','{S.Password}')";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }
        public Supplier SupplierLogin(string username, string password)
        {
            Supplier s = new Supplier();
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"select Supplier_Id from Supplier where Username='{username}' and Password='{password}'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            Physician p = new Physician();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                s.SupplierId = dr.GetInt32(0);
                            

            }
            dr.Close();
            cn.Close();
            cn.Dispose();
            return s;
        }
        public Supplier SupplierById(int id)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"select Supplier_Id,Username,Password from Supplier where Supplier_Id={id}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            Supplier s = new Supplier();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                s.SupplierId = dr.GetInt32(0);
                s.Username = dr.GetString(1);
                s.Password = dr.GetString(2);

                
            }
            dr.Close();
            cn.Close();
            cn.Dispose();
            return s;

        }




    }
}
