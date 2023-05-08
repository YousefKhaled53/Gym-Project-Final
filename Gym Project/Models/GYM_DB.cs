using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;

namespace Gym_Project.Models
{

	public class GYM_DB
	{
		public SqlConnection connection;
		public GYM_DB() {
			string connString = " Data Source=desktop-8u2ml7t;Initial Catalog=GYMDB;Integrated Security=True";
			connection = new SqlConnection(connString);
		}

		
		public string getpassword(string username) {
			string pass = ".";
			string q = "select password_ from user_gym where user_gym.user_name='" + username + "'";
			try
			{
				connection.Open();
				SqlCommand comm = new SqlCommand(q, connection);
				pass = (string)comm.ExecuteScalar();
			}
			catch (SqlException ex) { }
			finally { connection.Close(); }
			return pass;
		}
        public string getjob(string username)
        {
            string job = ".";
            string q = "select job from user_gym where user_gym.user_name='" + username + "'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                job = (string)comm.ExecuteScalar();
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
            return job;
        }
        public int getweight(string username)
        {
            int w = 0;
            string q = "select weight_ from Body_info where Body_info.user_name='" + username + "'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                object result = comm.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    w = Convert.ToInt32(result);
                }
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
            return w;
        }
        public int getheight(string username)
        {
            int h = 0;
            string q = "select height from Body_info where Body_info.user_name='" + username + "'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                object result = comm.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    h = Convert.ToInt32(result);
                }
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
            return h;
        }
        public int getmusclemass(string username)
        {
            int mm = 0;
            string q = "select Muscles_Percentage from Body_info where Body_info.user_name='" + username + "'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                object result = comm.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    mm = Convert.ToInt32(result);
                }
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
            return mm;
        }
        public int getfatpercentage(string username)
        {
            int fp = 0;
            string q = "select Fats_Percentage from Body_info where Body_info.user_name='" + username + "'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                object result = comm.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    fp = Convert.ToInt32(result);
                }
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
            return fp;
        }
        public void adduser(string fname,string lname,string bd,string g, string email,string username,string password)
        {
            string q = "insert into user_gym(first_name, last_name, birthday, gender, email, job, user_name, password_) values('" + fname + "', '" + lname + "', '" + bd + "', '" + g + "', '" + email + "', 'student', '" + username + "', '" + password + "')";
            string q2 = "insert into Body_info(user_name,height,Muscles_Percentage,Fats_Percentage,weight_) values('"+username+"',0,0,0,0)";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                SqlCommand comm2 = new SqlCommand(q2, connection);
                object result = comm.ExecuteNonQuery();
                object result2 = comm2.ExecuteNonQuery();
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
        }
        public void edituser(string fname, string lname, string bd, string email, string username, string password)
        {
            string q = "update user_gym set  first_name = '"+fname+"',last_name = '"+lname+"',email = '"+email+"',password_ = '"+password+"',birthday = '"+bd+"' where user_name = '"+username+"'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                object result = comm.ExecuteNonQuery();
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
        }
        public void edituserbodydata(int h, int w, int mm, int fp,string username)
        {
            string q = "update Body_info set  height = " + h+ ",weight_= " + w + ", fats_percentage = " + fp + ",Muscles_percentage = " + mm + " where Body_info .user_name = '"+username+"'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                object result = comm.ExecuteNonQuery();
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
        }
        public DataTable return_an_user(string username)
        {
            DataTable dt=new DataTable();
            string q =" select * from user_gym where user_gym.user_name = '"+username+"'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                dt.Load( comm.ExecuteReader());
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
            return dt;
        }
        public DataTable return_users_bodydata(string username)
        {
            DataTable dt = new DataTable();
            string q = " select * from Body_info where Body_info .user_name = '" + username + "'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                dt.Load(comm.ExecuteReader());
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
            return dt;
        }
        public void add_subscribtion_to_user(string un,int n,string price,string per, string sd)
        {
            string q = "Exec addsubscribtion @usname='"+un+"' , @num ="+n+" ,@price ='"+price+"', @period ='"+per+"' , @start ='"+sd+"'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);

                comm.ExecuteNonQuery();

            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
        }

    }
}
