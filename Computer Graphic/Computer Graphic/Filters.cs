using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computer_Graphic
{
    abstract class Filters
    {//вычисляет значение пикселя отфильтрованного изображения
        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);
        
        public virtual Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            // создаем пустое изображение такого же размера, что и исходное
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            // обход всех пикселей
            for(int i =0; i < resultImage.Width; i++)
            {
                // сигнализирует элементу worker о текущем процессе
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for(int j =0; j<resultImage.Height;j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }

        //приводит значение цвета к допустимому диапазону
        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

         public Bitmap subImage(Bitmap sourceImage1, Bitmap sourceImage2, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage1.Width, sourceImage1.Height);
            Color black = Color.FromArgb(0, 0, 0);
            for (int i = 0; i < sourceImage1.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage1.Height; j++)
                {
                    if (sourceImage1.GetPixel(i, j) != sourceImage2.GetPixel(i, j))
                    {
                        resultImage.SetPixel(i, j, black);
                    }
                }
            }
            return resultImage;
        }

        public Bitmap Init(Bitmap sourceImage, int _intensity)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            int intensity;
            Color sourceColor;
            for (int i = 0; i < resultImage.Width; i++)
            {
                for (int j = 0; j < resultImage.Height; j++)
                {
                    sourceColor = sourceImage.GetPixel(i, j);
                    intensity = (int)(0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B);
                    if (intensity < _intensity)
                        intensity = 0;
                    else
                        intensity = 1;

                    resultImage.SetPixel(i, j, Color.FromArgb(intensity, intensity, intensity));
                }
            }
            return resultImage;
        }
    }
}
