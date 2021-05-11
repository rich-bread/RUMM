using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace RUMM.Method
{
    public class Integrate
    {
        public static void Original(string trimedmap_folder, string compmap_path, int scale)
        {
            List<string> list_trimedmap_path = new List<string>();  //最小地図のファイルパスのリスト
            List<int> xcoordlist = new List<int>();                 //最小地図のXのリスト
            List<int> zcoordlist = new List<int>();                 //最小地図のZのリスト

            //最小地図フォルダ内を検索
            var trimedmap_info = Directory.EnumerateFiles(trimedmap_folder, "*", SearchOption.AllDirectories);

            //最小地図フォルダから、ファイルパス・Xナンバリング・Zナンバリングをそれぞれのリストに追加
            foreach (string info in trimedmap_info)
            {
                list_trimedmap_path.Add(info);

                string xcoord = info.Split(',')[0].Replace(trimedmap_folder + "\\", "");
                string zcoord = info.Split(',')[1].Replace(".png", "");

                xcoordlist.Add(int.Parse(xcoord));
                zcoordlist.Add(int.Parse(zcoord));
            }

            //X・Zの最大・最小数の絶対値 + 1 で統合地図サイズを計算
            int x_range = (Math.Abs(xcoordlist.Max()) + Math.Abs(xcoordlist.Min()) + 1) * scale;
            int z_range = (Math.Abs(zcoordlist.Max()) + Math.Abs(zcoordlist.Min()) + 1) * scale;

            //↑で算出した統合地図サイズの画像を作成
            Bitmap bitmap = new Bitmap(x_range, z_range);
            Graphics g = Graphics.FromImage(bitmap);

            g.TranslateTransform(Math.Abs(xcoordlist.Min()) * scale, Math.Abs(zcoordlist.Min()) * scale);

            //
            for (int i = 0; i < list_trimedmap_path.Count; i++)
            {
                Bitmap copy = new Bitmap(scale, scale);
                Graphics graphics = Graphics.FromImage(copy);

                Bitmap source = new Bitmap(list_trimedmap_path[i]);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                graphics.DrawImage(source, 0, 0, scale, scale);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                source.Dispose();
                graphics.Dispose();

                string xcoord = list_trimedmap_path[i].Split(',')[0].Replace(trimedmap_folder + "\\", "");
                string zcoord = list_trimedmap_path[i].Split(',')[1].Replace(".png", "");

                int x = int.Parse(xcoord) * scale;
                int z = int.Parse(zcoord) * scale;

                g.DrawImage(copy, new Point(x, z));
            }
            g.Dispose();

            bitmap.Save(compmap_path, System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Dispose();
        }

        public static void Designed_Realm3rd(string trimedmap_folder, string compmap_path, int scale)
        {
            List<string> list_trimedmap_path = new List<string>();  //最小地図のファイルパスのリスト
            List<int> xcoordlist = new List<int>();                 //最小地図のXのリスト
            List<int> zcoordlist = new List<int>();                 //最小地図のZのリスト

            //最小地図フォルダ内を検索
            var trimedmap_info = Directory.EnumerateFiles(trimedmap_folder, "*", SearchOption.AllDirectories);

            //最小地図フォルダから、ファイルパス・Xナンバリング・Zナンバリングをそれぞれのリストに追加
            foreach (string info in trimedmap_info)
            {
                list_trimedmap_path.Add(info);

                string xcoord = info.Split(',')[0].Replace(trimedmap_folder + "\\", "");
                string zcoord = info.Split(',')[1].Replace(".png", "");

                xcoordlist.Add(int.Parse(xcoord));
                zcoordlist.Add(int.Parse(zcoord) * -1);
            }

            //X・Zの最大・最小数の絶対値 + 1 で統合地図サイズを計算
            int x_range = (Math.Abs(xcoordlist.Max()) + Math.Abs(xcoordlist.Min()) + 1) * scale;
            int z_range = (Math.Abs(zcoordlist.Max()) + Math.Abs(zcoordlist.Min()) + 1) * scale;

            //↑で算出した統合地図サイズの画像を作成
            Bitmap bitmap = new Bitmap(x_range, z_range);
            Graphics g = Graphics.FromImage(bitmap);

            g.TranslateTransform(Math.Abs(xcoordlist.Min()) * scale, Math.Abs(zcoordlist.Min()) * scale);

            //
            for (int i = 0; i < list_trimedmap_path.Count; i++)
            {
                Bitmap copy = new Bitmap(scale, scale);
                Graphics graphics = Graphics.FromImage(copy);

                Bitmap source = new Bitmap(list_trimedmap_path[i]);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                graphics.DrawImage(source, 0, 0, scale, scale);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                source.Dispose();
                graphics.Dispose();

                string xcoord = list_trimedmap_path[i].Split(',')[0].Replace(trimedmap_folder + "\\", "");
                string zcoord = list_trimedmap_path[i].Split(',')[1].Replace(".png", "");

                int x = int.Parse(xcoord) * scale;
                int z = int.Parse(zcoord) * scale * -1;

                g.DrawImage(copy, new Point(x, z));
            }
            g.Dispose();

            bitmap.Save(compmap_path, System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Dispose();
        }

        public static void Original_Area(string area_text_path, string trimedmap_folder, string areacompmap_path)
        {
            List<string> coordslist = new List<string>();
            List<int> xcoordslist = new List<int>();
            List<int> zcoordslist = new List<int>();

            try
            {
                StreamReader sr = new StreamReader(area_text_path);
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    coordslist.Add(line);
                }
                sr.Close();
            }
            catch
            {
            }

            foreach (string precoord in coordslist)
            {
                string prexcoord = precoord.ToString().Split(',')[0];
                string prezcoord = precoord.ToString().Split(',')[1];

                xcoordslist.Add(int.Parse(prexcoord));
                zcoordslist.Add(int.Parse(prezcoord));
            }

            int scale = 384;

            int xcoordmax = xcoordslist.Max(); //Xの最大値
            int xcoordmin = xcoordslist.Min(); //Xの最小値
            int zcoordmax = zcoordslist.Max(); //Zの最大値
            int zcoordmin = zcoordslist.Min(); //Zの最小値
            int absxcoordmin = Math.Abs(xcoordmin); //X最小値の絶対値
            int abszcoordmin = Math.Abs(zcoordmin); //Z最小値の絶対値

            double precenterxcoord = Math.Truncate(((double)xcoordmax + (double)xcoordmin) / 2);
            double precenterzcoord = Math.Truncate(((double)zcoordmax + (double)zcoordmin) / 2);
            int centerxcoord = (int)precenterxcoord;
            int centerzcoord = (int)precenterzcoord;

            int prexcoordmaxsize = xcoordmax - Math.Abs(centerxcoord);
            int prexcoordminsize = xcoordmin - Math.Abs(centerxcoord);
            int prezcoordmaxsize = zcoordmax - Math.Abs(centerzcoord);
            int prezcoordminsize = zcoordmin - Math.Abs(centerzcoord);
            int xcoordsize = (prexcoordmaxsize + (prexcoordminsize * -1) + 1) * scale;
            int zcoordsize = (prezcoordmaxsize + (prezcoordminsize * -1) + 1) * scale;

            Bitmap bitmap = new Bitmap(xcoordsize, zcoordsize);
            Graphics g = Graphics.FromImage(bitmap);

            g.TranslateTransform(Math.Abs(prexcoordminsize) * scale, Math.Abs(prezcoordminsize) * scale);

            foreach (string mapcoord in coordslist)
            {
                int mapxcoord = int.Parse(mapcoord.Split(',')[0]);
                int mapzcoord = int.Parse(mapcoord.Split(',')[1]);

                int resizexcoord = mapxcoord - Math.Abs(centerxcoord);
                int resizezcoord = mapzcoord - Math.Abs(centerzcoord);

                Image image = Image.FromFile($@"{trimedmap_folder}\{mapxcoord},{mapzcoord}.png");

                int fx = resizexcoord * scale;
                int fz = resizezcoord * scale;

                g.DrawImage(image, new Point(fx, fz));
            }
            g.Dispose();

            bitmap.Save(areacompmap_path, System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Dispose();
        }

        public static void Designed_Realm3rd_Area(string area_text_path, string trimedmap_folder, string areacompmap_path)
        {
            List<string> coordslist = new List<string>();
            List<int> xcoordslist = new List<int>();
            List<int> zcoordslist = new List<int>();

            try
            {
                StreamReader sr = new StreamReader(area_text_path);
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    coordslist.Add(line);
                }
                sr.Close();
            }
            catch
            {
            }

            foreach (string precoord in coordslist)
            {
                string prexcoord = precoord.ToString().Split(',')[0];
                string prezcoord = precoord.ToString().Split(',')[1];

                xcoordslist.Add(int.Parse(prexcoord));
                zcoordslist.Add(int.Parse(prezcoord) * -1);
            }

            int scale = 384;

            int xcoordmax = xcoordslist.Max(); //Xの最大値
            int xcoordmin = xcoordslist.Min(); //Xの最小値
            int zcoordmax = zcoordslist.Max(); //Zの最大値
            int zcoordmin = zcoordslist.Min(); //Zの最小値
            int absxcoordmin = Math.Abs(xcoordmin); //X最小値の絶対値
            int abszcoordmin = Math.Abs(zcoordmin); //Z最小値の絶対値

            double precenterxcoord = Math.Truncate(((double)xcoordmax + (double)xcoordmin) / 2);
            double precenterzcoord = Math.Truncate(((double)zcoordmax + (double)zcoordmin) / 2);
            int centerxcoord = (int)precenterxcoord;
            int centerzcoord = (int)precenterzcoord;

            int prexcoordmaxsize = xcoordmax - Math.Abs(centerxcoord);
            int prexcoordminsize = xcoordmin - Math.Abs(centerxcoord);
            int prezcoordmaxsize = zcoordmax - Math.Abs(centerzcoord);
            int prezcoordminsize = zcoordmin - Math.Abs(centerzcoord);
            int xcoordsize = (prexcoordmaxsize + (prexcoordminsize * -1) + 1) * scale;
            int zcoordsize = (prezcoordmaxsize + (prezcoordminsize * -1) + 1) * scale;

            Bitmap bitmap = new Bitmap(xcoordsize, zcoordsize);
            Graphics g = Graphics.FromImage(bitmap);

            g.TranslateTransform(Math.Abs(prexcoordminsize) * scale, Math.Abs(prezcoordminsize) * scale);

            foreach (string mapcoord in coordslist)
            {
                int mapxcoord = int.Parse(mapcoord.Split(',')[0]);
                int mapzcoord = int.Parse(mapcoord.Split(',')[1]);

                int resizexcoord = mapxcoord - Math.Abs(centerxcoord);
                int resizezcoord = (mapzcoord * -1) - Math.Abs(centerzcoord);

                Image image = Image.FromFile($@"{trimedmap_folder}\{mapxcoord},{mapzcoord}.png");

                int fx = resizexcoord * scale;
                int fz = resizezcoord * scale;

                g.DrawImage(image, new Point(fx, fz));
            }
            g.Dispose();

            bitmap.Save(areacompmap_path, System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Dispose();
        }
    }
}