using Microsoft.VisualStudio.TestTools.UnitTesting;
using Technotheek.net_Core.Containers;
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
        public void See_If_Returns_Playlists()
        {

            PlaylistMock playlisMock = new PlaylistMock();
            PlaylistContainer playlistContainer = new PlaylistContainer(playlisMock);

            var actual = playlistContainer.GetPlaylists(10);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Adds_A_New_Playlist()
        {

            PlaylistMock playlisMock = new PlaylistMock();
            PlaylistContainer playlistContainer = new PlaylistContainer(playlisMock);

            playlistContainer.MakeNewPlaylist("Yoda", 1);
            var expected = 2;
            var actual = playlisMock.playlist.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Returns_Correct_Property()
        {

            PlaylistMock playlisMock = new PlaylistMock();
            PlaylistContainer playlistContainer = new PlaylistContainer(playlisMock);



            Assert.AreEqual(expected, actual);
        }
    }
}
