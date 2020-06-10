using Microsoft.VisualStudio.TestTools.UnitTesting;
using Technotheek.net_Core.Containers;
using Technotheek.net_Core.ViewModels;
using Technotheek.Tests;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;

namespace TechnotheekUnitTests
{
    [TestClass]
    public class SongTests
    {
        [TestMethod]
        public void See_If_Searched_Songs_Is_Correct()
        {
            var expected = "hey";
            Song song = new Song();
            SongMock iSongMock = new SongMock();
            SongContainer songContainer = new SongContainer(iSongMock);

            songContainer.SearchSong(song);
            var actual = song.Name;

            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void See_If_Returned_List_Is_Correct()
        {
            SongCreateViewModel song = new SongCreateViewModel();
            SongMock iSongMock = new SongMock();
            SongContainer sc = new SongContainer(iSongMock);


            var actual = sc.ReturnAllSongs().Count;

            var expected = iSongMock.ReturSongList().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void All_Songs_Are_Returned_And_Are_Correct()
        {

            SongMock iSongMock = new SongMock();
            SongContainer songContainer = new SongContainer(iSongMock);

            var actual = songContainer.ReturnAllSongs();
            int expected = 4;

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void See_If_Correct_Song_Is_Added()
        {
            SongCreateViewModel songCreateViewModel = new SongCreateViewModel();

            SongMock iSongMock = new SongMock();
            SongContainer songContainer = new SongContainer(iSongMock);

            songContainer.NewSongAdd(songCreateViewModel);
            var actual = songContainer.ReturnAllSongs().Count;
            var expected = 5;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Gets_Info_Of_Playing_Song()
        {
            SongMock iSongMock = new SongMock();
            SongContainer songContainer = new SongContainer(iSongMock);

            var actual = songContainer.GetSongInfo("Yoda").Name;
            var expected = "Yoda";

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Returns_Correct_Path_Of_Song()
        {

            SongMock iSongMock = new SongMock();
            SongCreateViewModel song = new SongCreateViewModel();
            SongContainer songContainer = new SongContainer(iSongMock);

            var actual = songContainer.GetSelectedSongPath("hey");

            var expected = "1";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void See_If_Song_Is_Added()
        {
            SongCreateViewModel songCreateViewModel = new SongCreateViewModel();

            SongMock iSongMock = new SongMock();
            SongContainer songContainer = new SongContainer(iSongMock);

            songContainer.NewSongAdd(songCreateViewModel);

            var actual = iSongMock.ReturnList();

            Assert.AreEqual(1, actual.Count);
        }
    }
}
