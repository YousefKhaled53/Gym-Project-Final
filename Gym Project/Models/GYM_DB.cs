using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
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

		public void add_user()
		{

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


    }
}
