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

        //Logs in the user
        public User Login(Login login, string Email, string Password, int userID)
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
                        login.IsAdmin = true;
                        int ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                        con.Close();
                        return GetUserData(ID);
                    }
                    else
                    {
                        user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        login.IsAdmin = false;
                        int ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                        con.Close();
                        return GetUserData(ID);
                    }
                }
            }
            else
            {
                login.Message = "Username or Password incorrect.";
                con.Close();
            }
            return GetUserData(userID);
        }

        // Sets the data for the user in the current session
        public User GetUserData(int userID)
        {
            User user = new User();

            SqlCommand getUserData = new SqlCommand("SELECT * FROM [User] WHERE ID = @User_ID", con);
            getUserData.Parameters.AddWithValue("User_ID", userID);

            con.Open();
            dataReader = getUserData.ExecuteReader();

            if (dataReader.Read())
            {
                user.Username = dataReader["Gebruikersnaam"].ToString();
                user.Password = dataReader["Wachtwoord"].ToString();
                user.Contact = Convert.ToInt32(dataReader["Contact"]);
                user.FirstName = dataReader["FirstName"].ToString();
                user.LastName = dataReader["LastName"].ToString();
                user.Street = dataReader["Street"].ToString();
                user.StreetNmr = Convert.ToInt32(dataReader["StreetNmr"]);
                user.City = dataReader["City"].ToString();
                user.ID = Int32.Parse(dataReader["ID"].ToString());
                user.FunctionType = (dataReader["FunctionType"].ToString());
            }
            con.Close();
            return user;
        }



        // Registration for the user
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
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void InsertImage(User user, int userID)
        { /*INSERT INTO[dbo].[User] (PictureLocation) values(@insertSongPath) WHERE ID values @userID*/
            SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[User] SET PictureLocation = @insertSongPath WHERE ID = @userID", con);
            cmd.Parameters.AddWithValue("@insertSongPath", user.PictureLocation);
            cmd.Parameters.AddWithValue("@userID", userID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public string GetUserPicture(User user, int userID)
        {
            SqlCommand cmd = new SqlCommand("SELECT PictureLocation FROM [User] WHERE ID = @User_ID", con);
            cmd.Parameters.AddWithValue("@User_ID", userID);
            con.Open();

            dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                var PicturePath = dataReader["PictureLocation"].ToString();
                con.Close();
                return PicturePath;
            }
            con.Close();
            return "";
        }
    }

}

