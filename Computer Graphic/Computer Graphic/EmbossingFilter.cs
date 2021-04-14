using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computer_Graphic
{
    class EmbossingFilter : MatrixFilter
    {
        public EmbossingFilter()
        {
            int size = 3;
            // создаем ядро фильтра
            this.kernel = new float[size, size];
            this.kernel[0, 1] = this.kernel[1, 0]  = 1;
            this.kernel[1, 2] = this.kernel[2, 1] = -1;
            //this.kernel[1, 0] = this.kernel[1, 1] = this.kernel[1, 2] = 0;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            //вычисляем радиусы фильтра по высоте и ширине на основании матрицы  
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            //цветовые компоненты результируещего цвета
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;

            //перебираем окрестноть пикселя
            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    //проверяем, что не вышли за границы
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            }
            return Color.FromArgb(Clamp((int)(resultR + 128), 0, 255),
                                  Clamp((int)(resultG + 128), 0, 255),
                                  Clamp((int)(resultB + 128), 0, 255));
        }
    }
}
