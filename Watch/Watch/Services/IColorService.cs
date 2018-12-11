using System.Collections.Generic;
using Xamarin.Forms;

namespace Watch.Services
{
    public interface IColorService
    {
        List<Color> Colors { get; }
    }
}