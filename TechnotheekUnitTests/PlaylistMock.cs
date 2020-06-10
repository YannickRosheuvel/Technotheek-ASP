using System;
using System.Collections.Generic;
using System.Text;
using Technotheek.net_Core.Interfaces;
using Technotheek.net_Core.Models;
using TechnotheekWeb.Models;

namespace TechnotheekUnitTests
{
    class PlaylistMock : IPlaylistDAL
    {

        public List<Playlist> playlist = new List<Playlist>
        {
            new Playlist
            {
                Name = "Yoda"
            }
        };

        public void AddNewPlaylist(string name, int ID)
        {
            Playlist list = new Playlist();
            list.Name = "yoda";
            playlist.Add(list);
        }

        public void AddSongToPlaylist(int songID, int playlistID)
        {
            throw new NotImplementedException();
        }

        public List<Song> GetPlaylistSongs(int selectedPlaylist)
        {
            throw new NotImplementedException();
        }

        public List<Playlist> RetrievePlaylists(int ID)
        {
            return new List<Playlist>(playlist);
        }
    }
}
