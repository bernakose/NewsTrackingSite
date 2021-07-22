using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Models.ViewModels
{
    public class NSelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}