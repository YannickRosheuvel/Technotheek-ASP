using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technotheek.net_Core.ViewModels
{
    public class ProfilePictureViewModel
    {
        public IFormFile Image { get; set; }
    }
}
