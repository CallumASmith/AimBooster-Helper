using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AimBooster_Helper
{
    public class Detect
    {
        public static int[][] GetPixels()
        {
            using (Bitmap image = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.CopyFromScreen(new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - Screen.PrimaryScreen.WorkingArea.Height / 2), Point.Empty, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
                }
                return GetPixelArray(image);
            }
        }
        private static bool ContainSameElements(int[] first, int firstStart, int second, int secondStart, int length)
        {
            for (int i = 0; i < length; ++i)
            {
                if (first[i + firstStart] != second)
                {
                    return false;
                }
            }
            return true;
        }
        public static IEnumerable<Point> FindPixels(int[][] Pixels)
        {
            var y = 0;
            foreach (var pixel in Pixels.Take(Screen.PrimaryScreen.Bounds.Height -1))
            {
                for (int x = 0, n = pixel.Length - 1; x < n; ++x)
                {
                    if (ContainSameElements(pixel, x, Settings.pixelColour, 0, 1))
                    {
                        yield return new Point(x, y);
                    }
                }
                y += 1;
            }
        }
        public static Point? FindBest(IEnumerable<Point> points)
        {
            if (points.Count() > 0)
            {
                return points.First();
            }
            return null;
            IEnumerable<Point> orderedPoints = points.OrderByDescending(p => p.X);          
            if (orderedPoints.Count() > 0)
            {
                return orderedPoints.ElementAt(new Random().Next(orderedPoints.Count() -1));
            }
            return null;
        }
        private static int[][] GetPixelArray(Bitmap bitmap)
        {
            var result = new int[bitmap.Height][];
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            for (int y = 0; y < bitmap.Height; ++y)
            {
                result[y] = new int[bitmap.Width];
                Marshal.Copy(bitmapData.Scan0 + y * bitmapData.Stride, result[y], 0, result[y].Length);
            }

            bitmap.UnlockBits(bitmapData);

            return result;
        }
    }
}
