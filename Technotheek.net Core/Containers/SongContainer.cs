using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnotheekWeb.Interfaces;
using TechnotheekWeb.Models;

namespace TechnotheekWeb.Containers
{
    public class SongContainer
    {
        ISongDAL songDAL;

        public SongContainer(ISongDAL songDAL)
        {
            this.songDAL = songDAL;
        }

        //public List<SongBELList> ReturnSearchSongs(BEL bel)
        //{
        //    return songDAL.SearchSong(bel);
        //}

        public List<SongListBEL> ReturnAllSongs()
        {
            return songDAL.GetAllSongs();
        }


        public string GetSelectedSongPath(string selectedSong)
        {
            return songDAL.GetPathOfSelectedSong(selectedSong);
        }

        public List<SongListBEL> SearchSong(Song sb)
        {
            return songDAL.LookUpSong(sb);
        }

        public void NewSongAdd(Song sb)
        {
            songDAL.AddNewSong(sb);
        }
        //public string ReturnIfSongsFound(SongBEL sb)
        //{
        //    return songDAL.CheckForResult(sb);
        //}
    }
}
