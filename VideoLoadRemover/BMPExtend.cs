using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace VideoLoadRemover
{
    public static class BMPExtend
    {
        public static Response GenResponse(Bitmap bmp, Bitmap savedbmp)
        {
            Bitmap bm1 = new Bitmap(bmp, new Size(bmp.Width / 2, bmp.Height / 2));
            Bitmap bm2 = new Bitmap(savedbmp, new Size(savedbmp.Width / 2, savedbmp.Height / 2));

            float val1 = 0;

            string response1 = "";

            try
            {
                val1 = (Compare(bm1, bm2) + 1) / 10000000;

                response1 = val1.ToString();
            }
            catch
            {
                //if compare functions fail
                response1 = "Image Size Error";
            }

            return new Response(response1, val1);
        }

        public static float Compare(Bitmap bmp1, Bitmap bmp2)
        {
            List<Color> im1 = new List<Color>(HistoGram(bmp1));
            List<Color> im2 = new List<Color>(HistoGram(bmp2));

            float val = 0;
            for (int i = 0; i < im1.Count; i++)
            {
                val += Math.Abs((float)(im1.ElementAt(i).ToArgb() - im2.ElementAt(i).ToArgb()));
            }

            return val;
        }

        public static List<Color> HistoGram(Bitmap bitmap)
        {
            Bitmap newbitmap = (Bitmap)bitmap.Clone();
            // Store the histogram in a dictionary          
            List<Color> histo = new List<Color>();
            for (int x = 0; x < newbitmap.Width; x++)
            {
                for (int y = 0; y < newbitmap.Height; y++)
                {
                    // Get pixel color 
                    Color c = newbitmap.GetPixel(x, y);
                    // If it exists in our 'histogram' increment the corresponding value, or add new
                    histo.Add(c);
                }
            }
            return histo;
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
