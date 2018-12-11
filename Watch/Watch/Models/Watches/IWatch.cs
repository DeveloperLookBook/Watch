using System;
using Xamarin.Forms;

namespace Watch.Models.Watches
{
    public interface IWatch : IModel<int>
    {
        TimeZoneInfo TimeZone    { get; set; }
        Color        ArrowsColor { get; set; }
        Color        DialColor   { get; set; }
    }
}