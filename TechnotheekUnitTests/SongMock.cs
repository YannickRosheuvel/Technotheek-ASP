using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnotheekWeb.Interfaces;
using TechnotheekWeb.Models;

namespace Technotheek.Tests
{
    public class SongMock : ISongDAL
    {
        public List<SongListBEL> songListBel = new List<SongListBEL>();
        List<SongListBEL> mockList = new List<SongListBEL>
        {
            new SongListBEL
            {
                Name = "hey", SongLink = "1"
            },
            new SongListBEL
            {
                Name = "Darkness", SongLink = "2"
            },
            new SongListBEL
            {
                Name = "I want to be a hippy", SongLink = "3"
            },
            new SongListBEL
            {
                Name = "90's Rave", SongLink = "4"
            }

        };

        public List<SongListBEL> ReturSongListBEL()
        {
            return new List<SongListBEL>(mockList);
        }

        public List<SongListBEL> GetAllSongs()
        {
            return new List<SongListBEL>(mockList);
        }

        public void AddNewSong(Song bel)
        {
            mockList.Add(new SongListBEL { Name = "hoi", SongLink = "5" });
        }

        public string GetPathOfSelectedSong(string songName)
        {
            for (int i = 0; i < mockList.Count; i++)
            {
                if (mockList[i].Name == songName)
                {
                    return mockList[i].SongLink;
                }
            }
            return "";
        }

        public List<SongListBEL> LookUpSong(Song bel)
        {
            for (int i = 0; i < mockList.Count; i++)
            {
                if (mockList[i].Name == "hey")
                {
                    bel.NameSong = mockList[i].Name;
                }
            }
            return new List<SongListBEL>();
        }

    }
}
