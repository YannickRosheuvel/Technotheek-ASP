using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using TechnotheekWeb.Models;


namespace TechnotheekWeb.Interfaces
{
    public interface ISongDAL
    {
        void AddNewSong(Song bel);
        string GetPathOfSelectedSong(string selectedSong);
        List<Song> GetAllSongs();
        List<Song> LookUpSong(Song bel);
        void AddNewPlaylist(string name, int ID);
        List<Playlist> RetrievePlaylists(int ID);

    }
}
