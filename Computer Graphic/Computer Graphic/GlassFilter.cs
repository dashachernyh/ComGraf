using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computer_Graphic
{
    class GlassFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Random rnd = new Random();
            Random rnd1 = new Random();
            double value_x = rnd.NextDouble();
            double value_y = rnd1.NextDouble();
            int new_x = (int)(x + (value_x - 0.5) * 10);
            int new_y = (int)(y + (value_y - 0.5) * 10);
            new_x = Clamp(new_x, 0, sourceImage.Width - 1);
            new_y = Clamp(new_y, 0, sourceImage.Height - 1);
            Color resultColor = sourceImage.GetPixel(new_x, new_y); ;
            return resultColor;
        }
    }
}
