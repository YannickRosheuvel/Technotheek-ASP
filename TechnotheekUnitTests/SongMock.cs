using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.Interfaces;
using TechnotheekWeb.Models;

namespace Technotheek.Tests
{
    public class SongMock : ISongDAL
    {
        public List<Song> songList = new List<Song>()
        {
            new Song
            {
                SongLink = "What I See - DYEN",
                Name = "What I See - DYEN"
            }
        };
        SongCreateViewModel songCreateViewModel = new SongCreateViewModel()
        {
            Name = "What I See - DYEN",
            ID = 1,
            Genre = "Hard Techno",
            Artist = "DYEN",
            Album = "No Album"
        };

        List<Playlist> playlist = new List<Playlist>
        {
            new Playlist
            {
                Name = "What I See - DYEN"
            }
        };

        public List<Playlist> ReturnList()
        {
            return new List<Playlist>(playlist);
        }


        List<Song> mockList = new List<Song>
        {
            new Song
            {
                Name = "What I See - DYEN", SongLink = "1"
            },
            new Song
            {
                Name = "Darkness", SongLink = "2"
            },
            new Song
            {
                Name = "I want to be a hippy", SongLink = "3"
            },
            new Song
            {
                Name = "90's Rave", SongLink = "4"
            }

        };

        public List<Song> ReturSongList()
        {
            return new List<Song>(mockList);
        }

        public List<Song> GetAllSongs()
        {
            return new List<Song>(mockList);
        }


        public string GetPathOfSelectedSong(string songName)
        {
            for (int i = 0; i < mockList.Count; i++)
            {
                if (mockList[i].Name == songName)
                {
                    return mockList[i].SongLink;
                }
            }
            return "";
        }

        public List<Song> LookUpSong(Song bel)
        {
            for (int i = 0; i < mockList.Count; i++)
            {
                if (mockList[i].Name == "What I See - DYEN")
                {
                    bel.Name = mockList[i].Name;
                }
            }
            return new List<Song>();
        }

        public void AddNewPlaylist(string name, int ID)
        {
            playlist.Add(new Playlist { Name = "What I See - DYEN" });
        }

        public void AddSongToPlaylist(int songID, int playlistID)
        {
            throw new NotImplementedException();
        }

        public Song GetPlayingSongInfo(string songLink)
        {
            for (int i = 0; i < mockList.Count; i++)
            {
                if (songList[i].SongLink == songLink)
                {
                    return songList[i];
                }
            }
            return new Song();
        }

        public void AddNewSong(SongCreateViewModel songCreateViewModel)
        {
            mockList.Add(new Song { Name = songCreateViewModel.Name, SongLink = songCreateViewModel.SongLink });
        }
    }
}
