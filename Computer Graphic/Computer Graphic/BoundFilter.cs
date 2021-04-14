using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computer_Graphic
{
    class BoundFilter : MatrixFilter
    {
        public BoundFilter()
        {
            int size = 3;
            // создаем ядро фильтра
            this.kernel = new float[size, size];
            this.kernel[0, 0] = this.kernel[2, 0] = 3;
            this.kernel[0, 2] = this.kernel[2, 2] = -3;
            this.kernel[1, 0] = this.kernel[1, 2] = 10;
        }
    }
}
