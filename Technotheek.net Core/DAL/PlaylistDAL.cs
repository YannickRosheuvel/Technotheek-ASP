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

        public List<Playlist> RetrievePlaylists(int userID)
        {
            List<Playlist> Playlist = new List<Playlist>();

            SqlCommand cmd = new SqlCommand("select * from [dbo].[Playlist] WHERE userID = @ID", con);
            cmd.Parameters.AddWithValue("@ID", userID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Playlist playlist = new Playlist();
                playlist.Name = (reader["PlaylistName"].ToString());
                playlist.ID = Convert.ToInt32((reader["ID"]));
                Playlist.Add(playlist);
            }
            con.Close();

            return Playlist;
        }

        //Haalt de namen van de nummer op op basis van de meegegeven id van GetSongID
        public List<Song> GetPlaylistSongs(int selectedPlaylist)
        {
            List<Song> Song = new List<Song>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Song WHERE ID IN(SELECT SongID FROM Playlist_Songs WHERE PlaylistID = @playlistID)", con);
            cmd.Parameters.AddWithValue("@playlistID", selectedPlaylist);
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

        public PlaylistSongs AddSongToPlaylist(PlaylistSongs playlistSongs)
        {
            if(playlistSongs.songID != 0 & playlistSongs.playlistID != 0)
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Playlist_Songs] (SongID, PlaylistID) values (@SongID, @PlaylistID)", con);
                cmd.Parameters.AddWithValue("@SongID", playlistSongs.songID);
                cmd.Parameters.AddWithValue("@PlaylistID", playlistSongs.playlistID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return playlistSongs;
            }
            else
            {
                return playlistSongs;
            }

        }

    }
}
