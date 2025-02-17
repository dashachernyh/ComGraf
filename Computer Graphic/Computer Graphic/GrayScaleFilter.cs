﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computer_Graphic
{
    class GrayScaleFilter : Filters
    {
        // вычисляет инверсию исходного цвета
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int intensity;
            Color sourceColor = sourceImage.GetPixel(x, y);
            intensity = (int)(0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B);
            Color resultColor = Color.FromArgb(intensity,
                                                intensity,
                                                intensity);
            return resultColor;
        }
    }
}
