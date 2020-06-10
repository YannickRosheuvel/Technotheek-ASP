using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using TechnotheekWeb.Interfaces;
using TechnotheekWeb.Models;

namespace Technotheek.Tests
{
    public class SongMock : ISongDAL
    {
        public List<Song> songListBel = new List<Song>();

        List<Playlist> playlist = new List<Playlist>
        {
            new Playlist
            {
                Name = "Yoda"
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
                Name = "hey", SongLink = "1"
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

        public void AddNewSong(Song bel)
        {
            mockList.Add(new Song { Name = "hoi", SongLink = "5" });
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
                if (mockList[i].Name == "hey")
                {
                    bel.Name = mockList[i].Name;
                }
            }
            return new List<Song>();
        }

        public void AddNewPlaylist(string name, int ID)
        {
            playlist.Add(new Playlist { Name = "Yoda" });
        }

        public List<Playlist> RetrievePlaylists(int ID)
        {
            return new List<Playlist>(playlist);
        }

        public List<Song> GetPlaylistSongs(string selectedPlaylist)
        {
            throw new NotImplementedException();
        }

        public void AddSongToPlaylist(int songID, int playlistID)
        {
            throw new NotImplementedException();
        }

        public Song GetPlayingSongInfo(string songLink)
        {
            throw new NotImplementedException();
        }
    }
}
