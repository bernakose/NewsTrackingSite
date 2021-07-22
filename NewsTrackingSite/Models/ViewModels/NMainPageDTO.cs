using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Models.ViewModels
{
    public class NMainPageDTO
    {
        public List<byte[]> Posters { get; set; }
        public List<Genre> Categories { get; set; }
        public List<News> RandomNews { get; set; }
    }
}