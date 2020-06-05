using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technotheek.net_Core.Interfaces;
using Technotheek.net_Core.Models;
using TechnotheekWeb;
using TechnotheekWeb.Models;

namespace Technotheek.net_Core.Containers
{
    public class PlaylistContainer
    {
        IPlaylistDAL playlistDAL;

        public PlaylistContainer(IPlaylistDAL playlistDAL)
        {
            this.playlistDAL = playlistDAL;
        }

        public void MakeNewPlaylist(string name, int ID)
        {
            playlistDAL.AddNewPlaylist(name, ID);
        }

        public List<Playlist> GetPlaylists(int ID)
        {
            return playlistDAL.RetrievePlaylists(ID);
        }

        public List<Song> RetrievePlaylistSongs(string selectedPlaylist)
        {
            return playlistDAL.GetPlaylistSongs(selectedPlaylist);
        }

        public void AddSongPlaylist(int songID, int playlistID)
        {
            playlistDAL.AddSongToPlaylist(songID, playlistID);
        }
    }
}
