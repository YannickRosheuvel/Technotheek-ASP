using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnotheekWeb.Models
{
    public class Song
    {
        public string Name { get; set; }
        public int Lenght { get; set; }
        public string SongLink { get; set; }
        public int ID { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Artist { get; set; }
        public int AlbumID { get; set; }
        public int GenreID { get; set; }
        public int ArtistID { get; set; }
    }
}
