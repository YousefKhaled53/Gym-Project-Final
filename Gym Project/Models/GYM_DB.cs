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
            string q = "SELECT TOP 1 weight_ FROM Body_info\r\nWHERE user_name = '"+username+"'\r\nORDER BY date_added_in DESC;";
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
            string q = "SELECT TOP 1 height FROM Body_info\r\nWHERE user_name = '" + username + "'\r\nORDER BY date_added_in DESC;";
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

            string q = "SELECT TOP 1 Muscles_Percentage FROM Body_info\r\nWHERE user_name = '" + username + "'\r\nORDER BY date_added_in DESC;";
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


            string q = "SELECT TOP 1 Fats_Percentage FROM Body_info\r\nWHERE user_name = '" + username + "'\r\nORDER BY date_added_in DESC;";

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
        public void edituser(string fname, string lname, string bd, string email, string username, string password, string pic)
        {
            string q = "update user_gym set  first_name = '"+fname+"',last_name = '"+lname+"',email = '"+email+"',password_ = '"+password+"',birthday = '"+bd+ "',profil_pic='"+pic+"' where user_name = '" + username+"'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                object result = comm.ExecuteNonQuery();
            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
        }
        public void edituserbodydata(int h, int w, int mm, int fp,string username,string date)
        {
            string q = "insert into Body_info(user_name,height,Muscles_Percentage,Fats_Percentage,weight_,date_added_in) values('"+username+"',"+h+","+mm+","+fp+","+w+",'"+date+"')";
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
            string q = " SELECT TOP 1 * FROM Body_info\r\nWHERE user_name = '"+username+"'\r\nORDER BY date_added_in DESC;";
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
        public void add_feedback_from_user(string un, int Fr, int cr, string c)
        {
            string q = "Exec add_Feedback_from_user @usname='"+un+"',@Fac_rate="+Fr+",@c_rate="+cr+",@comment=' "+c+"'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);

                comm.ExecuteNonQuery();

            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
        }
        public DataTable return_users_Feedback()
        {
            DataTable dt = new DataTable();
            string q = "select * from Feedback_from_user";
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
        public string getemail(string username)
        {
            string job = ".";
            string q = "select email from user_gym where user_gym.user_name='" + username + "'";
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
        public int get_specif_atribute_from_bodyinfo(string attribute,string month,string year,string username)
        {
            int fp = 0;

            string q = "select "+attribute+" from Body_info where date_added_in like '%"+year+"%-"+month+"-%' and user_name='"+username+"'";

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
        public string getprofilepiclink(string username)
        {
            string job = ".";
            string q = "select profil_pic from user_gym where user_gym.user_name='" + username + "'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                object result = comm.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    job = (string)comm.ExecuteScalar();
                }

            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
            return job;
        }
        public DateTime getvalid_until_date(string username)
        {
            DateTime dt= DateTime.Now;
            string q = "select valid_until from Subscription  where user_name='" + username + "'";
            try
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(q, connection);
                object result = comm.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    dt = (DateTime)comm.ExecuteScalar();
                }

            }
            catch (SqlException ex) { }
            finally { connection.Close(); }
            return dt;
        }


    }
}
