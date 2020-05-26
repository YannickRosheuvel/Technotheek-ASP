using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnotheekWeb.Models;

namespace TechnotheekWeb.ViewModels
{
    public class SongViewModel
    {
        public Song Song { get; set; }
        public string SongsFound { get; set; }
    }
}