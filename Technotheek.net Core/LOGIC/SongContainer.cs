using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
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

        public List<Song> ReturnAllSongs()
        {
            try
            {
                return songDAL.GetAllSongs();
            }
            catch
            {
                throw;
            }

        }


        public string GetSelectedSongPath(string selectedSong)
        {
            try
            {
                return songDAL.GetPathOfSelectedSong(selectedSong);
            }
            catch
            {
                throw;
            }
        }

        public List<Song> SearchSong(Song sb)
        {
            try
            {
                return songDAL.LookUpSong(sb);
            }
            catch
            {
                throw;
            }
        }

        public SongCreateViewModel NewSongAdd(SongCreateViewModel viewModel)
        {
            try
            {
                return songDAL.AddNewSong(viewModel);
            }
            catch
            {
                throw;
            }
        }

        public Song GetSongInfo(string songLink)
        {
            try
            {
                return songDAL.GetPlayingSongInfo(songLink);
            }
            catch
            {
                throw;
            }
        }
    }
}
