using Medicure_Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;


namespace DataAccessLib
{
    public class Paitent_Dal : Base_Dal
    {
        public List<Patient> GetAllPatient()
        {
            List<Patient> plist = new List<Patient>();
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"select * from Patient";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Patient p = new Patient();
                
                p.Id = Convert.ToInt32( dr[0]);
                p.Name = dr[1].ToString();
                p.MobileNo = dr[2].ToString();
                p.DateOfReg = dr[3].ToString();
                p.Username = dr[4].ToString();
                p.Password = dr[5].ToString();
                plist.Add(p);
            }
            cn.Close();
            cn.Dispose();
            return plist;
        }


        public void NewPaitents(Patient p)
        {

            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"insert into Patient(Name,MobileNo,DateOfReg,Username,Password)  values('{p.Name}','{p.MobileNo}','{p.DateOfReg}','{p.Username}','{p.Password}')";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i =cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }

        public Patient GetPaitentById(int id)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"  Select Name,MobileNo,DateOfReg,Username,Password From Patient where id={id}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            Patient p = new Patient();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                p.Name= dr[0].ToString();
                p.MobileNo= dr[1].ToString();
                p.DateOfReg= dr[2].ToString();
                p.Username= dr[3].ToString();
                p.Password = dr[4].ToString();
            }
             
            
            cn.Close();
            cn.Dispose();
            return p;
        }
        public void EditPatient(Patient p)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"UPDATE Patient  SET Name ='{p.Name}' ,MobileNo ='{p.MobileNo}',DateOfReg = '{p.DateOfReg}',Username ='{p.Username}' ,Password ='{p.Password}'  WHERE  Id={p.Id}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }
        public Patient PatientLogin(string username,string password)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"Select Id,Name,MobileNo,DateOfReg From Patient where Username='{username}' and Password='{password}'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            Patient p = new Patient();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                p.Id = Convert.ToInt32(dr[0]);
                p.Name = dr[1].ToString();
                p.MobileNo = dr[2].ToString();
                p.DateOfReg = dr[3].ToString();

            }


            cn.Close();
            cn.Dispose();
            return p;
        }
        public List<Appointment_Log> View_Appointment_History(int id)
        {
            SqlConnection cn = new SqlConnection(CnString);
            string sql = $"  select Appointment_ID,Patient_Id,Physician_Id,illness,Date_of_visit from Appointment_Log where Patient_Id ={id}";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            List<Appointment_Log> Appointments = new List<Appointment_Log>();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Appointment_Log a = new Appointment_Log();
                a.Appointment_ID = dr.GetInt32(0);
                a.Patient_Id = dr.GetInt32(1);
                a.Physician_Id = dr.GetInt32(2);
                a.illness = dr.GetString(3);
                a.Date_of_visit = dr.GetString(4);
                Appointments.Add(a);
            }
            dr.Close();
            cn.Close();
            cn.Dispose();
            return Appointments;


        }


    }
}
