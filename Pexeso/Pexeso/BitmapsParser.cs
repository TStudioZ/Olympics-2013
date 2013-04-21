using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso
{
    static class BitmapsParser
    {
        public static Bitmap LoadBitmap(string filename)
        {
            Bitmap bmp = null;
            try
            {
                bmp = new Bitmap(filename);
                bmp = new Bitmap(bmp, Grid.colWidth, Grid.colHeight);
            }
            catch (Exception)
            {
                return null;
            }
            return bmp;
        }
    }
}
