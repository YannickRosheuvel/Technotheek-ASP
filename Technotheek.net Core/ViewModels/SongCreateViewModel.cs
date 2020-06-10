using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Technotheek.net_Core.ViewModels
{
    public class SongCreateViewModel
    {
        [Required]
        public IFormFile Song { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Album { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public string Length { get; set; }
        public string SongLink { get; set; }
        public int ID { get; set; }
    }
}
