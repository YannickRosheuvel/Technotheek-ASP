﻿using System;
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
            try
            {
                playlistDAL.AddNewPlaylist(name, ID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public List<Playlist> GetPlaylists(int userID)
        {
            try
            {
                return playlistDAL.RetrievePlaylists(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public List<Song> RetrievePlaylistSongs(int selectedPlaylist)
        {
            try
            {
                return playlistDAL.GetPlaylistSongs(selectedPlaylist);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public PlaylistSongs AddSongPlaylist(PlaylistSongs playlistSongs)
        {
            try
            {
                return playlistDAL.AddSongToPlaylist(playlistSongs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}
