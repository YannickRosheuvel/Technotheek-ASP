using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TechnotheekWeb.Models;
using TechnotheekWeb.Interfaces;
using Dapper;

namespace TechnotheekWeb
{

    public class SongDAL : DatabaseHandler, ISongDAL
    {
        private List<SongListBEL> songView = new List<SongListBEL>();
        Song song = new Song();

        /// <summary>
        /// Sees if there are any matching songs according to the given input and returns the list of songs
        /// </summary>
        /// <param name="bel"></param>
        /// <returns></returns>
        public List<SongListBEL> LookUpSong(Song bel)
        {
            SongListBEL slb = new SongListBEL();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", bel.NameSong);
            List<SongListBEL> list = con.Query<SongListBEL>("LookUpSong", param, commandType: CommandType.StoredProcedure).ToList();
            songView = list;
            return new List<SongListBEL>(songView);
        }

        /// <summary>
        /// Returns the path of the song based on the selected song in the listbox
        /// </summary>
        /// <param name="bel"></param>
        /// <returns></returns>
        public string GetPathOfSelectedSong(string selectedSong)
        {
            SqlCommand cmd = new SqlCommand("select * from [Song] where Name like @SelectedSong", con);
            cmd.Parameters.AddWithValue("@SelectedSong", selectedSong);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            string SongLocation = "";

            while (dr.Read())
            {
                SongLocation = (dr["SongLink"].ToString());
            }
            con.Close();
            return SongLocation;
        }

        /// <summary>
        /// Gets all the songs from the database
        /// </summary>
        /// <returns></returns>
        public List<SongListBEL> GetAllSongs()
        {
            List<SongListBEL> songListBel = new List<SongListBEL>();

            SqlCommand cmd = new SqlCommand("select * from [Song]", con); 
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SongListBEL song = new SongListBEL();
                song.Name = (reader["Name"].ToString());
                songListBel.Add(song);
            }
            con.Close();

            return songListBel;
        }

        public void AddNewSong(Song bel)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Song] (Name, SongLink) values (@insertNameSong , @insertSongPath)", con);
            cmd.Parameters.AddWithValue("@insertNameSong", bel.NameSong);
            cmd.Parameters.AddWithValue("@insertSongPath", bel.SongPath);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
