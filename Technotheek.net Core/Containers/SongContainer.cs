using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
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

        public void NewSongAdd(Song sb)
        {
            songDAL.AddNewSong(sb);
        }
        //public string ReturnIfSongsFound(SongBEL sb)
        //{
        //    return songDAL.CheckForResult(sb);
        //}

        public void MakeNewPlaylist(string name, int ID)
        {
            songDAL.AddNewPlaylist(name, ID);
        }

        public List<Playlist> GetPlaylists(int ID)
        {
           return songDAL.RetrievePlaylists(ID);
        }
    }
}
