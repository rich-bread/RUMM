using System.Drawing;

namespace RUMM.Method
{
    public static class Call
    {
        public static void Device(string uploadedmap_path, string trimedmap_path)
        {
            Bitmap source = new Bitmap(uploadedmap_path);
            Rectangle rect;

            //↓PC(FHD)かどうかを判定
            if (source.Width == 1920 && source.Height == 1040 || source.Width == 1920 && source.Height == 1080)
            {
                rect = new Rectangle((int)580, (int)117, (int)759, (int)759);

                Bitmap trimed = source.Clone(rect, source.PixelFormat);

                trimed.Save(trimedmap_path);
                source.Dispose();
                trimed.Dispose();
            }
            else if (source.Width == 1920 && source.Height == 1030)
            {
                rect = new Rectangle((int)587, (int)123, (int)747, (int)747);

                Bitmap trimed = source.Clone(rect, source.PixelFormat);

                trimed.Save(trimedmap_path);
                source.Dispose();
                trimed.Dispose();
            }
            //↓iPad(第7世代10.2インチ)かどうかを判定
            else if (source.Width == 2160 && source.Height == 1620)
            {
                rect = new Rectangle((int)469, (int)135, (int)1222, (int)1222);
                Bitmap trimed = source.Clone(rect, source.PixelFormat);

                trimed.Save(trimedmap_path);
                source.Dispose();
                trimed.Dispose();
            }
            //デバイス: デスにゃんのスマホ
            else if (source.Width == 2340 && source.Height == 1080)
            {
                rect = new Rectangle((int)723, (int)90, (int)815, (int)815);
                Bitmap trimed = source.Clone(rect, source.PixelFormat);

                trimed.Save(trimedmap_path);
                source.Dispose();
                trimed.Dispose();
            }
            //デバイス: NintendoSwitch
            else if (source.Width == 1280 && source.Height == 720)
            {
                rect = new Rectangle((int)369, (int)60, (int)541, (int)541);
                Bitmap trimed = source.Clone(rect, source.PixelFormat);

                trimed.Save(trimedmap_path);
                source.Dispose();
                trimed.Dispose();
            }
            else
            {
                return;
            }
        }
    }
}