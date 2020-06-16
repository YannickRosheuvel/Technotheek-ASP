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
                Name = "Lekker lijste om te zagen"
            }
        };

        public List<Song> playlistSongs = new List<Song>
        {
            new Song
            {
                Name = "What I See",
                ID = 1
            }
        };

        public void AddNewPlaylist(string name, int ID)
        {
            Playlist list = new Playlist();
            list.Name = "Lekker lijste om te zagen";
            playlist.Add(list);
        }

        public PlaylistSongs AddSongToPlaylist(PlaylistSongs songsInPlaylist)
        {
            return songsInPlaylist;
        }

        public List<Song> GetPlaylistSongs(int selectedPlaylist)
        {
            List<Song> songsInPlaylist = new List<Song>();
            Song foundSong = new Song();
            foreach(var item in playlistSongs)
            {
                if(item.ID == selectedPlaylist)
                {
                    foundSong.ID = item.ID;
                    songsInPlaylist.Add(foundSong);
                    return songsInPlaylist;
                }
                else
                {
                    return songsInPlaylist;
                }
            }
            return songsInPlaylist;
        }

        public List<Playlist> RetrievePlaylists(int userID)
        {
            return new List<Playlist>(playlist);
        }
    }
}
