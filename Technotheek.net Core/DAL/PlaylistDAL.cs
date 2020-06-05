using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TechnotheekWeb.Models;
using TechnotheekWeb.Interfaces;
using Dapper;
using Technotheek.net_Core.Interfaces;
using TechnotheekWeb;
using Technotheek.net_Core.Models;

namespace Technotheek.net_Core.DAL
{
    public class PlaylistDAL : DatabaseHandler, IPlaylistDAL
    {
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

        //Haalt de ID van de playlist op
        public Playlist GetPlaylistID(string selectedPlaylist)
        {
            Playlist playlist = new Playlist();
            SqlCommand cmd = new SqlCommand("select * from [Playlist] WHERE PlaylistName = @Name", con);
            cmd.Parameters.AddWithValue("@Name", selectedPlaylist);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                playlist.ID = Int32.Parse((reader["ID"].ToString()));
            }
            con.Close();

            return playlist;
        }

        //Haalt de ID van de nummers op die in de Playlist zitten
        public List<Song> GetSongID(string selectedPlaylist)
        {
            List<Song> Song = new List<Song>();

            SqlCommand cmd = new SqlCommand("select * from [Playlist_Songs] WHERE PlaylistID = @ID", con);
            cmd.Parameters.AddWithValue("@ID", GetPlaylistID(selectedPlaylist).ID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Song song = new Song();
                song.ID = (Int32.Parse((reader["SongID"].ToString())));
                Song.Add(song);
            }
            con.Close();

            return Song;
        }

        //Haalt de namen van de nummer op op basis van de meegegeven id van GetSongID
        public List<Song> GetPlaylistSongs(string selectedPlaylist)
        {
            List<Song> Song = new List<Song>();

            foreach (var ID in GetSongID(selectedPlaylist))
            {
                SqlCommand cmd = new SqlCommand("select * from [Song] WHERE ID =" + ID.ID, con);
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
            }

            return Song;
        }

        public void AddSongToPlaylist(int songID, int playlistID)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Playlist_Songs] (SongID, PlaylistID) values (@SongID, @PlaylistID)", con);
            cmd.Parameters.AddWithValue("@SongID", songID);
            cmd.Parameters.AddWithValue("@PlaylistID", playlistID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
