
using Medicure_Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib
{
    public class Drug_Dal : Base_Dal
    {
        public void CreateDrug(Drug d)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"INSERT INTO Drug(Id,Name,Price,Supplier_Id)VALUES({d.Id},'{d.Name}',{d.Price},{d.Supplier_Id})";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }
        public void DeleteDrug(int id)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"DELETE FROM Drug WHERE Id={id};";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }
        public Drug DrugById(int id)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"Select Id,Name,Required_Qty,Instock_Qty,Price,Supplier_Id From Drug WHERE Id={id};";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            Drug d = new Drug();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();
                d.Id = Convert.ToInt32(dr[0]);
                d.Name = dr[1].ToString();
                d.Required_Qty = Convert.ToInt32(dr[2]);
                d.Instock_Qty = Convert.ToInt32(dr[3]);
                d.Price = Convert.ToInt64(dr[4]);
                d.Supplier_Id = Convert.ToInt32(dr[5]);

            }
            dr.Close();
            cn.Close();
            cn.Dispose();
            return d;
        }

        public List<Drug> GetAllDrug()
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"Select Id,Name,Required_Qty,Instock_Qty,Price,Supplier_Id,Expiry_Date From Drug ";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            List<Drug> DrugList = new List<Drug>();
            SqlDataReader dr = cmd.ExecuteReader();
            
            while(dr.Read())
            {
                Drug d = new Drug();
                
                d.Id = Convert.ToInt32(dr[0]);
                d.Name = dr[1].ToString();
                d.Required_Qty = Convert.ToInt32(dr[2]);
                d.Instock_Qty = Convert.ToInt32(dr[3]);
                d.Price = Convert.ToInt64(dr[4]);
                d.Supplier_Id = Convert.ToInt32(dr[5]);
                d.Expiry_Date = dr[6].ToString();
                if (d.Expiry_Date == DateTime.Now.ToString("MM/yyyy"))
                {
                    DeleteDrug(d.Id);
                }
                else
                {
                    DrugList.Add(d);
                }
            }
            dr.Close();
            cn.Close();
            cn.Dispose();
            return DrugList;
        }
        public void UpdateStock(int Did, int Dreq)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"Select Instock_Qty From Drug where Id={Did}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            int instock=0;
            int min = 100;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            
            if (dr.HasRows)
            {
                dr.Read();
                instock = dr.GetInt32(0);
            }
            dr.Close();
            cn.Close();
            cn.Dispose();
            
            if(instock>Dreq)
            {

                sql = $"UPDATE Drug SET Instock_Qty=Instock_Qty-{Dreq} WHERE Id={Did};";
                cn = new SqlConnection(CnString);
                cmd = new SqlCommand(sql, cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                cn.Close();
                cn.Dispose();

                if (instock - Dreq <= min)
                {
                    sql = $"UPDATE Drug SET Required_Qty=Required_Qty+100 WHERE Id={Did};";
                    cn = new SqlConnection(CnString);
                    cmd = new SqlCommand(sql, cn);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    cn.Dispose();

                }
            }

        }
        public List<Drug> DrugSupplier(int id)
        {
            List<Drug> drugs = new List<Drug>();
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"select Id,Name,Required_Qty,Price from Drug where Supplier_Id ={id} and Required_Qty != 0";
            cn.Open();
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Drug d = new Drug();
                dr.Read();
                d.Id = Convert.ToInt32(dr[0]);
                d.Name = dr[1].ToString();
                d.Required_Qty = Convert.ToInt32(dr[2]);
                d.Price = Convert.ToInt64(dr[3]);
                
                drugs.Add(d);
            }
            cn.Close();
            cn.Dispose();
            return drugs;

        }
        public void SupplierDrugsbtn(Supplier_Log s)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"UPDATE Drug SET Required_Qty=Required_Qty-{s.Qty} ,Instock_Qty=Instock_Qty+{s.Qty} WHERE Id={s.Drug_id};";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
            Supplier_Log_Dal d = new Supplier_Log_Dal();
            d.AddLog(s);
        }

    }
}
