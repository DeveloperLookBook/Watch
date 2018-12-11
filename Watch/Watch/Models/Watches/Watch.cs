using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xamarin.Forms;

namespace Watch.Models.Watches
{
    public class Watch : Model<int>, IWatch
    {
        public TimeZoneInfo TimeZone    { get; set; }
        public Color        DialColor   { get; set; } 
        public Color        ArrowsColor { get; set; }
    }
}
