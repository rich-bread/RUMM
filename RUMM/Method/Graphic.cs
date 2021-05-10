using System.Drawing;
using System.IO;

namespace RUMM.Method
{
    public class Graphic
    {
        public static void Resize_Own(string original, int scale)
        {
            Bitmap trimedmap_precomp = new Bitmap(scale, scale);
            Graphics graphics = Graphics.FromImage(trimedmap_precomp);

            Bitmap trimedmap_comp = new Bitmap(original);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.DrawImage(trimedmap_comp, 0, 0, scale, scale);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            trimedmap_comp.Dispose();
            graphics.Dispose();

            trimedmap_precomp.Save(original);
        }

        public static void Resize_Copy(string original, string destination, int scale)
        {
            Bitmap trimedmap_precomp = new Bitmap(scale, scale);
            Graphics graphics = Graphics.FromImage(trimedmap_precomp);

            Bitmap trimedmap_comp = new Bitmap(original);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.DrawImage(trimedmap_comp, 0, 0, scale, scale);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            trimedmap_comp.Dispose();
            graphics.Dispose();

            trimedmap_precomp.Save(destination);
        }
    }
}