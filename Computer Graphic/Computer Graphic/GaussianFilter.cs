using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computer_Graphic
{
    class GaussianFilter : MatrixFilter
    {
        //создает ядро 7*7 с параметром сигма 2
        public GaussianFilter()
        {
            creatGaussianKernel(7, 2);
        }
        public void creatGaussianKernel(int radius, float sigma)
        {
            // размер ядра 
            int size = 2 * radius + 1;
            // создаем ядро фильтра
            this.kernel = new float[size, size];
            //коэффициент нормировки ядра 
            float norm = 0;
            //расчитываем ядро линейного фильтра
            for (int i = -radius; i<= radius; i++)
                for(int j = -radius; j <= radius; j++)
                {
                    this.kernel[i + radius, j + radius] = (float)(Math.Exp(-(i * i + j * j) / (2 * sigma * sigma)));
                    norm += this.kernel[i + radius, j + radius];
                }
            //нормируем ядро по сумме
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    this.kernel[i, j] /= norm;
        }
    }
}
