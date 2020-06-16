using Microsoft.VisualStudio.TestTools.UnitTesting;
using Technotheek.net_Core.Containers;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using Technotheek.Tests;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;

namespace TechnotheekUnitTests
{
    [TestClass]
    public class PlaylistTests
    {

        [TestMethod]
        public void Adds_A_New_Playlist()
        {

            PlaylistMock playlisMock = new PlaylistMock();
            PlaylistContainer playlistContainer = new PlaylistContainer(playlisMock);

            playlistContainer.MakeNewPlaylist("Lekker lijste om te zagen", 1);
            var expected = 2;
            var actual = playlisMock.playlist.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Returns_the_Desired_Songs()
        {

            PlaylistMock playlisMock = new PlaylistMock();
            PlaylistContainer playlistContainer = new PlaylistContainer(playlisMock);

            var actual = playlistContainer.RetrievePlaylistSongs(1);
            var expected = 1;

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void Returns_Correct_Property()
        {

            PlaylistMock playlisMock = new PlaylistMock();
            PlaylistContainer playlistContainer = new PlaylistContainer(playlisMock);

            var actual = playlistContainer.GetPlaylists(1).Count;
            var expected = 1;

            Assert.AreEqual(expected, actual);
        }

        
        [TestMethod]
        public void The_Correct_Song_Is_Added_To_The_Correct_Playlist()
        {

            PlaylistMock playlisMock = new PlaylistMock();
            PlaylistContainer playlistContainer = new PlaylistContainer(playlisMock);
            PlaylistSongs playlistSongs = new PlaylistSongs();

            playlistSongs.playlistID = 1;
            playlistSongs.songID = 1;

            var actual = playlistContainer.AddSongPlaylist(playlistSongs);
            var expected = 1;

            Assert.AreEqual(expected, actual.songID);
            Assert.AreEqual(expected, actual.playlistID);
        }
    }
}
