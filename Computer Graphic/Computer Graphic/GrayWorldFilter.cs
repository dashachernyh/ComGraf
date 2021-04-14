using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;

namespace Computer_Graphic
{
    class GrayWorldFilter : Filters
    {
         protected int Avg;
         protected int R, G, B;
         public GrayWorldFilter(Bitmap sourceImage)
         {
             int size = sourceImage.Width * sourceImage.Height;
             R = 0;
             G = 0;
             B = 0;
             for (int i = 0; i < sourceImage.Width; i++)
             {
                 for (int j = 0; j < sourceImage.Height; j++)
                 {
                     Color currentColor = sourceImage.GetPixel(i, j);
                     R += currentColor.R;
                     G += currentColor.G;
                     B += currentColor.B;
                 }
             }
             R =R / size;
             G = G / size;
             B = B / size;
             Avg = (R + G + B) / 3;
         }
         protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
         {
             Color sourceColor = sourceImage.GetPixel(x, y);
             int resR = sourceColor.R * Avg / R;
             int resG = sourceColor.G * Avg / G;
             int resB = sourceColor.B * Avg / B;
             resR = Clamp(resR, 0, 255);
             resG = Clamp(resG, 0, 255);
             resB = Clamp(resB, 0, 255);
             Color resultColor = Color.FromArgb(resR, resG, resB);
             return resultColor;
         }
     }
}