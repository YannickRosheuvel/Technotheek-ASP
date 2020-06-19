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
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }


        public string GetSelectedSongPath(string selectedSong)
        {
            try
            {
                return songDAL.GetPathOfSelectedSong(selectedSong);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public List<Song> SearchSong(Song sb)
        {
            try
            {
                return songDAL.LookUpSong(sb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public SongCreateViewModel NewSongAdd(SongCreateViewModel viewModel)
        {
            try
            {
                return songDAL.AddNewSong(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public Song GetSongInfo(string songLink)
        {
            try
            {
                return songDAL.GetPlayingSongInfo(songLink);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
