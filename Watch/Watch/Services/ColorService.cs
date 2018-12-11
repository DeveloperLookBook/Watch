using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Watch.Services
{
    public class ColorService : IColorService
    {
        public List<Color> Colors { get; }

        public ColorService()
        {
            this.Colors = this.CreateColorList();
        }

        List<Color> CreateColorList()
        {
            var colors = new List<Color>();
            var type   = typeof(Color);
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var field in fields)
            {
                if (field?.Name != null)
                {
                    var color = (Color)field.GetValue(type);
                    colors.Add(color);
                }
            }

            return colors;
        }
    }
}
