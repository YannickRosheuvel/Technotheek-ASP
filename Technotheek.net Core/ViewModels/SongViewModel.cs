using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.Models;

namespace TechnotheekWeb.ViewModels
{
    public class SongViewModel
    {
        public SongCreateViewModel Song { get; set; }
        public string SongsFound { get; set; }
    }
}