using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technotheek.net_Core.ViewModels
{
    public class SongCreateViewModel
    {
        public IFormFile Song { get; set; }
    }
}
