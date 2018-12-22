using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textify.Core.Data.Models;

namespace Textify.MVC.Models
{
    public class SiteTextViewModel
    {
        public SiteText Text { get; set; }
        public bool Collapsed { get; set; }

        public SiteTextViewModel()
        {

        }

        public SiteTextViewModel(SiteText text)
        {
            Text = text;
        }
    }
}