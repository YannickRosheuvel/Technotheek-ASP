using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TechnotheekWeb.Interfaces;
using TechnotheekWeb.Models;

namespace TechnotheekWeb.DAL
{
    class UserDAL : DatabaseHandler, IUserDAL
    {
        User user = new User();

        /// <summary>
        /// sees whether or not the user is in the database and gives messages based on that.
        /// </summary>
        /// <param name="bel"></param>
        /// <returns></returns>
        public bool Login(Login login, string Email, string Password, int userID)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from [User] where Gebruikersnaam= @Username and Wachtwoord= @Password", con);
            cmd.Parameters.AddWithValue("@Username", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {

                for (int i = 0; i < dt.Rows.Count;)
                {
                    login.Message = "You are logged in as " + dt.Rows[i][1].ToString();
                    if (dt.Rows[i]["FunctionType"].ToString() == "Admin")
                    {
                        user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        GetUserData(user.ID);
                        con.Close();
                        return login.AdminOrNot = true;
                    }
                    else
                    {
                        user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        GetUserData(user.ID);
                        con.Close();
                        return login.AdminOrNot = false;
                    }
                }
            }
            else
            {
                login.Message = "Username or Password incorrect.";
                con.Close();
            }
            return login.AdminOrCostumer = true;
        }

        public void GetUserData(int userID)
        {

            SqlCommand getUserData = new SqlCommand("SELECT * FROM [User] WHERE ID = @User_ID", GetCon());
            getUserData.Parameters.AddWithValue("User_ID", userID);

            dataReader = getUserData.ExecuteReader();

            if (dataReader.Read())
            {
                user.FirstName = dataReader["Gebruikersnaam"].ToString();
                user.Password = dataReader["Wachtwoord"].ToString();
                user.Contact = Convert.ToInt32(dataReader["Contact"]);
                user.FirstName = dataReader["FirstName"].ToString();
                user.LastName = dataReader["LastName"].ToString();
                user.Street = dataReader["Street"].ToString();
                user.StreetNmr = Convert.ToInt32(dataReader["StreetNmr"]);
                user.City = dataReader["City"].ToString();
            }

        }

        /// <summary>
        /// Imports the User data into the database
        /// </summary>
        /// <param name="bel"></param>
        public void Registration(User bel, string Username, string Password, int Contact, string FirstName, string LastName, string Street, int StreetNmr, string City, int userID)
        {

            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[User]
           (
           [Gebruikersnaam]
           ,[Wachtwoord]
           ,[Contact]
           ,[FirstName]
           ,[LastName]
           ,[Street]
           ,[StreetNmr]
           ,[City]
           )
              VALUES
           (@Username,@Password,@Contact,@FirstName,@LastName,@Street,@StreetNmr,@City)", con);
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Contact", Contact);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Street", Street);
            cmd.Parameters.AddWithValue("@StreetNmr", StreetNmr);
            cmd.Parameters.AddWithValue("@City", City);
            cmd.Parameters.AddWithValue("@ID", userID);

            con.Open();
            //GetUserData(userID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
