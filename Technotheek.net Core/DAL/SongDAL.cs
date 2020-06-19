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
        private List<Song> songList = new List<Song>();

        //Zoekt naar nummer op basis van de input van de gebruiker
        public List<Song> LookUpSong(Song song)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", song.Name);

            List<Song> list = con.Query<Song>("LookUpSong", param, commandType: CommandType.StoredProcedure).ToList();
            songList = list;

            return new List<Song>(songList);
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

        // haalt informatie op van het spelende nummer
        public Song GetPlayingSongInfo(string songLink)
        {
            Song song = new Song();

            SqlCommand cmd = new SqlCommand("Select * from Song Inner Join Artist on Song.SongLink=@SongLink " +
                " Inner Join Album on Song.SongLink=@SongLink" +
                " Inner Join Genre on Song.SongLink=@SongLink", con);

            cmd.Parameters.AddWithValue("@SongLink", songLink);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                song.Name = (reader["Name"].ToString());
                song.Artist = (reader["ArtistName"].ToString());
                song.Genre = (reader["GenreName"].ToString());
                song.Album = (reader["AlbumName"].ToString());
            }

            con.Close();

            return song;
        }

        //Zet het pad van het nieuw toegevoegde nummer in de database
        public SongCreateViewModel AddNewSong(SongCreateViewModel songCreateViewModel)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Song] (Name, SongLink, GenreID, ArtistID, AlbumID) values (@NameSong , @SongPath, @Genre, @Artist, @Album)", con);
            cmd.Parameters.AddWithValue("@NameSong", songCreateViewModel.Name);
            cmd.Parameters.AddWithValue("@SongPath", songCreateViewModel.SongLink);
            cmd.Parameters.AddWithValue("@Genre", AddGenreID(songCreateViewModel));
            cmd.Parameters.AddWithValue("@Artist", AddArtistID(songCreateViewModel));
            cmd.Parameters.AddWithValue("@Album", AddAlbumID(songCreateViewModel));

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return songCreateViewModel;
        }

        public int AddAlbumID(SongCreateViewModel songCreateViewModel)
        {
            int AlbumID = 0;

            SqlCommand cmd = new SqlCommand(@"insert into Album (AlbumName)
                                            Select @Name Where not exists(select * from Album where AlbumName = @Name)", con);

            SqlCommand read = new SqlCommand(@"SELECT * FROM [Album] WHERE AlbumName = @Name", con);

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

        public int AddGenreID(SongCreateViewModel songCreateViewModel)
        {
            int GenreID = 0;

            SqlCommand read = new SqlCommand(@"SELECT * FROM [Genre] WHERE GenreName = @Name", con);
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

        public int AddArtistID(SongCreateViewModel songCreateViewModel)
        {
            int ArtistID = 0;

            SqlCommand cmd = new SqlCommand(@"insert into Artist (ArtistName)
                                            Select @Name Where not exists(select * from Artist where ArtistName = @Name)", con);
            SqlCommand read = new SqlCommand(@"SELECT * FROM [Artist] WHERE ArtistName = @Name", con);
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
    }
}
