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
            return songDAL.GetAllSongs();
        }


        public string GetSelectedSongPath(string selectedSong)
        {
            return songDAL.GetPathOfSelectedSong(selectedSong);
        }

        public List<Song> SearchSong(Song sb)
        {
            return songDAL.LookUpSong(sb);
        }

        public void NewSongAdd(SongCreateViewModel viewModel)
        {
            songDAL.AddNewSong(viewModel);
        }

        public Song GetSongInfo(string songLink)
        {
            return songDAL.GetPlayingSongInfo(songLink);
        }
    }
}
