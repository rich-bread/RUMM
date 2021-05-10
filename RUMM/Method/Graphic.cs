using System.Drawing;
using System.IO;

namespace RUMM.Method
{
    public class Graphic
    {
        public static void Resize_Own(string original, int scale)
        {
            Bitmap copy = new Bitmap(scale, scale);
            Graphics graphics = Graphics.FromImage(copy);

            Bitmap source = new Bitmap(original);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.DrawImage(source, 0, 0, scale, scale);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            source.Dispose();
            graphics.Dispose();

            copy.Save(original);
        }

        public static void Resize_Copy(string original, string destination, int scale)
        {
            Bitmap copy = new Bitmap(scale, scale);
            Graphics graphics = Graphics.FromImage(copy);

            Bitmap source = new Bitmap(original);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.DrawImage(source, 0, 0, scale, scale);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            source.Dispose();
            graphics.Dispose();

            copy.Save(destination);
        }
    }
}