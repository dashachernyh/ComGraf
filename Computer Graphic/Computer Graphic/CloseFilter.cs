using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Computer_Graphic
{
    class CloseFilter: MatrixFilter
    {
        public CloseFilter(int size)
        {
            this.kernel = new float[size, size];
            // создаем ядро фильтра
            
            for (int i = 0; i < size ; i++)
                for (int j = 0; j < size; j++)
                    this.kernel[i, j] = 1;

            kernel[0, 0] = kernel[0, size - 1] = kernel[size - 1, 0] = kernel[size - 1, size - 1] = 0;
        }

        protected int CheckPixelColor(Bitmap sourceImage, int x, int y)
        {
            int flag = 0;
            int count = 0;
            //вычисляем радиусы фильтра по высоте и ширине на основании матрицы  
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            //цветовые компоненты результируещего цвета
            if (sourceImage.GetPixel(x, y).R == 0)
            {
                return flag = 0;
            }
            else
            {
                //перебираем окрестноть пикселя
                for (int l = -radiusY; l <= radiusY; l++)
                {
                    if (flag == 1 && count == kernel.Length)
                        break;
                    else
                    {
                        for (int k = -radiusX; k <= radiusX; k++)
                        {
                            //проверяем, что не вышли за границы
                            int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                            int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                            Color neighborColor = sourceImage.GetPixel(idX, idY);
                            if (neighborColor.R == 1)
                            {
                                flag = 1;
                                count++;
                            }
                        }
                    }
                }
                return flag;
            }
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            int intensity = 107;
            int flag = 0;
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            // создаем пустое изображение такого же размера, что и исходное
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Bitmap resultImage2 = new Bitmap(sourceImage.Width, sourceImage.Height);
            sourceImage = Init(sourceImage, intensity);
            // обход всех пикселей
            
            for (int i = 0; i < resultImage.Width; i++)
            {
                // сигнализирует элементу worker о текущем процессе
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < resultImage.Height; j++)
                {
                    if (sourceImage.GetPixel(i, j).R == 0)
                    {
                        for (int l = -radiusY; l <= radiusY; l++)
                        {
                            for (int k = -radiusX; k <= radiusX; k++)
                            {
                                //проверяем, что не вышли за границы
                                int idX = Clamp(i + k, 0, resultImage.Width - 1);
                                int idY = Clamp(j + l, 0, resultImage.Height - 1);
                                Color neighborColor = sourceImage.GetPixel(idX, idY);
                                if (neighborColor.R == 1)
                                {
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        if (flag == 1)
                            resultImage.SetPixel(i, j, Color.FromArgb(1, 1, 1));
                        else
                            resultImage.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }

            for (int i = 0; i < resultImage2.Width; i++)
            {
                // сигнализирует элементу worker о текущем процессе
                worker.ReportProgress((int)((float)i / resultImage2.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < resultImage2.Height; j++)
                {
                    if (CheckPixelColor(resultImage, i, j) == 0)
                        resultImage2.SetPixel(i, j, Color.FromArgb(1, 1, 1));
                }
            }
            return resultImage2;
        }
    }
}
