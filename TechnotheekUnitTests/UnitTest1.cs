using Microsoft.VisualStudio.TestTools.UnitTesting;
using Technotheek.net_Core.ViewModels;
using Technotheek.Tests;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;

namespace TechnotheekUnitTests
{
    [TestClass]
    public class UnitTest1
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

            SongMock iSongMock = new SongMock();
            Song song = new Song();
            SongContainer songContainer = new SongContainer(iSongMock);

            songContainer.NewSongAdd(song);
            var actual = songContainer.ReturnAllSongs().Count;
            var expected = 5;

            Assert.AreEqual(expected , actual);

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
        public void Retreive_The_Correct_User_Data()
        {

            UserMock userMock = new UserMock();
            UserContainer userContainer = new UserContainer(userMock);

            var actual = userContainer.RetrieveUserData(1);
            var expected = "Yoda";

            Assert.AreEqual(expected, actual.Username);
        }

        [TestMethod]
        public void User_Logs_In_Correctely()
        {

            UserMock userMock = new UserMock();
            User user = new User();
            UserContainer userContainer = new UserContainer(userMock);
            Login login = new Login();

            var actual = userContainer.LoginUser("Yoda", "123", 1);

            Assert.AreEqual("Yoda"  , actual.Username);
        }

        [TestMethod]
        public void User_Is_Registered_Correctely()
        {

            UserMock userMock = new UserMock();
            User user = new User();
            UserContainer userContainer = new UserContainer(userMock);

            userContainer.AddUser(user, "Yoda", "", 123, "", "", "", 11, "", 1);
            var expected = "Yoda";

            Assert.AreEqual(expected, userMock.user.Username);
        }

        [TestMethod]
        public void See_If_Song_Is_Added()
        {

            SongMock iSongMock = new SongMock();
            Song song = new Song();
            SongContainer songContainer = new SongContainer(iSongMock);

            songContainer.NewSongAdd(song);

            var actual = iSongMock.ReturnList();

            Assert.AreEqual(1, actual.Count) ;
        }

        [TestMethod]
        public void See_If_Returns_Playlists()
        {

            SongMock iSongMock = new SongMock();
            Song song = new Song();
            SongContainer songContainer = new SongContainer(iSongMock);

            var actual = songContainer.GetPlaylists(10);

            Assert.IsNotNull(actual);
        }
    }
}
