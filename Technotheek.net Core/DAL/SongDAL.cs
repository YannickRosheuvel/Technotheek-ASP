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
using Technotheek.net_Core.ViewModels;

namespace TechnotheekWeb
{

    public class SongDAL : DatabaseHandler, ISongDAL
    {
        private List<Song> songView = new List<Song>();

        //Zoekt naar nummer op basis van de input van de gebruiker
        public List<Song> LookUpSong(Song bel)
        {
            Song slb = new Song();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", bel.Name);
            List<Song> list = con.Query<Song>("LookUpSong", param, commandType: CommandType.StoredProcedure).ToList();
            songView = list;
            return new List<Song>(songView);
        }

        public string GetPathOfSelectedSong(string selectedSong)
        {
            SqlCommand cmd = new SqlCommand("select * from [Song] where Name like @SelectedSong", con);
            cmd.Parameters.AddWithValue("@SelectedSong", selectedSong);
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

        public List<Song> GetAllSongs()
        {
            List<Song> Song = new List<Song>();

            SqlCommand cmd = new SqlCommand("select * from [Song]", con); 
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Song song = new Song();
                song.Name = (reader["Name"].ToString());
                song.ID = Convert.ToInt32((reader["ID"]));
                Song.Add(song);
            }
            con.Close();

            return Song;
        }

        //Zet het pad van het nieuw toegevoegde nummer in de database
        public void AddNewSong(SongCreateViewModel songCreateViewModel)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Song] (Name, SongLink, GenreID, ArtistID, AlbumID) values (@NameSong , @SongPath, @Genre, @Artist, @Album)", con);
            cmd.Parameters.AddWithValue("@NameSong", songCreateViewModel.Name);
            cmd.Parameters.AddWithValue("@SongPath", songCreateViewModel.SongLink);
            cmd.Parameters.AddWithValue("@Genre", AddGenre(songCreateViewModel));
            cmd.Parameters.AddWithValue("@Artist", AddArtist(songCreateViewModel));
            cmd.Parameters.AddWithValue("@Album", AddAlbum(songCreateViewModel));
            //cmd.Parameters.AddWithValue("@Album", songCreateViewModel.Album);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public int AddAlbum(SongCreateViewModel songCreateViewModel)
        {
            int AlbumID = 0;

            SqlCommand cmd = new SqlCommand(@"insert into Album (Name)
                                            Select @Name Where not exists(select * from Album where Name = @Name)", con);
            SqlCommand read = new SqlCommand(@"SELECT * FROM [Album] WHERE Name = @Name", con);
            cmd.Parameters.AddWithValue("@Name", songCreateViewModel.Album);
            read.Parameters.AddWithValue("@Name", songCreateViewModel.Album);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader reader = read.ExecuteReader();
            while (reader.Read())
            {
                AlbumID = Convert.ToInt32((reader["ID"].ToString()));
            }
            con.Close();
            return AlbumID;
        }

        public int AddGenre(SongCreateViewModel songCreateViewModel)
        {
            int GenreID = 0;

            SqlCommand read = new SqlCommand(@"SELECT * FROM [Genre] WHERE Name = @Name", con);
            read.Parameters.AddWithValue("@Name", songCreateViewModel.Genre);
            con.Open();
            SqlDataReader reader = read.ExecuteReader();
            while (reader.Read())
            {
                GenreID = Convert.ToInt32((reader["ID"].ToString()));
            }
            con.Close();
            return GenreID;
        }

        public int AddArtist(SongCreateViewModel songCreateViewModel)
        {
            int ArtistID = 0;

            SqlCommand cmd = new SqlCommand(@"insert into Artist (Name)
                                            Select @Name Where not exists(select * from Artist where Name = @Name)", con);
            SqlCommand read = new SqlCommand(@"SELECT * FROM [Artist] WHERE Name = @Name", con);
            cmd.Parameters.AddWithValue("@Name", songCreateViewModel.Artist);
            read.Parameters.AddWithValue("@Name", songCreateViewModel.Artist);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader reader = read.ExecuteReader();
            while (reader.Read())
            {
                ArtistID = Convert.ToInt32((reader["ID"].ToString()));
            }
            con.Close();
            return ArtistID;
        }

        public Song GetPlayingSongInfo(string songLink)
        {
            Song song = new Song();

            SqlCommand cmd = new SqlCommand("select * from [Song] WHERE SongLink = @SongLink", con);
            cmd.Parameters.AddWithValue("@SongLink", songLink);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                song.Name = (reader["Name"].ToString());
                song.ArtistID = Convert.ToInt32((reader["ArtistID"]));
                song.AlbumID = Convert.ToInt32((reader["AlbumID"]));
                song.GenreID = Convert.ToInt32((reader["GenreID"]));
            }
            con.Close();

            song.Genre = GenreName(song.GenreID);
            song.Artist = ArtistName(song.ArtistID);
            song.Album = AlbumName(song.AlbumID);

            return song;
        }

        public string GenreName(int genreID)
        {
            string genre = "";

            SqlCommand read = new SqlCommand(@"SELECT * FROM [Genre] WHERE ID = @genreID", con);
            read.Parameters.AddWithValue("@genreID", genreID);
            con.Open();
            SqlDataReader reader = read.ExecuteReader();
            while (reader.Read())
            {
                genre = (reader["Name"].ToString());
            }
            con.Close();
            return genre;
        }

        public string ArtistName(int artistID)
        {
            string artist = "";

            SqlCommand read = new SqlCommand(@"SELECT * FROM [Artist] WHERE ID = @artistID", con);
            read.Parameters.AddWithValue("@artistID", artistID);
            con.Open();
            SqlDataReader reader = read.ExecuteReader();
            while (reader.Read())
            {
                artist = (reader["Name"].ToString());
            }
            con.Close();
            return artist;
        }

        public string AlbumName(int albumID)
        {
            string album = "";

            SqlCommand read = new SqlCommand(@"SELECT * FROM [Album] WHERE ID = @albumID", con);
            read.Parameters.AddWithValue("@albumID", albumID);
            con.Open();
            SqlDataReader reader = read.ExecuteReader();
            while (reader.Read())
            {
                album = (reader["Name"].ToString());
            }
            con.Close();
            return album;
        }
    }
}
