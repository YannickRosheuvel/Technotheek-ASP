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
using Technotheek.net_Core.Models;

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
            List<Song> Song = new List<Song>();

            SqlCommand cmd = new SqlCommand("select * from [Song]", con); 
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Song song = new Song();
                song.Name = (reader["Name"].ToString());
                Song.Add(song);
            }
            con.Close();

            return Song;
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

        public void AddNewPlaylist(string name, int ID)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Playlist] (PlaylistName, userID) values (@inserPlaylistName, @userID)", con);
            cmd.Parameters.AddWithValue("@inserPlaylistName", name);
            cmd.Parameters.AddWithValue("@userID", ID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Playlist> RetrievePlaylists(int ID)
        {
            List<Playlist> Playlist = new List<Playlist>();

            SqlCommand cmd = new SqlCommand("select * from [dbo].[Playlist] WHERE userID = @ID", con);
            cmd.Parameters.AddWithValue("@ID", ID);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Playlist playlist = new Playlist();
                playlist.Name = (reader["PlaylistName"].ToString());
                Playlist.Add(playlist);
            }
            con.Close();

            return Playlist;
        }
    }
}
