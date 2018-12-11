﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Watch.Extensions
{
    public static class ColorExtension
    {
        public static string ToHex(this Color color)
        {
            return $@"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}