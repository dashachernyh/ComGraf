using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computer_Graphic
{
    class MedianFilter : Filters
    {
        protected int[] matrix = null;
        protected int median;
        protected int size = 3;

        public MedianFilter()
        {
            this.matrix = new int[size * size];
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            //вычисляем радиусы фильтра по высоте и ширине на основании матрицы  
            int radiusX = size / 2;
            int radiusY = size / 2;

            //перебираем окрестноть пикселя
            for (int i = -radiusY; i <= radiusY; i++)
            {
                for (int j = -radiusX; j <= radiusX; j++)
                {
                    //проверяем, что не вышли за границы
                    int idX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + i, 0, sourceImage.Height - 1);
                    Color sourceColor = sourceImage.GetPixel(idX, idY);
                    this.matrix[(j + radiusX) * size + i + radiusY] = (sourceColor.R + sourceColor.G + sourceColor.B) / 3;
                    int m = this.matrix[(j + radiusX) * size + i + radiusY];
                    int it = (j + radiusX) *size + i + radiusY;
                }
            }
            Array.Sort(matrix, 0, size * size - 1);
            
            median = this.matrix[(size * size) / 2 + 1];
         
            return Color.FromArgb(Clamp(median, 0, 255),
                                  Clamp(median, 0, 255),
                                  Clamp(median, 0, 255));
        }
    }
}