using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.Models;


namespace TechnotheekWeb.Interfaces
{
    public interface ISongDAL
    {
        SongCreateViewModel AddNewSong(SongCreateViewModel songCreateViewModel);
        string GetPathOfSelectedSong(string selectedSong);
        List<Song> GetAllSongs();
        List<Song> LookUpSong(Song bel);
        Song GetPlayingSongInfo(string songLink);
    }
}
