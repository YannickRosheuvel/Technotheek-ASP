using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using TechnotheekWeb.Models;

namespace Technotheek.net_Core.Interfaces
{
    public interface IPlaylistDAL
    {
        List<Playlist> RetrievePlaylists(int ID);
        List<Song> GetPlaylistSongs(int selectedPlaylist);
        PlaylistSongs AddSongToPlaylist(PlaylistSongs playlistSongs);
        void AddNewPlaylist(string name, int ID);
    }
}
