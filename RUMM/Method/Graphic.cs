using System.Drawing;
using System.IO;

namespace RUMM.Method
{
    public class Graphic
    {
        public static void Resize_Own(string trimedmap_path, string trimedmap_path2, int scale)
        {
            Bitmap trimedmap_precomp = new Bitmap(scale, scale);
            Graphics graphics = Graphics.FromImage(trimedmap_precomp);

            Bitmap trimedmap_comp = new Bitmap(trimedmap_path2);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.DrawImage(trimedmap_comp, 0, 0, scale, scale);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            trimedmap_comp.Dispose();
            graphics.Dispose();

            trimedmap_precomp.Save(trimedmap_path2);

            File.Copy(trimedmap_path2, trimedmap_path, true);
            File.Delete(trimedmap_path2);
        }

        public static void Resize_Copy(string trimedmap_path, string trimedmap_path2, int scale)
        {
            Bitmap trimedmap_precomp = new Bitmap(scale, scale);
            Graphics graphics = Graphics.FromImage(trimedmap_precomp);

            Bitmap trimedmap_comp = new Bitmap(trimedmap_path);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.DrawImage(trimedmap_comp, 0, 0, scale, scale);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            trimedmap_comp.Dispose();
            graphics.Dispose();

            trimedmap_precomp.Save(trimedmap_path2);
        }
    }
}