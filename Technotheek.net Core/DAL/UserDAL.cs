using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TechnotheekWeb.Interfaces;
using TechnotheekWeb.Models;
using Technotheek.net_Core.ViewModels;

namespace TechnotheekWeb.DAL
{
    class UserDAL : DatabaseHandler, IUserDAL
    {
        User user = new User();

        //Logs in the user
        public User Login(LoginViewModel model)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from [User] where Gebruikersnaam= @Username and Wachtwoord= @Password", con);
            cmd.Parameters.AddWithValue("@Username", model.Email);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {

                for (int i = 0; i < dt.Rows.Count;)
                {
                    //login.Message = "You are logged in as " + dt.Rows[i][1].ToString();
                    if (dt.Rows[i]["FunctionType"].ToString() == "Admin")
                    {
                        user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        model.IsAdmin = true;
                        int ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                        con.Close();
                        return GetUserData(ID);
                    }
                    else
                    {
                        user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        model.IsAdmin = false;
                        int ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                        con.Close();
                        return GetUserData(ID);
                    }
                }
            }
            else
            {
                //login.Message = "Username or Password incorrect.";
                con.Close();
            }
            return GetUserData(0);
        }

        // Deze methode haalt de user data op en geeft die mee aan de login methode
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

        public void Registration(RegisterViewModel user)
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
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Contact", user.Contact);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Street", user.Street);
            cmd.Parameters.AddWithValue("@StreetNmr", user.StreetNmr);
            cmd.Parameters.AddWithValue("@City", user.City);
            cmd.Parameters.AddWithValue("@ID", user.ID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void InsertImage(User user, int userID)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[User] SET PictureLocation = @insertSongPath WHERE ID = @userID", con);
            cmd.Parameters.AddWithValue("@insertSongPath", user.PictureLocation);
            cmd.Parameters.AddWithValue("@userID", userID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public string GetUserPicture(int userID)
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

