using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TechnotheekWeb.Models;
using TechnotheekWeb.Interfaces;

namespace TechnotheekWeb
{
    /// <summary>
    /// database connection class
    /// </summary>
    public class DatabaseHandler : IDatabaseHandler
    {
        /// <summary>
        /// Gets the SQL Database connection
        /// </summary>
        public SqlConnectionStringBuilder builder { get; private set; }
        public SqlDataReader dataReader { get; set; }
        public SqlConnection con { get; private set; }
        public DataTable table { get; private set; }
        public DatabaseHandler()
        {
            builder = new SqlConnectionStringBuilder();

            builder.DataSource = "mssql.fhict.local";
            builder.UserID = "dbi429901_technothee";
            builder.Password = "StarWars1";
            builder.InitialCatalog = "dbi429901_technothee";

            con = new SqlConnection(builder.ConnectionString);
            table = new DataTable();
        }

        public bool result { get; set; }

        public void CUD(string query)
        {

            result = false;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {

            }
            result = true;
        }

        public bool TestConnection()
        {
            bool open = false;

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                    open = true;
                    con.Close();
                }
            }
            catch (Exception)
            {
                open = false;
            }
            return open;
        }

    }
}

