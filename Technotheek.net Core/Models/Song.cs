using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TechnotheekWeb.Models
{
    public class Song
    {

        public string SelectedSong { get; set; }
        public List<string> ListSongs { get; set; }
        public string NameSong { get; set; }
        public string SongPath { get; set; }
        public string SongsFound { get; set; }

    }
}
