﻿using BillingSystem.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystem.DAL
{
    public class userDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;


        #region Select Data from Database
        public DataTable Select()
        {
            //Static Method to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //To hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //SQL Query to Get Data From Database
                String sql = "SELECT * FROM tbl_users";
                //For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Database Connection Open
                conn.Open();
                //Fill Data in our DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
            return dt;
        }
        #endregion
        #region Insert Data in Database
        public bool Insert(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                String sql = "INSERT INTO tbl_users(first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.last_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed Successfully then the value to rows will be greater than 0 else it will be less than 0
                if (rows > 0)
                {
                    //Query Successfull
                    isSuccess = true;
                }
                else
                {
                    //Query Failed
                    isSuccess = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }
        #endregion
        #region Update Data in Database
        public bool Update(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, address=@address, gender=@gender, user_type=@user_type, added_date=@added_date, added_by=@added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.last_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    //Query Successfull
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Delete Data from database
        public bool Delete(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("id", u.id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    //Query Successfull
                    isSuccess = true;
                }
                else
                {
                    //Query Failed
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion
        #region Search User on Database usingKeywords
        public DataTable Search(string keywords)
        {
            //Static Method to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //To hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //SQL Query to Get Data From Database
                String sql = "SELECT * FROM tbl_users  WHERE id like '%" + keywords + "%' OR first_name LIKE '%" + keywords + "%' OR last_name LIKE '%" + keywords + "%' OR username LIKE '" + keywords + "' ";
                //For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Database Connection Open
                conn.Open();
                //Fill Data in our DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
            return dt;
        }

        #endregion
        #region Getting User ID From Username
        public userBLL GetIDFromUsername(string username)
        {
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT id FROM tbl_users WHERE username = '" + username + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();

                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    u.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return u;
        }
        #endregion
    }
}
