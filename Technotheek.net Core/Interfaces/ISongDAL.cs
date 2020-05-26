using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnotheekWeb.Models;


namespace TechnotheekWeb.Interfaces
{
    public interface ISongDAL
    {
        void AddNewSong(Song bel);
        string GetPathOfSelectedSong(string selectedSong);
        List<SongListBEL> GetAllSongs();
        List<SongListBEL> LookUpSong(Song bel);
    }
}
