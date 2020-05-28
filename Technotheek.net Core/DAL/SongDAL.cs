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
        private List<Song> songView = new List<Song>();

        // Searches for a song according to the users input
        public List<Song> LookUpSong(Song bel)
        {
            Song slb = new Song();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", bel.Name);
            List<Song> list = con.Query<Song>("LookUpSong", param, commandType: CommandType.StoredProcedure).ToList();
            songView = list;
            return new List<Song>(songView);
        }

        //Gets the path of the song so it can be played
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

        //Gets all of the songs stored in the database
        public List<Song> GetAllSongs()
        {
            List<Song> songListBel = new List<Song>();

            SqlCommand cmd = new SqlCommand("select * from [Song]", con); 
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Song song = new Song();
                song.Name = (reader["Name"].ToString());
                songListBel.Add(song);
            }
            con.Close();

            return songListBel;
        }

        //Gives the admin the control to add new songs and name them
        public void AddNewSong(Song bel)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Song] (Name, SongLink) values (@insertNameSong , @insertSongPath)", con);
            cmd.Parameters.AddWithValue("@insertNameSong", bel.Name);
            cmd.Parameters.AddWithValue("@insertSongPath", bel.SongLink);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddNewPlaylist(string name)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Playlist] (PlaylistName) values (@inserPlaylistName)", con);
            cmd.Parameters.AddWithValue("@inserPlaylistName", name);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
